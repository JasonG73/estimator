using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Estimator.Client.Pages;
using Estimator.Components;
using Estimator.Components.Account;
using Estimator.Data;
using Estimator.Data.TenantExtention;
using Estimator.Data.Tenant;
using Estimator.Components.PriceAndSize;
using Microsoft.AspNetCore.Authentication;
using Estimator.Components.PriceAndSize.Footing;
using Estimator.Components.PriceAndSize.Pad;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLocalization();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddAuthenticationStateSerialization();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

//builder.Services.AddDbContextFactory<TenantAreaDbContext>(
// options => options.UseSqlite(connectionString), ServiceLifetime.Scoped);

builder.Services.AddDbContext<TenantAreaDbContext>(options => options.UseSqlite(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

builder.Services.AddScoped<TenantIdProvider>();
builder.Services.AddScoped<PriceAndSizeService>();
builder.Services.AddScoped<PadPriceAndSizeService>();
builder.Services.AddScoped<MetricFootingReinforcingService>();
builder.Services.AddScoped<IClaimsTransformation, CustomClaimsTransformation>();
builder.Services.AddScoped<IMessageWriter, LoggingMessageWriter>();


var app = builder.Build();

using var scope = app.Services.CreateScope();

var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

appContext.Database.Migrate();

var tenantContext = scope.ServiceProvider.GetRequiredService<TenantAreaDbContext>();
tenantContext.Database.Migrate();


string[] supportedCultures = ["en-US", "fr"];
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Estimator.Client._Imports).Assembly);

//app.UseMiddleware<TenantMiddleware>();
//app.UseMyCustomMiddleware();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();



app.Run();
