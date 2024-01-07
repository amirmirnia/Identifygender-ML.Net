using MLNameClass;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:3000")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
builder.Services.AddControllers();
builder.Services.AddNamePredictEnginPool();
builder.Services.AddSingleton<Name>();
var app = builder.Build();
// Configure the HTTP request pipeline.


app.UseCors("MyPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
