# Utils
dot net utils

* add option pattern implementation v0.1

Use:
```csharp
    1. var someString = Optional.Some("Hello").Map();
    // someString => "Hello"
            
    2. int someInt = Optional.Some(200).Map();
    // someInt => 200

    3. string shouldBeMyValue = Optional
                                .Of(() => "my value")
                                .WhenNone(() => "no value")
                                .Map();
    // shouldBeMyValue => "my value"

    4. var shouldBeNoValue = Optional
                                .Of<string>(() => null)
                                .WhenNone(() => "no value")
                                .Map();
    // shouldBeNoValue => "no value"
```

See unit test project for more example.
