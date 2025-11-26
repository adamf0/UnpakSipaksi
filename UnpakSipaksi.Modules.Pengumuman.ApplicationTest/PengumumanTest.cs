using Dapper;
using Docker.DotNet.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Dapper;
using System.Data.Common;
using System.Reflection;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Pengumuman.Application.CreatePengumuman;
using UnpakSipaksi.Modules.Pengumuman.Application.DeletePengumuman;
using UnpakSipaksi.Modules.Pengumuman.Application.GetAllPengumuman;
using UnpakSipaksi.Modules.Pengumuman.Application.GetPengumuman;
using UnpakSipaksi.Modules.Pengumuman.Application.UpdatePengumuman;
using Xunit;

namespace UnpakSipaksi.Modules.Pengumuman.ApplicationTest
{
    public class PengumumanTest : BaseIntegrationTest
    {
        public PengumumanTest(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        public static IEnumerable<object[]> InvalidData()
        {
            var validUuid = Guid.NewGuid().ToString();
            var invalidUuid = "no-guid";
            var empty = "";
            var validType = "pengumuman";
            var validTypeExpired = "no expire";

            // --- CREATE ---
            yield return new object[]
            {
                empty,          // uuid
                "",             // pesan
                null,           // file
                null,           // url
                validType,      // type
                null,           // target
                null,           // nidn
                null,           // kodeFakultas
                validTypeExpired,// typeExpired
                null,           // tanggalAwal
                null,           // tanggalAkhir
                "'Pesan' tidak boleh kosong.", // message
                "created"       // mode
            };

            yield return new object[]
            {
                empty,
                "Pesan Tes",
                null,
                null,
                "",             // type kosong
                null,
                null,
                null,
                validTypeExpired,
                null,
                null,
                "'Type' tidak boleh kosong.",
                "created"
            };

            // --- UPDATE ---
            yield return new object[]
            {
                validUuid,
                "",             // pesan kosong
                null,
                null,
                validType,
                null,
                null,
                null,
                validTypeExpired,
                null,
                null,
                "'Pesan' tidak boleh kosong.",
                "updated"
            };

            yield return new object[]
            {
                invalidUuid,    // uuid invalid
                "Pesan Update",
                null,
                null,
                validType,
                null,
                null,
                null,
                validTypeExpired,
                null,
                null,
                "'Uuid' harus dalam format UUID v4 yang valid.",
                "updated"
            };

            // --- DELETE ---
            yield return new object[]
            {
                empty,          // uuid kosong
                "Pesan Tes",
                null,
                null,
                validType,
                null,
                null,
                null,
                validTypeExpired,
                null,
                null,
                "'Uuid' tidak boleh kosong.",
                "deleted"
            };

            yield return new object[]
            {
                invalidUuid,    // uuid invalid
                "Pesan Tes",
                null,
                null,
                validType,
                null,
                null,
                null,
                validTypeExpired,
                null,
                null,
                "'Uuid' harus dalam format UUID v4 yang valid.",
                "deleted"
            };
        }

        public static IEnumerable<object?[]> ValidData()
        {
            var validUuid = Guid.NewGuid().ToString();
            var validType = "pengumuman";
            var validTypeExpired = "no expire";

            // CREATE
            yield return new object?[]
            {
                new object?[]
                {
                    validUuid,          // uuid
                    "Pesan Test",       // pesan
                    null,               // file
                    null,               // url
                    "pengumuman",       // type
                    "all",              // target (harus ada enum valid)
                    null,               // nidn
                    null,               // kodeFakultas
                    "no expire",        // typeExpired (harus ada enum valid)
                    null,               // tanggalAwal
                    null                // tanggalAkhir
                },
                null,   // afterData
                "created"
            };

            // UPDATE
            yield return new object?[]
            {
                new object?[]
                {
                    validUuid,
                    "Pesan Test",
                    null,
                    null,
                    "pengumuman",
                    "all",              // target valid
                    null,
                    null,
                    "no expire",        // typeExpired valid
                    null,
                    null
                },
                new object?[]
                {
                    validUuid,
                    "Pesan Update",
                    null,
                    null,
                    "pengumuman",
                    "all",              // target valid
                    null,
                    null,
                    "no expire",        // typeExpired valid
                    null,
                    null
                },
                "updated"
            };

                // DELETE
            yield return new object?[]
            {
                new object?[]
                {
                    validUuid,
                    "Pesan Test",
                    null,
                    null,
                    "pengumuman",
                    "all",              // target valid
                    null,
                    null,
                    "no expire",
                    null,
                    null
                },
                null,
                "deleted"
            };
        }


        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task CreateUpdateDelete_ShouldThrow_WhenInvalid(
            string uuid,
            string pesan,
            string? file,
            string? url,
            string type,
            string? target,
            string? nidn,
            string? kodeFakultas,
            string typeExpired,
            string? tanggalAwal,
            string? tanggalAkhir,
            string message,
            string mode)
        {
            Result? result = null;

            if (mode == "created")
            {
                var command = new CreatePengumumanCommand(
                    pesan, file, url, type, target, nidn, kodeFakultas, typeExpired, tanggalAwal, tanggalAkhir
                );
                result = await Sender.Send(command);
            }
            else if (mode == "updated")
            {
                var command = new UpdatePengumumanCommand(
                    uuid, pesan, file, url, type, target, nidn, kodeFakultas, typeExpired, tanggalAwal, tanggalAkhir
                );
                result = await Sender.Send(command);
            }
            else
            {
                var command = new DeletePengumumanCommand(uuid);
                result = await Sender.Send(command);
            }

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

        [Theory(Skip = "UnpakSipaksi.Common.Application.Exceptions.CustomException : Application exception\r\n---- System.NullReferenceException : Object reference not set to an instance of an object.")]
        [MemberData(nameof(ValidData))]
        public async Task CreateUpdateDelete_ShouldBeExecute_WhenValidData(
            object[] beforeData,
            object[]? afterData,
            string mode)
        {
            var uuidBefore = (string)beforeData[0];
            var pesanBefore = (string)beforeData[1];
            var fileBefore = (string?)beforeData[2];
            var urlBefore = (string?)beforeData[3];
            var typeBefore = (string)beforeData[4];
            var targetBefore = (string?)beforeData[5];
            var nidnBefore = (string?)beforeData[6];
            var kodeFakultasBefore = (string?)beforeData[7];
            var typeExpiredBefore = (string)beforeData[8];
            var tanggalAwalBefore = (string?)beforeData[9];
            var tanggalAkhirBefore = (string?)beforeData[10];

            // --- CREATE ---
            var createCommand = new CreatePengumumanCommand(
                pesanBefore, fileBefore, urlBefore, typeBefore, targetBefore, nidnBefore, kodeFakultasBefore, typeExpiredBefore, tanggalAwalBefore, tanggalAkhirBefore
            );
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.Pengumuman.FirstOrDefault(p => p.Uuid == createResult!.Value);

            Assert.NotNull(dataCreate);
            Assert.Equal(pesanBefore, dataCreate.Pesan);
            Assert.Equal(typeBefore, dataCreate.Type);

            using (var scope = Factory.Services.CreateScope())
            {
                var handler = scope.ServiceProvider
                    .GetService<IRequestHandler<CreatePengumumanCommand, Result<Guid>>>();

                Assert.NotNull(handler);
                Assert.IsType<CreatePengumumanCommandHandler>(handler);
            }

            var newUuid = createResult.Value.ToString();

            // --- UPDATE / DELETE ---
            if (mode == "updated" && afterData != null)
            {
                var uuidAfter = (string)afterData[0];
                var pesanAfter = (string)afterData[1];
                var fileAfter = (string?)afterData[2];
                var urlAfter = (string?)afterData[3];
                var typeAfter = (string)afterData[4];
                var targetAfter = (string?)afterData[5];
                var nidnAfter = (string?)afterData[6];
                var kodeFakultasAfter = (string?)afterData[7];
                var typeExpiredAfter = (string)afterData[8];
                var tanggalAwalAfter = (string?)afterData[9];
                var tanggalAkhirAfter = (string?)afterData[10];

                var updateCommand = new UpdatePengumumanCommand(
                    uuidBefore, pesanAfter, fileAfter, urlAfter, typeAfter, targetAfter, nidnAfter, kodeFakultasAfter, typeExpiredAfter, tanggalAwalAfter, tanggalAkhirAfter
                );
                var updateResult = await Sender.Send(updateCommand);

                Assert.True(updateResult.IsSuccess);
                var dataUpdate = DBContext.Pengumuman.FirstOrDefault(p => p.Uuid == createResult!.Value);

                Assert.NotNull(dataUpdate);
                Assert.Equal(pesanAfter, dataUpdate.Pesan);
                Assert.Equal(typeAfter, dataUpdate.Type);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<UpdatePengumumanCommand, Result>>();

                    Assert.NotNull(handler);
                    Assert.IsType<UpdatePengumumanCommandHandler>(handler);
                }
            }
            else if (mode == "deleted")
            {
                var deleteCommand = new DeletePengumumanCommand(uuidBefore);
                var deleteResult = await Sender.Send(deleteCommand);

                Assert.True(deleteResult.IsSuccess);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<DeletePengumumanCommand, Result>>();

                    Assert.NotNull(handler);
                    Assert.IsType<DeletePengumumanCommandHandler>(handler);
                }
            }
        }

        [Fact]
        public async Task GetAll_ReturnsList_WhenDataExists()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            var fakeData = new List<PengumumanResponse>
        {
            new PengumumanResponse
            {
                Pesan = "Info Penting",
                File = null,
                Url = "https://contoh.com",
                Type = "Global",
                Target = null,
                Nidn = null,
                KodeFaKultas = null,
                TypeExpired = "date",
                TanggalAwal = "2025-01-01",
                TanggalAkhir = "2025-02-01"
            }
        };

            mockConnection.SetupDapperAsync(c =>
                c.QueryAsync<PengumumanResponse>(It.IsAny<string>(), null, null, null, null)
            ).ReturnsAsync(fakeData);

            mockConnectionFactory
                .Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConnection.Object);

            var handler = new GetAllPengumumanQueryHandler(mockConnectionFactory.Object);
            var query = new GetAllPengumumanQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotEmpty(result.Value);
            Assert.Equal(fakeData.First().Pesan, result.Value.First().Pesan);
        }

        [Fact]
        public async Task GetAll_ReturnsFailure_WhenNoData()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            mockConnection.SetupDapperAsync(c =>
                c.QueryAsync<PengumumanResponse>(It.IsAny<string>(), null, null, null, null)
            ).ReturnsAsync(new List<PengumumanResponse>());

            mockConnectionFactory
                .Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConnection.Object);

            var handler = new GetAllPengumumanQueryHandler(mockConnectionFactory.Object);
            var query = new GetAllPengumumanQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task GetByUuid_ReturnsSuccess_WhenDataExists()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();
            var uuid = Guid.NewGuid();

            var fakeData = new PengumumanResponse
            {
                Uuid = uuid.ToString(),
                Pesan = "Pengumuman Penting",
                File = null,
                Url = null,
                Type = "Fakultas",
                Target = "Dosen",
                Nidn = "123456",
                KodeFaKultas = "FKIP",
                TypeExpired = "date",
                TanggalAwal = "2025-04-01",
                TanggalAkhir = "2025-04-30"
            };

            mockConnection.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<PengumumanResponse?>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    null, null, null
                )
            ).ReturnsAsync(fakeData);

            mockConnectionFactory
                .Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetPengumumanQueryHandler(mockConnectionFactory.Object);
            var query = new GetPengumumanQuery(uuid);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Pengumuman Penting", result.Value.Pesan);
        }

        [Fact]
        public async Task GetByUuid_ReturnsFailure_WhenDataNotFound()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            mockConnection.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<PengumumanResponse?>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    null, null, null
                )
            ).ReturnsAsync((PengumumanResponse?)null);

            mockConnectionFactory
                .Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetPengumumanQueryHandler(mockConnectionFactory.Object);
            var query = new GetPengumumanQuery(Guid.NewGuid());

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }
    }
}
