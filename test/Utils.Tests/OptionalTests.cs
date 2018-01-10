using System;
using FluentAssertions;
using System.Diagnostics;
using Utils.Interface;
using Xunit;

namespace Utils.Tests
{
    public class OptionalTests
    {
        [Fact]
        public void SomeMap_Success()
        {
            var shouldBeHello = Optional.Some("Hello").Map();
            shouldBeHello.Should().Be("Hello");
            
            int shouldBe200 = Optional.Some(200).Map();
            shouldBe200.Should().Be(200);
        }
        
        [Fact]
        public void WhenSome_ValidPredicate_WhenSome()
        {
            string result = Optional
                                .Some("a very long value to test")
                                .WhenSome(s => s.Length > 10)
                                .WhenNone(() => "value too short")
                                .Map();
            result.Should().Be("a very long value to test");
        }

        [Fact]
        public void WhenSome_NonValidPredicate_None()
        {
            string result = Optional
                                .Some("short")
                                .WhenSome(s => s.Length > 10)
                                .WhenNone(() => "value too long")
                                .Map();
            result.Should().Be("value too long");
        }

        [Fact]
        public void WhenSome_ValidPredicate_Success()
        {
            int shouldBe10 = Optional
                                .Some(10)
                                .WhenSome(s => s > 3)
                                .WhenNone(() => 138)
                                .Map();
            shouldBe10.Should().Be(10);

            int shouldBe138 = Optional
                                .Some(10)
                                .WhenSome(s => s > 203)
                                .WhenNone(() => 138)
                                .Map();
            shouldBe138.Should().Be(138);
        }
        [Fact]
        public void SomeAndNone_Success()
        {
            // Assert:
            string shouldBeMyValue = Optional
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
            IOptional<string> shouldBeNone = Optional
                                                .Of<string>(() => null)
                                                .WhenSome(() => shouldBeFalse = true)
                                                .WhenNone(() => "no value");
            shouldBeFalse.Should().BeFalse();
            shouldBeNone.ShouldBeEquivalentTo(Optional<string>.None());
        }

        [Fact]
        public void OptionalOf_ValidValue_WhenSome()
        {
            string shouldBeMyValue = Optional
                                        .Of(() => "my value")
                                        .WhenNone(() => "no value")
                                        .Map();
            shouldBeMyValue.Should().Be("my value");
        }

        [Fact]
        public void OptionalOf_Null_WhenNone()
        {
            var shouldBeNoValue = Optional
                                    .Of<string>(() => null)
                                    .WhenNone(() => "no value")
                                    .Map();

            shouldBeNoValue.Should().Be("no value");
        }
        
        [Fact]
        public void OptionalOf__withNullFunc_WhenNoneNotSet__mustReturnsDefaultT()
        {
            var shouldBeDefaultT = Optional
                                    .Of<string>(() => null)
                                    .Map();

            //TODO: fix this issue => our goal is to never return null so...
            shouldBeDefaultT.Should().Be(default(string));
        }

        [Fact]
        public void WhenSome_NullFunc_ThrowsArgumentNullException()
        {
            var assertAction = new Action(() =>
            {
                var option = Optional
                                .Some("my value")
                                .WhenSome(null);
            });
            assertAction.ShouldThrow<ArgumentNullException>();
        }
        
        [Fact]
        public void WhenNone_NullFunc_ThrowsArgumentNullException()
        {
            var assertAction = new Action(() =>
            {
                var option = Optional
                                .Some("my value")
                                .WhenNone(null);
            });
            assertAction.ShouldThrow<ArgumentNullException>();
        }
    }
}
