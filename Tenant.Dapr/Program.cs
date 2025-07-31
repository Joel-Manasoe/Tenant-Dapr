using Dapr.Actors;
using Dapr.Actors.Client;
using Tenant.Dapr.Actors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDaprClient();
builder.Services.AddHealthChecks();
builder.Services.AddActors(options =>
{
    options.Actors.RegisterActor<TenantActor>();
});

builder.Services.AddSingleton<IActorProxyFactory, ActorProxyFactory>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// add cors policy
builder.Services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
{
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
}));

var app = builder.Build();
//use cors
app.UseCors("CorsPolicy");

app.MapHealthChecks("/healthz");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapActorsHandlers();
app.Run();
