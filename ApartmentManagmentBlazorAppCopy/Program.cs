using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ApartmentManagmentBlazorAppCopy.Components;
using ApartmentManagmentBlazorAppCopy.Components.Account;
using ApartmentManagmentBlazorAppCopy.Data;
using ApartmentManagmentBlazorAppCopy;
using ApartmentManagmentBlazorAppCopy.Services;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

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

builder.Services.AddDbContext<ApartmentsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApartmentsContext")));

builder.Services.AddDbContext<UserContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApartmentsContext")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<UserContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

//builder.Services.AddScoped<ITokenServices, TokenServices>();
builder.Services.AddScoped<IMeterServices, MeterServices>();
builder.Services.AddScoped<IChoresService, ChoresServices>();
builder.Services.AddScoped<IApartmentServices, ApartmentServices>();
//builder.Services.AddScoped<IAuthServices, AuthServices>();
builder.Services.AddScoped<IExploitationCostsServices, ExploitationCostsServices>();
builder.Services.AddScoped<IPurchaseServices, PurchaseServices>();
builder.Services.AddScoped<IRentServices, RentServices>();
builder.Services.AddScoped<IRommateServices, RommateServices>();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

builder.Services.AddBlazorBootstrap();
builder.Services
    .AddBlazorise( options =>
    {
        options.Immediate = true;
    } )
    .AddBootstrapProviders()
    .AddFontAwesomeIcons();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.AddApartmentEnpoints();
app.AddRentEndpoints();
app.AddRoomateEndpoints();
app.AddExploitationCostsEndpoints();
app.AddMeterEndpoints();
app.AddPurchaseEndpoints();

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();