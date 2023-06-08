using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Capstone_23_Proteine.Data;
using Capstone_23_Proteine.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;


public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var services = builder.Services;

        // Retrieve SendGrid API key from Azure Vault
        string vaultUri = "https://proteinekeyvault.vault.azure.net/";
        string sendGridApiKeySecretName = "SendGridKey";

        var credential = new DefaultAzureCredential();
        var client = new SecretClient(new Uri(vaultUri), credential);

        KeyVaultSecret secret = client.GetSecret(sendGridApiKeySecretName);

        string sendGridApiKey = secret.Value;

        // Register the sendGridApiKey as a dependency
        services.AddSingleton(sendGridApiKey);

        // Retrieve the database connection string
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        // Add services to the container.
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        builder.Services.AddControllersWithViews();

        builder.Services.AddTransient<IEmailSender>(serviceProvider => new EmailSender(sendGridApiKey));

        builder.Services.AddCoreAdmin();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        /*----create Admin accounts----*/
        using (var scope = app.Services.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var roles = new[] { "Admin", "Manager", "Member" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        /*----User----*/
        using (var scope = app.Services.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            string email = "admin@admin.com";
            string password = "adminPass1!";

            /*find account*/
            if (await userManager.FindByEmailAsync(email) == null)
            {
                /*create account*/
                var user = new IdentityUser();
                user.UserName = email;
                user.Email = email;
                user.EmailConfirmed = false;

                /*register user in database*/
                await userManager.CreateAsync(user, password);

                /*add user to specific role*/
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });

        app.Use(async (context, next) =>
        {
            var signInManager = context.RequestServices.GetRequiredService<SignInManager<IdentityUser>>();

            if (signInManager.IsSignedIn(context.User))
            {
                await next.Invoke();
            }
            else
            {
                context.Response.Redirect("/Home/Landing");
            }
        });

        app.MapRazorPages();

        app.UseCoreAdminCustomUrl("adminpanel");

        app.Run();
    }
}
