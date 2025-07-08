using FluentValidation;
using MatchDay.RESTApi.DatabaseLayer;
using MatchDay.RESTApi.DatabaseLayer.Interfaces;
using MatchDay.RESTApi.ServiceLayer;
using MatchDay.RESTApi.ServiceLayer.Interfaces;
using MatchDay.RESTApi.WebLayer.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add validation
builder.Services.AddValidatorsFromAssembly(typeof(CreateTeamDtoValidator).Assembly, includeInternalTypes: true);

// Add services to the container.
builder.Services.AddScoped<IMatchDayService, MatchDayService>();
builder.Services.AddScoped<IMatchDayRepository, MatchDayRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
