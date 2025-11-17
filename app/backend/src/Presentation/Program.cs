using HouseholdBudget.Core.Application;
using HouseholdBudget.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// DIコンテナにサービスを登録
builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// Swagger を有効化
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger UI を有効化
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
