using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// 📦 Добавление сервисов
builder.Services.AddControllersWithViews();
builder.Services.AddInfrastructureServices(builder.Configuration); // <-- тут регистрируется ApplicationDbContext

builder.Configuration.AddUserSecrets<Program>();

// 🔐 Настройка аутентификации
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(options =>
{
    options.ClientId = "";
    options.ClientSecret = "";
    options.CallbackPath = "/signin-google";
});

var app = builder.Build();

// 🧨 Вызов сидера (и БД создаётся, и данные добавляются)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
}

// 📡 Обработка ошибок и HTTPS
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 🔐 Подключение аутентификации
app.UseAuthentication(); // <-- ОБЯЗАТЕЛЬНО перед UseAuthorization
app.UseAuthorization();

// 📍 Маршрутизация
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

