using Capstone_23_Proteine.Data;
using Capstone_23_Proteine.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using SendGrid;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var sendGridApiKey = builder.Configuration["SendGridKey"];



        // Set up the Azure Key Vault client
        string keyVaultName = "SendGridApiKey";
        string secretName = "SendGridKeyVault";

        var keyVaultUrl = $"https://{keyVaultName}.vault.azure.net/";
        var client = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());

        // Retrieve the SendGrid API key from the Key Vault
        KeyVaultSecret secret = client.GetSecret(secretName);
        string apiKey = secret.Value;

        // Use the API key to initialize the SendGridClient
        string host = "api.sendgrid.com"; // or the appropriate SendGrid API host
        var sendGridClient = new SendGridClient(apiKey, host);







        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        builder.Services.AddControllersWithViews();

        builder.Services.AddTransient<IEmailSender>(serviceProvider => new EmailSender(sendGridApiKey));

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
            pattern: "{controller=Home}/{action=Landing}/{id?}");

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
                user.EmailConfirmed= false;

                /*register user in database*/
                await userManager.CreateAsync(user, password);

                /*add user to specific role*/
                await userManager.AddToRoleAsync(user, "Admin");


            }


        }
        app.MapRazorPages();

        app.Run();
    }
}
