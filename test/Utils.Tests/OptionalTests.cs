using FluentAssertions;
using System.Diagnostics;
using Xunit;

namespace Utils.Tests
{
    public class OptionalTests
    {
        [Fact]
        public void SomeAndNone_Success()
        {
            // Assert:
            string shouldBeMyValue = Optional<string>
                                        .Some("my value")
                                        .WhenSome(() => Debug.WriteLine("I can do some stuff"))
                                        .WhenNone(() => "no value")
                                        .Map();
            shouldBeMyValue.Should().Be("my value");

            string shouldNoValue = Optional<string>
                                        .None()
                                        .WhenSome(() => Debug.WriteLine("You shouldn't see this"))
                                        .WhenNone(() => "no value")
                                        .Map();
            shouldNoValue.Should().Be("no value");
        }

        [Fact]
        public void OptionalOf_NullValue_None()
        {
            var shouldBeFalse = false;
            IOptional<string> shouldBeNone = Optional<string>
                                                    .Of(() => null)
                                                    .WhenSome(() => shouldBeFalse = true)
                                                    .WhenNone(() => "no value");
            shouldBeFalse.Should().BeFalse();
            shouldBeNone.ShouldBeEquivalentTo(Optional<string>.None());
        }

        [Fact]
        public void OptionalOf_ValidValue_WhenSome()
        {
            string shouldBeMyValue = Optional<string>
                                        .Of(() => "my value")
                                        .WhenSome()
                                        .WhenNone(() => "no value")
                                        .Map();
            shouldBeMyValue.Should().Be("my value");
        }

        [Fact]
        public void OptionalOf_Null_WhenNone()
        {
            var result = Optional<string>.Of(() => null)
                                         .WhenSome()
                                         .WhenNone(() => "no value")
                                         .Map();

            result.Should().Be("no value");
        }

        [Fact]
        public void OptionalOf_WhenSome()
        {
            string result = Optional<string>
                                .Of(() => "my value")
                                .WhenSome()
                                .WhenNone(() => "no value")
                                .Map();
            result.Should().Be("my value");
        }
        
        [Fact]
        public void WhenSome_ValidPredicate_WhenSome()
        {
            string result = Optional<string>
                                .Of(() => "my value")
                                .WhenSome(s => s.Length > 3)
                                .WhenNone(() => "no value")
                                .Map();
            result.Should().Be("my value");
        }

        [Fact]
        public void WhenSome_NonValidPredicate_None()
        {
            string result = Optional<string>
                                .Of(() => "my")
                                .WhenSome(s => s.Length > 3)
                                .WhenNone(() => "no value")
                                .Map();
            result.Should().Be("no value");
        }

        [Fact]
        public void OptionalOf_WhenSomeIntPredicate_None()
        {
            int shouldBe10 = Optional<int>
                                .Of(() => 10)
                                .WhenSome(s => s > 3)
                                .WhenNone(() => 138)
                                .Map();
            shouldBe10.Should().Be(10);

            int shouldBe138 = Optional<int>
                                .Of(() => 10)
                                .WhenSome(s => s > 203)
                                .WhenNone(() => 138)
                                .Map();
            shouldBe138.Should().Be(138);
        }
    }
}
