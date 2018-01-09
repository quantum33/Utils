# Utils
dot net utils

* add option pattern implementation v0.1

Use:
```csharp
    // 1st example:
    string shouldBeMyValue = Optional<string>
                                .Of(() => "my value")
                                .WhenSome()
                                .WhenNone(() => "no value")
                                .Map();
    shouldBeMyValue.Should().Be("my value");

    // 2nd:
    var shouldBeNoValue = Optional<string>.Of(() => null)
                                .WhenSome()
                                .WhenNone(() => "no value")
                                .Map();
    shouldBeNoValue.Should().Be("no value");
```

See unit test project for more example.
