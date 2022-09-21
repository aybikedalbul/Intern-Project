
using InternProject.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
var CorsPolicyName = "test";
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Context>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("LocationDbString")));
builder.Services.AddCors(options =>
{
    options.AddPolicy(CorsPolicyName,
        builder => builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()

        .SetIsOriginAllowed((Localhost) =>
        {
            return true;
        })
        );
});
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
var CorsPoicyName = app.UseCors();
app.UseAuthorization();

app.MapControllers();
app.Run();


