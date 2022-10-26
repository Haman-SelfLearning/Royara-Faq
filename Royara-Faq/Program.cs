using DataAccess.Interfaces;
using DataAccess.Services;
using HealthChecks.UI.Client;
using HealthChecks.UI.Configuration;
using Infrastructure.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IConfiguration _configuration = builder.Configuration;

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(_configuration)
    .CreateLogger();
//builder.Host.UseSerilog((context, config) =>
//{
//    config.WriteTo
//        .Seq("https://localhost:5341")
//        .useogger();

//});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Context

builder.Services.AddDbContext<DataContext>(options=>
    //options.UseSqlServer("name=ConnectionStrings:Connection")
    options.UseNpgsql("name=ConnectionStrings:Connection")

    );
#endregion

#region Identity Service

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = _configuration["IdentityAddress"];
        options.Audience = "FaqAud";
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ManageFaq", policy =>
    
        policy.RequireClaim("scope","faqpolicy.manage"));
});

#endregion

#region IOC

builder.Services.AddTransient<IFaqService, FaqService>();

#endregion

#region Health Check

builder.Services.AddHealthChecks()
    //.AddCheck<DataBaseHealthCheck>("SQLCheck");
    .AddSqlServer(_configuration["ConnectionStrings:Connection"])
    .AddNpgSql(_configuration["ConnectionStrings:Connection"]);

builder.Services.AddHealthChecksUI(s =>
    s.AddHealthCheckEndpoint("FaqHealthCheck", "/health"))
    //.AddInMemoryStorage()
    .AddPostgreSqlStorage(_configuration["ConnectionStrings:Connection"]);



#endregion
var app = builder
    
    .Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHealthChecks("/health", new HealthCheckOptions()
{
    Predicate = _=> true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
});

app.UseHealthChecksUI(delegate (Options options)
{
    options.UIPath = "/healthui";
    options.ApiPath = "/healthuiapi";
});
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
//app.MapHealthChecks("/health");
app.Run();
