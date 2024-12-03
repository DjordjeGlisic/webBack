using Microsoft.EntityFrameworkCore;
using Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<ProdavnicaContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("ProdavnicaCS")));
builder.Services.AddCors(options=>{
    options.AddPolicy("CORS",bilder=>{
        /*bilder.WithOrigins(new string[]{
            "https://localhost:5200",
            "http://localhost:5200",
            "https://127.0.0.1:5200",
            "http://127.0.0.1:52005200",
            
            
            
        })*/
        bilder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();

    });

});
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.UseCors("CORS");
app.UseHttpsRedirection();
app.Run();

