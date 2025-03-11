using MassTransit;
using OrderAPI.Consumers;
using SHARED.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMassTransit(conf =>
{
    conf.AddConsumer<StockCheckResultConsumer>();
    conf.AddConsumer<NotificationCompletedConsumer>();
    conf.AddConsumer<PaymentNotCompletedConsumer>();

    conf.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost");

        cfg.ReceiveEndpoint("stock-check-queue", e =>
        {
            e.ConfigureConsumer<StockCheckResultConsumer>(context);
        });
        cfg.ReceiveEndpoint("payment-check-queue", e =>
        {
            e.ConfigureConsumer<PaymentNotCompletedConsumer>(context);
        });

        cfg.ReceiveEndpoint("notification-queue", e =>
        {
            e.ConfigureConsumer<NotificationCompletedConsumer>(context);
        });
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
