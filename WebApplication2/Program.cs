using BLL.Iservices;
using BLL.Services;
using BLL;
using Dal_Repository.Irepository;
using Dal_Repository.models;
using Dal_Repository.Repository;
using Microsoft.AspNetCore.Authentication.Negotiate;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
   .AddNegotiate();

builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy.
    options.FallbackPolicy = options.DefaultPolicy;
});
builder.Services.AddControllers();
//הזרקת תלויות
builder.Services.AddScoped<Irepository<Appointment>, AppointmentsRepository>();
builder.Services.AddScoped<IAppointmentsService, AppointmentsService>();
//auto mapper:
builder.Services.AddAutoMapper(typeof(MyMapper));
//טיפול בהרשאות - נדרש עבור צד הלקוח- דף הגאוה סקריפט
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod()
      .AllowAnyHeader());
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
