using FluentAssertions.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StoreSystem.Business;
using StoreSystem.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var conString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"])),
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

builder.Services.AddDbContext<StoreDbContext>(options => options.UseSqlServer(conString));
builder.Services.AddScoped<UserManager>();
builder.Services.AddScoped<SalesManager>();
builder.Services.AddScoped<SaleManager>();
builder.Services.AddScoped<MobileManager>();
builder.Services.AddScoped<BulkManager>();
builder.Services.AddScoped<BrandManager>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler(c => c.Run(async context =>
{
    var exception = context.Features.Get<IExceptionHandlerPathFeature>().Error;
    string RequestURL = context.Request.Host.Value;
    RequestURL = RequestURL + "" + context.Request.Path;

    var response = new
    {
        ResponseCode = "500",
        Message = "error",
        Status = "false",
        ErrorMessage = exception.Message != null ? exception.Message : "",
        Data = exception.StackTrace != null ? exception.StackTrace : "",
    };
    await context.Response.WriteAsJsonAsync(response);
}));

app.Run();
