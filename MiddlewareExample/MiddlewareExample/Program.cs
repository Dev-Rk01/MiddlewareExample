
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();


//app.Use(async (context, next) =>
//{
//	Console.WriteLine($"Logic before executing the next delegate in the Use method {1}");
//    await next.Invoke();
//    Console.WriteLine($"Logic after executing the next delegate in the Use method {2}");
//});

//app.Run(async (context) =>
//{
//    Console.WriteLine($"Writing the response to the client in the Run method {3}");
//    Console.WriteLine($"Hello from the middleware component {4}.");
//    await context.Response.WriteAsync($"Last");
//});


//app.Use(async (context, next) =>
//{
//	context.Response.StatusCode = 200;
//	await context.Response.WriteAsync($"Hello from the middleware component {1}.");
//	await next.Invoke();
//});

//app.Run(async context =>
//{
//	Console.WriteLine($"Writtin the response to the clien in the run method {2}");

//	await context.Response.WriteAsync($"Hello from the middleware component {3}.");
//});

app.Use(async (context, next) =>
{
	Console.WriteLine($"Logic before excuting the next delegate in the use method {1}");
	await next.Invoke();
	Console.WriteLine($"Logic after excuting the next delegate in the use method {2}");
});

app.Map("/usingmap", builder =>
{
	builder.Use(async (context, next) =>
	{
		Console.WriteLine($"Map branch logic in the use method before the next delegate {3}");
		await next.Invoke();
		Console.WriteLine($"map branch logic in the use method after the next delegate {4}");
		await context.Response.WriteAsync($"Hello from the map branch {5}");
	});


	//builder.Run(async context =>
	//{
	//	Console.WriteLine("Map branch logic in the run method");
	//	await context.Response.WriteAsync("Hello from the map branch");
	//});
});

app.MapWhen(context => context.Request.Query.ContainsKey("testquerystring"), builder =>
{
	builder.Use(async (context, next) =>
	{
		await next.Invoke();
		await context.Response.WriteAsync($"Hello from the MapWhen branch {6}");
	});
});

app.Run(async context =>
{
	Console.WriteLine($"Writing the response to the client in the run method {7}");
	await context.Response.WriteAsync($"Hello from the middleware component {8}");
});

app.MapControllers();


app.Run();
