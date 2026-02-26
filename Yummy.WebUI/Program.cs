using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Yummy.WebAPI.Services;
using Yummy.WebUI.Validator.MarkerValidationRules;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(config =>
{
    var polity = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();
    config.Filters.Add(new AuthorizeFilter(polity));
    config.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});
builder.Services.AddHttpClient("YummyClient", client =>
{
    var baseUrl = builder.Configuration.GetSection("ApiSettings")["BaseUrl"];
    if (string.IsNullOrEmpty(baseUrl))
        throw new Exception("HATA: appsettings içindeki BaseUrl okunamadý!");

    client.BaseAddress = new Uri(baseUrl);
});
builder.Services.AddHttpClient("OpenAIClient", client =>
{
    var baseUrl = builder.Configuration.GetSection("OpenAIAddress")["AIUrl"];
    if (string.IsNullOrEmpty(baseUrl))
        throw new Exception("HATA: appsettings içindeki AIUrl okunamadý!");

    client.BaseAddress = new Uri(baseUrl);
});
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<MarkerValidation>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Login/";
        options.LogoutPath = "/Login/Logout/";
        options.Cookie.Name = "YummyAuthCookie";
    });

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Register}/{action=Register}/{id?}");

app.Run();
