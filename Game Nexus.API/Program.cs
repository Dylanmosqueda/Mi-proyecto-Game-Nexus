using Game_Nexus.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Registrar los servicios para inyección de dependencias
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IReviewService, ReviewService>();

var app = builder.Build();


// app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();