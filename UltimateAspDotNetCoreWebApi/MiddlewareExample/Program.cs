using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.Use(async (context, next) =>
{
    Debug.WriteLine("Logic before executing the next delegate in the Use method");
    await next();
    Debug.WriteLine("Logic after executing the next delegate in the Use method");
});
app.Map("/usingmapbranch", builder =>
{
    builder.Use(async (context, next) =>
    {
        Debug.WriteLine("Map branch logic in the Use method before the next delegate");
        await next();
        Debug.WriteLine("Map branch logic in the Use method after the next delegate");
    });
    builder.Run(async context =>
    {
        Debug.WriteLine("Map branch response to the client in the Run method");
        await context.Response.WriteAsync("Hello from the Map branch.");
    });
});
app.MapWhen(context => context.Request.Query.ContainsKey("test"), builder =>
{
    builder.Run(async context =>
    {
        await context.Response.WriteAsync($"Hello from the MapWhen branch. test: {context.Request.Query["test"]}");
    });
});
app.Run(async context =>
{
    await context.Response.WriteAsync("Hello from the middleware component.");
});

app.MapControllers();

app.Run();
