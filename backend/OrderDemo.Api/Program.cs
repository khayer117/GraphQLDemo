using Microsoft.EntityFrameworkCore;
using OrderDemo.Api;
using OrderDemo.Api.Data;
using OrderDemo.Api.GraphQL;
using OrderDemo.Api.GraphQL.Types;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite("Data Source=orders.db"));

builder.Services.AddCors(opt =>
    opt.AddDefaultPolicy(policy =>
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()));

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddType<OrderType>()
    .AddType<OrderItemType>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
    SeedData.Initialize(db);
}

app.UseCors();
app.MapGraphQL();

app.Run("http://localhost:5080");
