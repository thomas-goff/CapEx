using CapEx.Components;
using CapEx.Data;
using CapEx.Repositories;
using CapEx.Services.Approvals;
using CapEx.Services.Authentication;
using CapEx.Services.Dashboard;
using CapEx.Services.Requests;
using CapEx.Services.Security;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "Connection string 'DefaultConnection' is missing from appsettings.json.");

builder.Services.AddDbContextFactory<CapExDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRequestRepository, RequestRepository>();
builder.Services.AddScoped<IApprovalRepository, ApprovalRepository>();

builder.Services.AddSingleton<IApprovalTierPolicy, AmountBasedApprovalTierPolicy>();
builder.Services.AddScoped<IRequestService, RequestService>();

builder.Services.AddSingleton<IApprovalWorkflow, ApprovalWorkflow>();
builder.Services.AddScoped<IApprovalService, ApprovalService>();

builder.Services.AddSingleton<IDashboardMetricsCalculator, DashboardMetricsCalculator>();

builder.Services.AddSingleton<IPasswordVerifier, PlainTextPasswordVerifier>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
