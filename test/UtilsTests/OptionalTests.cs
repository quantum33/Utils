using System;
using FluentAssertions;
using FluentAssertions.Common;
using NUnit.Framework;
using Utils;

namespace UtilsTests
{
    [TestFixture]
    public class OptionalTests
    {
        [Test]
        public void Constructor_ValidValue_Success()
        {
            var option = new Optional<string>("my value");

            // Assert:
            option.HasValue.Should().BeTrue();
            option.Value.ShouldBeEquivalentTo("my value");
        }

        [Test]
        public void Constructor_NullValue_HasValueMustReturnFalse()
        {
            var option = new Optional<string>(null);

            // Assert:
            option.HasValue.Should().BeFalse();
        }

        [Test]
        public void Constructor_NoValue_HasValueMustReturnFalse()
        {
            var option = new Optional<string>();

            // Assert:
            option.HasValue.Should().BeFalse();
            Action action = () => { var value = option.Value; };
            action
                .ShouldThrow<InvalidOperationException>()
                .WithMessage("Use the 'HasValue' property before using the 'Value' property");
        }

        [Test]
        public void Equals_ValidValues_Success()
        {
            var optionA = new Optional<string>("my value");
            var optionB = new Optional<string>("my value");

            // Assert:
            optionA.IsSameOrEqualTo(optionB);
            optionA.ShouldBeEquivalentTo(optionB);
        }

        [Test]
        public void Equals_DifferentValidValues_MustFail()
        {
            var optionA = new Optional<string>("my value");
            var optionB = new Optional<string>("any other value");

            // Assert:
            optionA.Should().NotBeSameAs(optionB);
        }

        [Test]
        public void Equals_DifferentTypes_MustFail()
        {
            var optionA = new Optional<string>("my value");
            var optionB = new Optional<object>("my value");

            // Assert:
            optionA.Should().NotBeSameAs(optionB);
        }
    }
}
