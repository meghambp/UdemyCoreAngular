using System.Text;
using API.Data;
using API.Extensions;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(
    options =>
        {
            options.AddPolicy("Policy1",
                builder =>
                {
                    builder.WithOrigins("http://localhost:4200/")
                    .AllowAnyHeader()
                    .AllowAnyOrigin();
                });

});
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "API", Version = "v1" });
});
builder.Services.AddApplicationServices(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
    app.UseDeveloperExceptionPage();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors();
/*x => 
    { 
        /*x
        .AllowAnyHeader()
        .AllowAnyMethod()
        .WithOrigins("http://localhost:4200/");
        x.AddPolicy("Policy1",
                builder =>
                {
                    builder.WithOrigins("http://example.com",
                                        "http://www.contoso.com");
                });
    });*/
    

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
//app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
app.Run();

