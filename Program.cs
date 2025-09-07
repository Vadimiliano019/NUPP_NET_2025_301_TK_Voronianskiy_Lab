using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Transport API", Version = "v1" });
});

// Ðו÷סענאצ³ÿ סונג³ס³ג, ÿךשמ ןמענ³בםמ
// builder.Services.AddSingleton<ICrudServiceAsync<BusModel>, BusService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Transport API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();  // ÎÁÎÂ'‗ÇÊÎÂÎ!!!
