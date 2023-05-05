using Microsoft.EntityFrameworkCore;
using InfosetPizza.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Cors Policy için ekledim. 30. Satýr dahil
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll",
        policy => {
            policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<InfosetPizzaDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("infosetConnectionString")));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
