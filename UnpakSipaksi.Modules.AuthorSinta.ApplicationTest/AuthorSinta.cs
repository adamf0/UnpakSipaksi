using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.AuthorSinta.Application.CreateAuthorSinta;
using UnpakSipaksi.Modules.AuthorSinta.Application.DeleteAuthorSinta;
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
    }
}
