# Web Startup

For all supported web API technologies, a NuGet package is provided that depends on `Firestorm.Host`. Here in lies the main [configuration builder](configuration-builder.md), that'll allow you to configure Firestorm.

## ASP<span>.</span>NET Core

Firestorm provides Middleware for ASP<span>.</span>NET Core.

```
PM> Install-Package Firestorm.AspNetCore2
```

You can use the `UseFirestorm` extension method to add the Firestorm Middleware to your application. You will need to add services in your `ConfigureServices` method too.

```csharp
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddFirestorm()
            .AddEndpoints();
			// Other services omitted for brevity
    }
	
    public void Configure(IApplicationBuilder app)
    {
        app.UseFirestorm();
    }
}
```

## OWIN

Another package provides a different `UseFirestorm` extension method to setup OWIN Middleware.

This extension uses a parameter to configure the services instead.

```
PM> Install-Package Firestorm.Owin
```

```csharp
public class Startup
{
    public void Configure(IAppBuilder app)
    {
        app.UseFirestorm(c =>
        {
            c.AddEndpoints();
            // Other services omitted for brevity
        });
    }
}
```

## WebAPI 2.0

ASP<span>.</span>NET Web API 2.0 is also supported through a `FirestormController`.

You can apply the default route mapping with the `SetupFirestorm` extension method. This extension also uses a parameter to configure the services.

```
PM> Install-Package Firestorm.AspNetWebApi2
```

```csharp
public static class WebApiConfig
{
    public static void Register(HttpConfiguration config)
    {
        config.SetupFirestorm(c =>
        {
            c.AddEndpoints();
            // Other services omitted for brevity
        });
    }
}
```