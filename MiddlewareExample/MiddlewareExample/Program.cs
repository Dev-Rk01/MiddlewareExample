var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();


app.Use(async (context, next) =>
{
	Console.WriteLine($"Logic before executing the next delegate in the Use method {1}");
    await next.Invoke();
    Console.WriteLine($"Logic after executing the next delegate in the Use method {2}");
});

app.Run(async (context) =>
{
    Console.WriteLine($"Writing the response to the client in the Run method {3}");
    Console.WriteLine($"Hello from the middleware component {4}.");
    await context.Response.WriteAsync($"Last");
});


app.MapControllers();


app.Run();
