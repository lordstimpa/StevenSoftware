var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowSpecificOrigin", policy =>
	{
		policy.WithOrigins("https://stevensoftware.se", "https://localhost:60064")
			  .AllowAnyHeader()
			  .AllowAnyMethod();
	});
});

var app = builder.Build();
app.UseCors("AllowSpecificOrigin");

app.UseDefaultFiles();
app.MapStaticAssets();

app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
	c.SwaggerEndpoint("/swagger/v1/swagger.json", "StevenSoftware API V1");
});
app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
