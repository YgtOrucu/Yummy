using FluentValidation;
using FluentValidation.AspNetCore;
using Yummy.WebUI.Validator.MarkerValidationRules;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient("YummyClient", client =>
{
    var baseUrl = builder.Configuration.GetSection("ApiSettings")["BaseUrl"];
    if (string.IsNullOrEmpty(baseUrl))
        throw new Exception("HATA: appsettings içindeki BaseUrl okunamadý!");

    client.BaseAddress = new Uri(baseUrl);
});
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<MarkerValidation>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Category}/{action=CategoryList}/{id?}");

app.Run();
