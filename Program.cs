using API.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Registrar servicios usando los métodos de extensión
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
	throw new InvalidOperationException("Connection string 'DefaultConnection' is not found.");
}

builder.Services.AddDatabase(connectionString);
builder.Services.AddRepositories();
builder.Services.AddApplicationServices();

builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
