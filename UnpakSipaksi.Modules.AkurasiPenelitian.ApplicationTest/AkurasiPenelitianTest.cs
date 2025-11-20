using Dapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Dapper;
using System.Data;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.AkurasiPenelitian.Application.CreateAkurasiPenelitian;
using UnpakSipaksi.Modules.AkurasiPenelitian.Application.DeleteAkurasiPenelitian;
using UnpakSipaksi.Modules.AkurasiPenelitian.Application.GetAkurasiPenelitian;
using UnpakSipaksi.Modules.AkurasiPenelitian.Application.GetAllAkurasiPenelitian;
using UnpakSipaksi.Modules.AkurasiPenelitian.Application.UpdateAkurasiPenelitian;
using Xunit;

namespace Application.Integration.Tests
{
    public class AkurasiPenelitianTest : BaseIntegrationTest
    {
        public AkurasiPenelitianTest(IntegrationTestWebAppFactory factory) : base(factory)
        {

        }

        public static IEnumerable<object[]> InvalidData()
        {
            var valid = Guid.NewGuid().ToString();
            var empty = "";

            yield return new object[] { empty, "", 10, "'Nama' tidak boleh kosong.", "created" };
            yield return new object[] { empty, "tes", -10, "'Skor' tidak boleh negative.", "created" };

            yield return new object[] { valid, "", 10, "'Nama' tidak boleh kosong.", "updated" };
            yield return new object[] { valid, "tes", -10, "'Skor' tidak boleh negative.", "updated" };
            yield return new object[] { empty, "tes", 10, "'Uuid' tidak boleh kosong.", "updated" };
            yield return new object[] { "no-guid", "tes", 10, "'Uuid' harus dalam format UUID v4 yang valid.", "updated" };

            yield return new object[] { empty, "tes", 10, "'Uuid' tidak boleh kosong.", "deleted" };
            yield return new object[] { "no-guid", "tes", 10, "'Uuid' harus dalam format UUID v4 yang valid.", "deleted" };

            yield return new object[] { empty, "", 0, "'Uuid' tidak boleh kosong.", "get" };
            yield return new object[] { "no-guid", "", 0, "'Uuid' harus dalam format UUID v4 yang valid.", "get" };
        }

        public static IEnumerable<object?[]> ValidData()
        {
            yield return new object?[] { new object[] { "tes", 10 }, null, "created" };
            yield return new object?[] { new object[] { "tes", 10 }, new object[] { "tes2", 11 }, "updated" };
            yield return new object?[] { new object[] { "tes", 10 }, null, "deleted" };
        }

        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task CreateUpdateDelete_ShouldThrow_WhenInvalidFluentValidation(
            string uuid,
            string nama,
            int skor,
            string message,
            string mode)
        {
            // Act
            Result? result = null;
            if (mode == "created")
            {
                var command = new CreateAkurasiPenelitianCommand(nama, skor);
                result = await Sender.Send(command);
            }
            else if (mode == "updated")
            {
                var command = new UpdateAkurasiPenelitianCommand(uuid, nama, skor);
                result = await Sender.Send(command);
            }
            else if (mode == "deleted")
            {
                var command = new DeleteAkurasiPenelitianCommand(uuid);
                result = await Sender.Send(command);
            }
            else {
                var command = new GetAkurasiPenelitianQuery(uuid);
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
            var namaBefore = (string)beforeData[0];
            var skorBefore = (int)beforeData[1];

            var createCommand = new CreateAkurasiPenelitianCommand(namaBefore, skorBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.AkurasiPenelitian.FirstOrDefault(p => p.Uuid == createResult!.Value);

            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);
            Assert.Equal(skorBefore, dataCreate.Skor);

            using (var scope = Factory.Services.CreateScope())
            {
                var handler = scope.ServiceProvider
                    .GetService<IRequestHandler<CreateAkurasiPenelitianCommand, Result<Guid>>>();

                Assert.NotNull(handler);
                Assert.IsType<CreateAkurasiPenelitianCommandHandler>(handler);
            }

            var newUuid = createResult.Value.ToString();

            // --- UPDATE / DELETE ---
            if (mode == "updated")
            {
                var namaAfter = (string)afterData![0];
                var skorAfter = (int)afterData[1];

                var updateCommand = new UpdateAkurasiPenelitianCommand(newUuid, namaAfter, skorAfter);
                var updateResult = await Sender.Send(updateCommand);

                Assert.True(updateResult.IsSuccess);
                var dataUpdate = DBContext.AkurasiPenelitian.FirstOrDefault(p => p.Uuid == createResult!.Value);

                Assert.NotNull(dataUpdate);
                Assert.Equal(namaAfter, dataUpdate.Nama);
                Assert.Equal(skorAfter, dataUpdate.Skor);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<UpdateAkurasiPenelitianCommand, Result>>();

                    Assert.NotNull(handler);
                    Assert.IsType<UpdateAkurasiPenelitianCommandHandler>(handler);
                }
            }
            else if (mode == "deleted")
            {
                var deleteCommand = new DeleteAkurasiPenelitianCommand(newUuid);
                var deleteResult = await Sender.Send(deleteCommand);

                Assert.True(deleteResult.IsSuccess);
            }
        }

        [Fact]
        public async Task Create_ShouldThrow_WhenInvalidRuleDomain()
        {
            var namaBefore = "tes";
            var skorBefore = int.MaxValue;

            var createCommand = new CreateAkurasiPenelitianCommand(namaBefore, skorBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsFailure);
            Assert.Equal("AkurasiPenelitian.InvalidSkor", createResult.Error.Code);
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();
            var namaBefore = "tes";
            var skorBefore = 1;

            var updateCommand = new UpdateAkurasiPenelitianCommand(guid, namaBefore, skorBefore);
            var updateResult = await Sender.Send(updateCommand);

            Assert.True(updateResult.IsFailure);
            Assert.Equal("AkurasiPenelitian.NotFound", updateResult.Error.Code);
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenInvalidRuleDomain()
        {
            // --- CREATE ---
            var namaBefore = "tes";
            var skorBefore = 1;

            var createCommand = new CreateAkurasiPenelitianCommand(namaBefore, skorBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.AkurasiPenelitian.FirstOrDefault(p => p.Uuid == createResult!.Value);

            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);
            Assert.Equal(skorBefore, dataCreate.Skor);

            var newUuid = createResult.Value.ToString();

            // --- UPDATE ---
            //if (mode == "updated")
            //{
                var namaAfter = "tes2";
                var skorAfter = int.MaxValue;

                var updateCommand = new UpdateAkurasiPenelitianCommand(newUuid, namaAfter, skorAfter);
                var updateResult = await Sender.Send(updateCommand);

                Assert.True(updateResult.IsFailure);
                Assert.Equal("AkurasiPenelitian.InvalidSkor", updateResult.Error.Code);
            //}
        }

        [Fact]
        public async Task Delete_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();

            var deleteCommand = new DeleteAkurasiPenelitianCommand(guid);
            var deleteResult = await Sender.Send(deleteCommand);

            Assert.True(deleteResult.IsFailure);
            Assert.Equal("AkurasiPenelitian.NotFound", deleteResult.Error.Code);
        }

        [Fact]
        public async Task Handle_ReturnsList_WhenDataExists()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            var fakeData = new List<AkurasiPenelitianResponse>
            {
                new AkurasiPenelitianResponse
                {
                    Uuid = "123",
                    Nama = "Penelitian 1",
                    Skor = 20
                }
            };

            // Setup Dapper extension
            mockConnection.SetupDapperAsync(c => c.QueryAsync<AkurasiPenelitianResponse>(
            It.IsAny<string>(), null, null, null, null))
            .ReturnsAsync(fakeData);

            mockConnectionFactory
             .Setup(f => f.OpenConnectionAsync())
             .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetAllAkurasiPenelitianQueryHandler(mockConnectionFactory.Object);
            var query = new GetAllAkurasiPenelitianQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotEmpty(result.Value);
            Assert.Equal(fakeData.First().Nama, result.Value.First().Nama);
        }

        [Fact]
        public async Task Handle_ReturnsFailure_WhenNoData()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            // Empty data
            mockConnection.SetupDapperAsync(c => c.QueryAsync<AkurasiPenelitianResponse>(
                It.IsAny<string>(), null, null, null, null))
                .ReturnsAsync(new List<AkurasiPenelitianResponse>());

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConnection.Object);

            var handler = new GetAllAkurasiPenelitianQueryHandler(mockConnectionFactory.Object);
            var query = new GetAllAkurasiPenelitianQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }
    }
}
