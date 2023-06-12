using CollegeProjectManagment.Core.Identity;
using CollegeProjectManagment.Core.ServiceContracts;
using CollegeProjectManagment.Core.Services;
using CollegeProjectManagment.DI;
using CollegeProjectManagment.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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

//Separate file to configure services
builder.Services.ConfigureServices();

//Identity

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.Password.RequiredLength = 5;
})
    .AddEntityFrameworkStores<RepositoryContext>()
    .AddDefaultTokenProviders()
    .AddUserStore<UserStore<ApplicationUser, ApplicationRole, RepositoryContext, Guid>>()
    .AddRoleStore<RoleStore<ApplicationRole, RepositoryContext, Guid>>();

builder.Services.AddTransient<IJwtService, JwtService>();

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