var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddDapr();
builder.Services.AddDaprClient();
builder.Services.AddHealthChecks();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapHealthChecks("/healthz");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseCloudEvents(); // for dar pub/sub
app.MapControllers(); // for pubsub to work
app.MapSubscribeHandler();
app.Run();
