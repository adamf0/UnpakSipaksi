using FluentAssertions;
using Moq;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.DomainTest.MemberDosen
{
    public class MemberDosenTests
    {
        private static Mock<IPenelitianHibahRepository> CreateMockRepo(bool hasUniqueData = true)
        {
            var mock = new Mock<IPenelitianHibahRepository>();
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
        private async Task<Domain.PenelitianHibah.PenelitianHibah> CreateValidHibah(int id = 1)
        {
            var repo = CreateMockRepo();
            var result = await Domain.PenelitianHibah.PenelitianHibah.Create(repo.Object, "123", "2024-01-01", "judul");
            result.IsSuccess.Should().BeTrue();

            var hibah = result.Value;
            SetId(hibah, id);
            return hibah;
        }

        [Fact]
        public void Create_ShouldReturnSuccess_WhenValid()
        {
            // Act
            var result = Domain.MemberDosen.MemberDosen.Create(0, 1, "123");

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.PenelitianHibahId.Should().Be(1);
            result.Value.NIDN.Should().Be("123");
            result.Value.Status.Should().Be(0); // Default status
        }

        [Fact]
        public async void Update_ShouldReturnSuccess_WhenValid()
        {
            // Arrange
            var original = Domain.MemberDosen.MemberDosen.Create(0, 1, "123").Value;
            var hibah = await CreateValidHibah();

            // Act
            var result = Domain.MemberDosen.MemberDosen.Update(0, original, hibah, "456");

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.NIDN.Should().Be("456");
        }

        [Fact]
        public async void Approve_ShouldUpdateStatusToApproved()
        {
            // Arrange
            var member = Domain.MemberDosen.MemberDosen.Create(0, 1, "123").Value;
            var hibah = await CreateValidHibah();

            // Act
            var result = Domain.MemberDosen.MemberDosen.Approve(member, hibah);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Status.Should().Be(1);
        }

        [Fact]
        public async void Approve_ShouldReturnFailure_WhenNull()
        {
            // Act
            var hibah = await CreateValidHibah();
            var result = Domain.MemberDosen.MemberDosen.Approve(null, hibah);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("PenelitianHibah.EmptyData");
        }

        [Fact]
        public async void Reject_ShouldUpdateStatusToRejected()
        {
            // Arrange
            var member = Domain.MemberDosen.MemberDosen.Create(0, 1, "123").Value;
            var hibah = await CreateValidHibah();

            // Act
            var result = Domain.MemberDosen.MemberDosen.Reject(member, hibah);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Status.Should().Be(-1);
        }

        [Fact]
        public async void Reject_ShouldReturnFailure_WhenNull()
        {
            // Act
            var hibah = await CreateValidHibah();
            var result = Domain.MemberDosen.MemberDosen.Reject(null, hibah);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("PenelitianHibah.EmptyData");
        }
    }
}
