# Implicit Generics [![Nuget](https://img.shields.io/nuget/v/raiqub.implicitgenerics)](https://www.nuget.org/packages/raiqub.implicitgenerics)

_Provides a mechanism that allows generic type parameter to be inferred implicitly._

[🏃 Quickstart](#quickstart) &nbsp; | &nbsp; [📗 Guide](#guide) &nbsp; | &nbsp; [📦 NuGet](https://www.nuget.org/packages/raiqub.implicitgenerics)

<hr />

Sometimes when developing generic methods extensions it needs additional generic parameter types,
and when any of types can not be inferred then all generic parameter types must be specified.

This library brings ``IOutParam<out T>`` interface and ``OutParam`` class to help developers create method
parameters that lets the compiler infer the type of the generic parameter.

## Compatibility

Raiqub.ImplicitGenerics is currently compatible with the following frameworks:
* .NET Standard >= 1.0
* .NET 6.0

## Quickstart

1. **Reference the package** <br/>
   Add the package to your project, for example via:

   ```sh
   Install-Package Raiqub.ImplicitGenerics

   --or--

   dotnet add package Raiqub.ImplicitGenerics
   ```
2. **Add using** <br/>
   To use the types provided by this library add using on top of ``.cs`` file:
   ```sh
   using Raiqub.ImplicitGenerics
   ```
You should now be ready to use the library types on your project.

## Guide

To understand the scenarios covered by this library, some examples are given.

### Registering adapter on the dependency injector

Take an example of a method that registers an adapter:

```csharp
public static IServiceCollection AddAdapter<TIn, TOut, TAdapter>(
    this IServiceCollection services)
    where TAdapter : class, IAdapter<TIn, TOut> =>
    services.AddSingleton<IAdapter<TIn, TOut>, TAdapter>();
```

To call this method all three parameters must be specified:

```csharp
services.AddAdapter<int, float, Int32ToFloatAdapter>()
```

But, using the type provided by this library:

```csharp
public static IServiceCollection AddAdapter<TIn, TOut>(
    this IServiceCollection services,
    IOutParam<IAdapter<TIn, TOut>> outParam) =>
    services.AddSingleton(typeof(IAdapter<TIn, TOut>), outParam.Type);
```

Then the call is just:

```csharp
using Raiqub.ImplicitGenerics;

services.AddAdapter(OutParam.Of<Int32ToFloatAdapter>())
```

### Down casting dictionary values

Take another example of a method that down-cast the values of a dictionary:

```csharp
public static IDictionary<TKey, TOther> DownCastValues<TKey, TValue, TOther>(
    this IDictionary<TKey, TValue> dictionary)
    where TKey : notnull
    where TValue : class
    where TOther : class, TValue =>
    dictionary
        .Select(it => (it.Key, Value: (TOther)it.Value))
        .ToDictionary(it => it.Key, it => it.Value);
```

And to call this method:

```csharp
IDictionary<string, string> result = dict.DownCastValues<string, object, string>();
```

But, using the provided ``IOutParam<out T>`` interface:

```csharp
public static IDictionary<TKey, TOther> DownCastValues<TKey, TValue, TOther>(
    this IDictionary<TKey, TValue> dictionary,
    IOutParam<TOther> outParam)
    where TKey : notnull
    where TValue : class
    where TOther : class, TValue =>
    dictionary
        .Select(it => (it.Key, Value: (TOther)it.Value))
        .ToDictionary(it => it.Key, it => it.Value);
```

Finally, the call is:

```csharp
using Raiqub.ImplicitGenerics;

IDictionary<string, string> result = dict.DownCastValues(OutParam.Of<string>());
```

### More examples

You can find more examples going to the test project: [ParamTest.cs](./tests/Raiqub.ImplicitGenerics.Tests/ParamTest.cs).

## Contributing

If something is not working for you or if you think that the source file
should change, feel free to create an issue or Pull Request.
I will be happy to discuss and potentially integrate your ideas!

## License

See the [LICENSE](./LICENSE) file for details.
