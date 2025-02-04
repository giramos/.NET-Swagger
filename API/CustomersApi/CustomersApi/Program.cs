var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(); // añade el controlador que hemos creado
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(routing => routing.LowercaseUrls = true); // minuscula en la ruta . De https://localhost:7163/api/Customer a https://localhost:7163/api/customer


var app = builder.Build(); //  construye una app

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) // swagger solo desarrollo
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// ESTO es lógica. No se recomienda ponerlo aqui
//app.MapGet("/customer/{id}", (long id) =>
//{
//    return "net 6";
//});

app.MapControllers();

app.Run();
