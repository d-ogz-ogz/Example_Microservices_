using MassTransit;
using Org.BouncyCastle.Crypto.Agreement.Srp;
using SHARED;
using StockAPI.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMassTransit(configure =>
{
    configure.AddConsumer<StockCheckConsumer>();
    configure.AddConsumer<StockRollBackConsumer>();
    configure.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost");
        cfg.ReceiveEndpoint(RabbitMQConstants.QueueNames.StockCheck, e =>
        {
            e.ConfigureConsumer<StockCheckConsumer>(context);
        });
        cfg.ReceiveEndpoint(RabbitMQConstants.QueueNames.StockRollback, e =>
        {
            e.ConfigureConsumer<StockRollBackConsumer>(context);
        }
        );

    });
});
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
