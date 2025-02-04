var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(); // a�ade el controlador que hemos creado
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build(); //  construye una app

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) // swagger solo desarrollo
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// ESTO es l�gica. No se recomienda ponerlo aqui
//app.MapGet("/customer/{id}", (long id) =>
//{
//    return "net 6";
//});

app.MapControllers();

app.Run();
