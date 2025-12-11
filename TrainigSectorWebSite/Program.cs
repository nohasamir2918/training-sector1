using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TrainigSectorDataEntry.DataContext;
using TrainigSectorDataEntry.DepandecyInjection;
using TrainigSectorDataEntry.Interface;
using TrainigSectorDataEntry.Logging;
using TrainigSectorDataEntry.Models;
using TrainigSectorDataEntry.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddRepositories();
builder.Services.AddDbContext<TrainingSectorDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();
// Register the logger repository
builder.Services.AddScoped<ILoggerRepository, LoggerRepository>();

//Mapping
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddHttpContextAccessor();
var app = builder.Build();

var supportedCultures = new[]
{
    new CultureInfo("ar"),
    new CultureInfo("en")
};

var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("ar"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};

// ✅ نجعل اللغة الافتراضية من الكوكي فقط أو من الرابط
localizationOptions.RequestCultureProviders.Clear();
localizationOptions.RequestCultureProviders.Add(new CookieRequestCultureProvider());
localizationOptions.RequestCultureProviders.Add(new QueryStringRequestCultureProvider());

app.UseRequestLocalization(localizationOptions);

// ✅ تأكيد اللغة الافتراضية في الـ Thread
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("ar");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("ar");

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}",
new[] { "TrainigSectorWebSite.Controllers" }
);
app.Run();


