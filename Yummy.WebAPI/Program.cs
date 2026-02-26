using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using Yummy.WebAPI.Context;
using Yummy.WebAPI.Validator.MarkerValidationRules;
using Yummy.WebAPI.Entities;
using Yummy.WebAPI.Model;
using Microsoft.AspNetCore.Identity;
using Yummy.WebAPI.Configurations;
using Yummy.WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<YummyContext>(option => option.UseSqlServer(connectionString));

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<MarkerValidation>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{
    opt.Password.RequiredLength = 6;
    opt.Password.RequireNonAlphanumeric = false;
    opt.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<YummyContext>()
.AddErrorDescriber<TurkishIdentityErrorDescriber>()
.AddDefaultTokenProviders();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddScoped<IMailService, MailService>();

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
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
