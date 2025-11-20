using Dapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.AuthorSinta.Application.CreateAuthorSinta;
using UnpakSipaksi.Modules.AuthorSinta.Application.DeleteAuthorSinta;
using UnpakSipaksi.Modules.AuthorSinta.Application.GetAllAuthorSinta;
using UnpakSipaksi.Modules.AuthorSinta.Application.GetAuthorSinta;
using UnpakSipaksi.Modules.AuthorSinta.Application.UpdateAuthorSinta;
using UnpakSipaksi.Modules.AuthorSinta.ApplicationTest;
using Xunit;

namespace Application.Integration.Tests
{
    public class AuthorSintaTest : BaseIntegrationTest
    {
        public AuthorSintaTest(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        public static IEnumerable<object[]> InvalidData()
        {
            var valid = Guid.NewGuid().ToString();
            var empty = "";

            // CREATE
            yield return new object[] { empty, "", "1234567", 10, "'Nidn' tidak boleh kosong.", "created" };
            yield return new object[] { empty, "1234567890", "", 10, "'AuthorId' tidak boleh kosong.", "created" };
            yield return new object[] { empty, "1234567890", "1234567", 0, "'Score' tidak boleh kosong.", "created" };

            // UPDATE
            yield return new object[] { valid, "", "1234567", 10, "'Nidn' tidak boleh kosong.", "updated" };
            yield return new object[] { valid, "1234567890", "", 10, "'AuthorId' tidak boleh kosong.", "updated" };
            yield return new object[] { valid, "1234567890", "1234567", 0, "'Score' tidak boleh kosong.", "updated" };
            yield return new object[] { empty, "1234567890", "1234567", 10, "'Uuid' tidak boleh kosong.", "updated" };
            yield return new object[] { "no-guid", "1234567890", "1234567", 10, "'Uuid' harus dalam format UUID v4 yang valid.", "updated" };

            // DELETE
            yield return new object[] { empty, "1234567890", "1234567", 10, "'Uuid' tidak boleh kosong.", "deleted" };
            yield return new object[] { "no-guid", "1234567890", "1234567", 10, "'Uuid' harus dalam format UUID v4 yang valid.", "deleted" };
        }

        public static IEnumerable<object?[]> ValidData()
        {
            yield return new object?[] { new object[] { "1234567890", "1234567", 100 }, null, "created" };
            yield return new object?[] { new object[] { "1234567890", "1234567", 100 }, new object[] { "9876543210", "7654321", 200 }, "updated" };
            yield return new object?[] { new object[] { "1234567890", "1234567", 100 }, null, "deleted" };
        }

        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task CreateUpdateDelete_ShouldThrow_WhenInvalidFluentValidation(
            string uuid,
            string nidn,
            string authorId,
            int score,
            string message,
            string mode)
        {
            // Act
            Result? result = null;
            if (mode == "created")
            {
                var command = new CreateAuthorSintaCommand(nidn, authorId, score);
                result = await Sender.Send(command);
            }
            else if (mode == "updated")
            {
                var command = new UpdateAuthorSintaCommand(uuid, nidn, authorId, score);
                result = await Sender.Send(command);
            }
            else
            {
                var command = new DeleteAuthorSintaCommand(uuid);
                result = await Sender.Send(command);
            }

            // Assert
            Assert.True(result.IsFailure);
            if (result.Error is ValidationError validationError)
            {
                Assert.Contains(validationError.Errors, e => e.Description == message);
            }
            else
            {
                Assert.Equal(message, result.Error.Description);
            }
        }

        [Theory]
        [MemberData(nameof(ValidData))]
        public async Task CreateUpdateDelete_ShouldBeExecute_WhenValidData(
            object[] beforeData,
            object[]? afterData,
            string mode)
        {
            // --- CREATE ---
            var nidnBefore = (string)beforeData[0];
            var authorIdBefore = (string)beforeData[1];
            var scoreBefore = (int)beforeData[2];

            var createCommand = new CreateAuthorSintaCommand(nidnBefore, authorIdBefore, scoreBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.AuthorSinta.FirstOrDefault(p => p.Uuid == createResult!.Value);

            Assert.NotNull(dataCreate);
            Assert.Equal(nidnBefore, dataCreate.Nidn);
            Assert.Equal(authorIdBefore, dataCreate.AuthorId);
            Assert.Equal(scoreBefore, dataCreate.Score);

            using (var scope = Factory.Services.CreateScope())
            {
                var handler = scope.ServiceProvider
                    .GetService<IRequestHandler<CreateAuthorSintaCommand, Result<Guid>>>();

                Assert.NotNull(handler);
                Assert.IsType<CreateAuthorSintaCommandHandler>(handler);
            }

            var newUuid = createResult.Value.ToString();

            // --- UPDATE / DELETE ---
            if (mode == "updated")
            {
                var nidnAfter = (string)afterData![0];
                var authorIdAfter = (string)afterData[1];
                var scoreAfter = (int)afterData[2];

                var updateCommand = new UpdateAuthorSintaCommand(newUuid, nidnAfter, authorIdAfter, scoreAfter);
                var updateResult = await Sender.Send(updateCommand);

                Assert.True(updateResult.IsSuccess);
                var dataUpdate = DBContext.AuthorSinta.FirstOrDefault(p => p.Uuid.ToString() == createResult!.Value.ToString());

                Assert.NotNull(dataUpdate);
                Assert.Equal(nidnAfter, dataUpdate.Nidn);
                Assert.Equal(authorIdAfter, dataUpdate.AuthorId);
                Assert.Equal(scoreAfter, dataUpdate.Score);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<UpdateAuthorSintaCommand, Result>>();

                    Assert.NotNull(handler);
                    Assert.IsType<UpdateAuthorSintaCommandHandler>(handler);
                }
            }
            else if (mode == "deleted")
            {
                var deleteCommand = new DeleteAuthorSintaCommand(newUuid);
                var deleteResult = await Sender.Send(deleteCommand);

                Assert.True(deleteResult.IsSuccess);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<DeleteAuthorSintaCommand, Result>>();

                    Assert.NotNull(handler);
                    Assert.IsType<DeleteAuthorSintaCommandHandler>(handler);
                }
            }
        }

        [Fact]
        public async Task Create_ShouldThrow_WhenInvalidRuleDomain()
        {
            var nidnBefore = "1234567890";
            var authorIdBefore = "1234567";
            var scoreBefore = -100;

            var createCommand = new CreateAuthorSintaCommand(nidnBefore, authorIdBefore, scoreBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsFailure);
            Assert.Equal("AuthorSinta.InvalidSkor", createResult.Error.Code);
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();
            var nidnBefore = "1234567890";
            var authorIdBefore = "1234567";
            var scoreBefore = -100;

            var updateCommand = new UpdateAuthorSintaCommand(guid, nidnBefore, authorIdBefore, scoreBefore);
            var updateResult = await Sender.Send(updateCommand);

            Assert.True(updateResult.IsFailure);
            Assert.Equal("AuthorSinta.NotFound", updateResult.Error.Code);
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenInvalidRuleDomain()
        {
            // --- CREATE ---
            var nidnBefore = "1234567890";
            var authorIdBefore = "1234567";
            var scoreBefore = 100;

            var createCommand = new CreateAuthorSintaCommand(nidnBefore, authorIdBefore, scoreBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.AuthorSinta.FirstOrDefault(p => p.Uuid == createResult!.Value);

            Assert.NotNull(dataCreate);
            Assert.Equal(nidnBefore, dataCreate.Nidn);
            Assert.Equal(authorIdBefore, dataCreate.AuthorId);
            Assert.Equal(scoreBefore, dataCreate.Score);

            var newUuid = createResult.Value.ToString();

            // --- UPDATE ---
            //if (mode == "updated")
            //{
            var nidnAfter = "1234567890";
            var authorIdAfter = "1234567";
            var scoreAfter = -100;

            var updateCommand = new UpdateAuthorSintaCommand(newUuid, nidnAfter, authorIdAfter, scoreAfter);
            var updateResult = await Sender.Send(updateCommand);

            Assert.True(updateResult.IsFailure);
            Assert.Equal("AuthorSinta.InvalidSkor", updateResult.Error.Code);
            //}
        }

        [Fact]
        public async Task Delete_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();

            var deleteCommand = new DeleteAuthorSintaCommand(guid);
            var deleteResult = await Sender.Send(deleteCommand);

            Assert.True(deleteResult.IsFailure);
            Assert.Equal("AuthorSinta.NotFound", deleteResult.Error.Code);
        }

        [Fact]
        public async Task HandleList_ReturnsSuccess_WhenDataExists()
        {
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            var fakeData = new List<AuthorSintaResponse>
            {
                new AuthorSintaResponse { Uuid = "1", Nidn = "123", AuthorId = "A1", Score = 80 }
            };

            mockConnection.SetupDapperAsync(c => c.QueryAsync<AuthorSintaResponse>(
                It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync(fakeData);

            mockConnectionFactory
                .Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetAllAuthorSintaQueryHandler(mockConnectionFactory.Object);
            var query = new GetAllAuthorSintaQuery();

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.NotEmpty(result.Value);
            Assert.Equal(fakeData.First().Uuid, result.Value.First().Uuid);
        }

        [Fact]
        public async Task HandleList_ReturnsFailure_WhenNoData()
        {
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            mockConnection.SetupDapperAsync(c => c.QueryAsync<AuthorSintaResponse>(
                It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync(new List<AuthorSintaResponse>());

            mockConnectionFactory
                .Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetAllAuthorSintaQueryHandler(mockConnectionFactory.Object);
            var query = new GetAllAuthorSintaQuery();

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task HandleSingle_ReturnsSuccess_WhenDataExists()
        {
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();
            var uuid = Guid.NewGuid();

            var fakeData = new AuthorSintaResponse { Uuid = uuid.ToString(), Nidn = "123", AuthorId = "A1", Score = 80 };

            mockConnection.SetupDapperAsync(c => c.QuerySingleOrDefaultAsync<AuthorSintaResponse>(
                It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync(fakeData);

            mockConnectionFactory
                .Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetAuthorSintaQueryHandler(mockConnectionFactory.Object);
            var query = new GetAuthorSintaQuery(uuid);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal(fakeData.Uuid, result.Value.Uuid);
        }

        [Fact]
        public async Task HandleSingle_ReturnsFailure_WhenDataNotFound()
        {
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            mockConnection.SetupDapperAsync(c => c.QuerySingleOrDefaultAsync<AuthorSintaResponse>(
                It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync((AuthorSintaResponse?)null);

            mockConnectionFactory
                .Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetAuthorSintaQueryHandler(mockConnectionFactory.Object);
            var query = new GetAuthorSintaQuery(Guid.NewGuid());

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.False(result.IsSuccess);
        }


    }
}
