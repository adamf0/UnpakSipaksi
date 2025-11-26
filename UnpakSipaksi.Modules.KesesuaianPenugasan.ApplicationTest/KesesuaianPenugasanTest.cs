using Dapper;
using Docker.DotNet.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq;
using Moq.Dapper;
using System.Data.Common;
using System.Reflection;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KesesuaianPenugasan.Application.CreateKesesuaianPenugasan;
using UnpakSipaksi.Modules.KesesuaianPenugasan.Application.DeleteKesesuaianPenugasan;
using UnpakSipaksi.Modules.KesesuaianPenugasan.Application.GetAllKesesuaianPenugasan;
using UnpakSipaksi.Modules.KesesuaianPenugasan.Application.GetBobotKesesuaianPenugasan;
using UnpakSipaksi.Modules.KesesuaianPenugasan.Application.GetKesesuaianPenugasan;
using UnpakSipaksi.Modules.KesesuaianPenugasan.Application.UpdateKesesuaianPenugasan;
using UnpakSipaksi.Modules.KesesuaianPenugasan.Domain.KesesuaianPenugasan;
using Xunit;

namespace UnpakSipaksi.Modules.KesesuaianPenugasan.ApplicationTest
{
    public class KesesuaianPenugasanTest : BaseIntegrationTest
    {
        public KesesuaianPenugasanTest(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        public static IEnumerable<object[]> InvalidData()
        {
            var valid = Guid.NewGuid().ToString();
            var empty = "";

            // CREATE
            yield return new object[] { empty, "", 10, "'Nama' tidak boleh kosong.", "created" };
            yield return new object[] { empty, "tes", -10, "'Nilai' tidak boleh negative.", "created" };

            // UPDATE
            yield return new object[] { valid, "", 10, "'Nama' tidak boleh kosong.", "updated" };
            yield return new object[] { valid, "tes", -10, "'Nilai' tidak boleh negative.", "updated" };
            yield return new object[] { empty, "tes", 10, "'Uuid' tidak boleh kosong.", "updated" };
            yield return new object[] { "no-guid", "tes", 10, "'Uuid' harus dalam format UUID v4 yang valid.", "updated" };

            // DELETE
            yield return new object[] { empty, "tes", 10, "'Uuid' tidak boleh kosong.", "deleted" };
            yield return new object[] { "no-guid", "tes", 10, "'Uuid' harus dalam format UUID v4 yang valid.", "deleted" };
        }

        public static IEnumerable<object?[]> ValidData()
        {
            yield return new object?[] { new object[] { "tes", 100 }, null, "created" };
            yield return new object?[] { new object[] { "tes", 100 }, new object[] { "tes2", 200 }, "updated" };
            yield return new object?[] { new object[] { "tes", 100 }, null, "deleted" };
        }

        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task CreateUpdateDelete_ShouldThrow_WhenInvalidFluentValidation(
            string uuid,
            string nama,
            int nilai,
            string message,
            string mode)
        {
            // Act
            Result? result = null;
            if (mode == "created")
            {
                var command = new CreateKesesuaianPenugasanCommand(nama, nilai);
                result = await Sender.Send(command);
            }
            else if (mode == "updated")
            {
                var command = new UpdateKesesuaianPenugasanCommand(uuid, nama, nilai);
                result = await Sender.Send(command);
            }
            else
            {
                var command = new DeleteKesesuaianPenugasanCommand(uuid);
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
            var nilaiBefore = (int)beforeData[1];

            var createCommand = new CreateKesesuaianPenugasanCommand(namaBefore, nilaiBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.KesesuaianPenugasan.FirstOrDefault(p => p.Uuid == createResult!.Value);

            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);
            Assert.Equal(nilaiBefore, dataCreate.Nilai);

            using (var scope = Factory.Services.CreateScope())
            {
                var handler = scope.ServiceProvider
                    .GetService<IRequestHandler<CreateKesesuaianPenugasanCommand, Result<Guid>>>();

                Assert.NotNull(handler);
                Assert.IsType<CreateKesesuaianPenugasanCommandHandler>(handler);
            }

            var newUuid = createResult.Value.ToString();

            // --- UPDATE / DELETE ---
            if (mode == "updated")
            {
                var namaAfter = (string)afterData![0];
                var nilaiAfter = (int)afterData[1];

                var updateCommand = new UpdateKesesuaianPenugasanCommand(newUuid, namaAfter, nilaiAfter);
                var updateResult = await Sender.Send(updateCommand);

                Assert.True(updateResult.IsSuccess);
                var dataUpdate = DBContext.KesesuaianPenugasan.FirstOrDefault(p => p.Uuid == createResult!.Value);

                Assert.NotNull(dataUpdate);
                Assert.Equal(namaAfter, dataUpdate.Nama);
                Assert.Equal(nilaiAfter, dataUpdate.Nilai);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<UpdateKesesuaianPenugasanCommand, Result>>();

                    Assert.NotNull(handler);
                    Assert.IsType<UpdateKesesuaianPenugasanCommandHandler>(handler);
                }
            }
            else if (mode == "deleted")
            {
                var deleteCommand = new DeleteKesesuaianPenugasanCommand(newUuid);
                var deleteResult = await Sender.Send(deleteCommand);

                Assert.True(deleteResult.IsSuccess);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<DeleteKesesuaianPenugasanCommand, Result>>();

                    Assert.NotNull(handler);
                    Assert.IsType<DeleteKesesuaianPenugasanCommandHandler>(handler);
                }
            }
        }

        [Fact]
        public async Task Create_ShouldThrow_WhenInvalidRuleDomain()
        {
            var nama = "tes";
            var nilai = int.MaxValue; // contoh melanggar aturan domain

            var command = new CreateKesesuaianPenugasanCommand(nama, nilai);
            var result = await Sender.Send(command);

            Assert.True(result.IsFailure);
            Assert.Equal("KesesuaianPenugasan.InvalidValueNilai", result.Error.Code);
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();
            var nama = "tes";
            var nilai = 10;

            var command = new UpdateKesesuaianPenugasanCommand(guid, nama, nilai);
            var result = await Sender.Send(command);

            Assert.True(result.IsFailure);
            Assert.Equal("KesesuaianPenugasan.NotFound", result.Error.Code);
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenInvalidRuleDomain()
        {
            // --- CREATE ---
            var namaBefore = "tes";
            var nilaiBefore = 10;

            var createCommand = new CreateKesesuaianPenugasanCommand(namaBefore, nilaiBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.KesesuaianPenugasan.FirstOrDefault(p => p.Uuid == createResult!.Value);

            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);
            Assert.Equal(nilaiBefore, dataCreate.Nilai);

            var newUuid = createResult.Value.ToString();

            // --- UPDATE (melanggar aturan domain)
            var namaAfter = "tes2";
            var nilaiAfter = int.MaxValue;

            var updateCommand = new UpdateKesesuaianPenugasanCommand(newUuid, namaAfter, nilaiAfter);
            var updateResult = await Sender.Send(updateCommand);

            Assert.True(updateResult.IsFailure);
            Assert.Equal("KesesuaianPenugasan.InvalidValueNilai", updateResult.Error.Code);
        }

        [Fact]
        public async Task Delete_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();

            var command = new DeleteKesesuaianPenugasanCommand(guid);
            var result = await Sender.Send(command);

            Assert.True(result.IsFailure);
            Assert.Equal("KesesuaianPenugasan.NotFound", result.Error.Code);
        }

        [Fact]
        public async Task GetAll_ReturnsList_WhenDataExists()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();

            var fake = new List<KesesuaianPenugasanResponse>
            {
                new() { Uuid = Guid.NewGuid().ToString(), Nama = "Data 1", Nilai = "10" }
            };

            mockConn.SetupDapperAsync(c =>
                c.QueryAsync<KesesuaianPenugasanResponse>(It.IsAny<string>(), null, null, null, null)
            ).ReturnsAsync(fake);

            mockFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConn.Object);

            var handler = new GetAllKesesuaianPenugasanQueryHandler(mockFactory.Object);
            var result = await handler.Handle(new GetAllKesesuaianPenugasanQuery(), CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Single(result.Value);
            Assert.Equal("Data 1", result.Value[0].Nama);
        }

        [Fact]
        public async Task GetAll_ReturnsFailure_WhenEmpty()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();

            mockConn.SetupDapperAsync(c =>
                c.QueryAsync<KesesuaianPenugasanResponse>(It.IsAny<string>(), null, null, null, null)
            ).ReturnsAsync(new List<KesesuaianPenugasanResponse>());

            mockFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConn.Object);

            var handler = new GetAllKesesuaianPenugasanQueryHandler(mockFactory.Object);
            var result = await handler.Handle(new GetAllKesesuaianPenugasanQuery(), CancellationToken.None);

            Assert.False(result.IsSuccess);
            Assert.Equal(KesesuaianPenugasanErrors.EmptyData().Code, result.Error.Code);
        }

        [Fact]
        public async Task GetBobot_ReturnsSuccess_WhenSingleValueFound()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();

            mockConn.SetupDapperAsync(c =>
                c.QueryAsync<int>(It.IsAny<string>(), null, null, null, null)
            ).ReturnsAsync(new List<int> { 100 });

            mockFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConn.Object);

            var handler = new GetBobotKesesuaianPenugasanQueryHandler(mockFactory.Object);
            var result = await handler.Handle(new GetBobotKesesuaianPenugasanQuery(), CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal(100, result.Value);
        }

        [Fact]
        public async Task GetBobot_ReturnsFailure_WhenEmpty()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();

            mockConn.SetupDapperAsync(c =>
                c.QueryAsync<int>(It.IsAny<string>(), null, null, null, null)
            ).ReturnsAsync(new List<int>());

            mockFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConn.Object);

            var handler = new GetBobotKesesuaianPenugasanQueryHandler(mockFactory.Object);
            var result = await handler.Handle(new GetBobotKesesuaianPenugasanQuery(), CancellationToken.None);

            Assert.False(result.IsSuccess);
            Assert.Equal(KesesuaianPenugasanErrors.EmptyData().Code, result.Error.Code);
        }

        [Fact]
        public async Task GetBobot_ReturnsFailure_WhenMultipleValues()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();

            mockConn.SetupDapperAsync(c =>
                c.QueryAsync<int>(It.IsAny<string>(), null, null, null, null)
            ).ReturnsAsync(new List<int> { 10, 20 });

            mockFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConn.Object);

            var handler = new GetBobotKesesuaianPenugasanQueryHandler(mockFactory.Object);
            var result = await handler.Handle(new GetBobotKesesuaianPenugasanQuery(), CancellationToken.None);

            Assert.False(result.IsSuccess);
            Assert.Equal(KesesuaianPenugasanErrors.NotSameValue().Code, result.Error.Code);
        }

        [Fact]
        public async Task GetDefault_ReturnsSuccess_WhenFound()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();

            var uuid = Guid.NewGuid();
            var fake = new KesesuaianPenugasanDefaultResponse
            {
                Id = "1",
                Uuid = uuid.ToString(),
                Nama = "Default",
                Nilai = 10
            };

            mockConn.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KesesuaianPenugasanDefaultResponse?>(It.IsAny<string>(), It.IsAny<object>(), null, null, null)
            ).ReturnsAsync(fake);

            mockFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConn.Object);

            var handler = new GetKesesuaianPenugasanDefaultQueryHandler(mockFactory.Object);
            var result = await handler.Handle(new GetKesesuaianPenugasanDefaultQuery(uuid), CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal("Default", result.Value.Nama);
        }

        [Fact]
        public async Task GetDefault_ReturnsFailure_WhenNotFound()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();

            mockConn.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KesesuaianPenugasanDefaultResponse?>(It.IsAny<string>(), It.IsAny<object>(), null, null, null)
            ).ReturnsAsync((KesesuaianPenugasanDefaultResponse?)null);

            mockFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConn.Object);

            var uuid = Guid.NewGuid();
            var handler = new GetKesesuaianPenugasanDefaultQueryHandler(mockFactory.Object);
            var result = await handler.Handle(new GetKesesuaianPenugasanDefaultQuery(uuid), CancellationToken.None);

            Assert.False(result.IsSuccess);
            Assert.Equal(KesesuaianPenugasanErrors.NotFound(uuid).Code, result.Error.Code);
        }

        [Fact]
        public async Task GetByUuid_ReturnsSuccess_WhenFound()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();

            var uuid = Guid.NewGuid();
            var fake = new KesesuaianPenugasanResponse
            {
                Uuid = uuid.ToString(),
                Nama = "Some Data",
                Nilai = "99"
            };

            mockConn.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KesesuaianPenugasanResponse?>(It.IsAny<string>(), It.IsAny<object>(), null, null, null)
            ).ReturnsAsync(fake);

            mockFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConn.Object);

            var handler = new GetKesesuaianPenugasanQueryHandler(mockFactory.Object);
            var result = await handler.Handle(new GetKesesuaianPenugasanQuery(uuid), CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal("Some Data", result.Value.Nama);
        }

        [Fact]
        public async Task GetByUuid_ReturnsFailure_WhenNotFound()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();

            mockConn.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KesesuaianPenugasanResponse?>(It.IsAny<string>(), It.IsAny<object>(), null, null, null)
            ).ReturnsAsync((KesesuaianPenugasanResponse?)null);

            mockFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConn.Object);

            var uuid = Guid.NewGuid();
            var handler = new GetKesesuaianPenugasanQueryHandler(mockFactory.Object);
            var result = await handler.Handle(new GetKesesuaianPenugasanQuery(uuid), CancellationToken.None);

            Assert.False(result.IsSuccess);
            Assert.Equal(KesesuaianPenugasanErrors.NotFound(uuid).Code, result.Error.Code);
        }
    }
}
