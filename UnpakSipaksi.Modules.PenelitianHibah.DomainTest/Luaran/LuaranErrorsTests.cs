using System;
using FluentAssertions;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.Luaran;
using Xunit;

namespace UnpakSipaksi.Modules.PenelitianHibah.DomainTest.Luaran
{
    public class LuaranErrorsTests
    {
        [Fact]
        public void EmptyData_ShouldReturnExpectedError()
        {
            // Act
            var error = LuaranErrors.EmptyData();

            // Assert
            error.Code.Should().Be("Luaran.EmptyData");
            error.Description.Should().Contain("data is not found");
        }

        [Fact]
        public void NotFound_ShouldReturnExpectedError()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var error = LuaranErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("Luaran.NotFound");
            error.Description.Should().Contain(id.ToString());
        }

        [Fact]
        public void NotFoundHibah_ShouldReturnExpectedError()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var error = LuaranErrors.NotFoundHibah(id);

            // Assert
            error.Code.Should().Be("Luaran.NotFoundHibah");
            error.Description.Should().Contain(id.ToString());
        }

        [Fact]
        public void InvalidData_ShouldReturnExpectedError()
        {
            // Act
            var error = LuaranErrors.InvalidData();

            // Assert
            error.Code.Should().Be("Luaran.InvalidData");
            error.Description.Should().Contain("not match");
        }
    }
}
