using BankingSystem.Services.AccountService.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

var builder = WebApplication.CreateBuilder(args);

// Configuração de MongoDB
string mongoConnectionString = builder.Configuration.GetConnectionString("MongoDb");
string databaseName = builder.Configuration["MongoDbSettings:DatabaseName"];

// Mapeia classes para inversão de dependencia
builder.Services.AddSingleton<IAccountRepository>(sp => new AccountRepository(mongoConnectionString, databaseName));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
// }

//app.UseHttpsRedirection();
app.MapControllers();
app.Run();