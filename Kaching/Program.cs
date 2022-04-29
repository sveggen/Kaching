using Kaching.Data;
using Kaching.Models;
using Kaching.Repositories;
using Kaching.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add user services to the container.
var userConnectionString = builder.Configuration.GetConnectionString("Identity");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(userConnectionString));


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
        options.User.RequireUniqueEmail = true;
        options.Password.RequireDigit = false;
        options.Password.RequireUppercase = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
builder.Services.AddScoped<IExpenseEventRepository, ExpenseEventRepository>();

builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();

builder.Services
    .AddFluentEmail(builder.Configuration["Mailgun:Sender"])
    .AddMailGunSender(
        builder.Configuration["Mailgun:Domain"],
        builder.Configuration["Mailgun:API"],
        FluentEmail.Mailgun.MailGunRegion.USA);

// Add other db services to the container.
var connectionString = builder.Configuration.GetConnectionString("Kaching");
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
    pattern: "{controller=Expenses}/{action=Index}/{month?}");
app.MapRazorPages();

app.Run();
