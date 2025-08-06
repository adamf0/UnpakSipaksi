using FluentAssertions;
using Moq;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.MemberMahasiswa;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;

namespace UnpakSipaksi.Modules.PenelitianPkm.DomainTest.MemberMahasiswa
{
    public class MemberMahasiswaTests
    {
        private static Mock<IPenelitianPkmRepository> CreateMockRepo(bool hasUniqueData = true)
        {
            var mock = new Mock<IPenelitianPkmRepository>();
            mock.Setup(x => x.HasUniqueDataAsync(
                It.IsAny<Guid?>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>())
            ).ReturnsAsync(hasUniqueData);
            return mock;
        }

        private static void SetId<T>(T obj, int id)
        {
            typeof(T).GetProperty("Id")?.SetValue(obj, id);
        }

        private async Task<Domain.PenelitianPkm.PenelitianPkm> CreateValidHibah(int id = 1)
        {
            var repo = CreateMockRepo();
            var result = await Domain.PenelitianPkm.PenelitianPkm.Create(repo.Object, "123", "2024-01-01", "judul");
            result.IsSuccess.Should().BeTrue();

            var hibah = result.Value;
            SetId(hibah, id);
            return hibah;
        }

        [Fact]
        public void Create_ShouldReturnSuccess_WhenValid()
        {
            // Act
            var result = Domain.MemberMahasiswa.MemberMahasiswa.Create(0, 1, "123456789");

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.NPM.Should().Be("123456789");
            result.Value.PenelitianPkmId.Should().Be(1);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void Create_ShouldReturnFailed_WhenInvalid()
        {
            // Act
            var result = Domain.MemberMahasiswa.MemberMahasiswa.Create(0, 0, "12345678");

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("MemberMahasiswa.InvalidNpm");
            result.Error.Description.Should().Be("Npm is invalid format");
        }

        [Fact]
        public async void Update_ShouldChangeNPM_WhenValid()
        {
            // Arrange
            var hibah = await CreateValidHibah();
            var mahasiswa = Domain.MemberMahasiswa.MemberMahasiswa.Create(0, 1, "123456789").Value;

            // Act
            var result = Domain.MemberMahasiswa.MemberMahasiswa.Update(0, mahasiswa, hibah, "123456788");

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.NPM.Should().Be("123456788");
        }

        [Fact]
        public async void UpdateMbkm_ShouldFail_WhenEntityIsNull()
        {
            //arrange
            var hibah = await CreateValidHibah();

            //act
            var result = Domain.MemberMahasiswa.MemberMahasiswa.UpdateMbkm(null, hibah, "https://drive.google.com/file.pdf");

            //assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("PenelitianPkm.EmptyData");
        }

        [Theory]
        [InlineData("not-a-url")]
        [InlineData("ftp://drive.google.com/file.pdf")]
        [InlineData("http://drive.google.com/file.pdf")]
        public async void UpdateMbkm_ShouldFail_WhenUrlIsInvalid(string invalidUrl)
        {
            //arrange
            var hibah = await CreateValidHibah();
            var entity = Domain.MemberMahasiswa.MemberMahasiswa.Create(0, 1, "123456788").Value;

            //act
            var result = Domain.MemberMahasiswa.MemberMahasiswa.UpdateMbkm(entity, hibah, invalidUrl);

            //assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("MemberMahasiswa.InvalidUrlBuktiMbkm");
        }

        [Fact]
        public async void UpdateMbkm_ShouldFail_WhenHostIsNotGoogleDrive()
        {
            //arrange
            var hibah = await CreateValidHibah();
            var entity = Domain.MemberMahasiswa.MemberMahasiswa.Create(0, 1, "123456788").Value;

            //act
            var result = Domain.MemberMahasiswa.MemberMahasiswa.UpdateMbkm(entity, hibah, "https://example.com/file.pdf");

            //assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("MemberMahasiswa.InvalidHostBuktiMbkm");
        }

        [Fact]
        public async void UpdateMbkm_ShouldSucceed_WhenValidGoogleDriveUrl()
        {
            //arrange
            var hibah = await CreateValidHibah();
            var entity = Domain.MemberMahasiswa.MemberMahasiswa.Create(0, 1, "123456788").Value;

            //act
            var result = Domain.MemberMahasiswa.MemberMahasiswa.UpdateMbkm(entity, hibah, "https://drive.google.com/file.pdf");

            //assert
            result.IsSuccess.Should().BeTrue();
            result.Value.BuktiMbkm.Should().Be("https://drive.google.com/file.pdf");
        }
    }
}
