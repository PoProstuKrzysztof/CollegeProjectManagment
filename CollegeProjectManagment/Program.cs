using CollegeProjectManagment.DI;
using CollegeProjectManagment.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//adding database connection
builder.Services.AddDbContextPool<RepositoryContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
sqlServerOptionsAction: sqlOptions =>
{
    sqlOptions.EnableRetryOnFailure();
    sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
}));
builder.Services.ConfigureAuthentication(builder);

builder.Services.ConfigureServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.SeedData();

app.Run();