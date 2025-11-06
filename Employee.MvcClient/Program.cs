using Employee.MvcClient.Services;

namespace Employee.MvcClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var baseUrl = builder.Configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7224/";

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddHttpClient<EmployeeApiService>(client =>
            {
                client.BaseAddress = new Uri(baseUrl);
                client.Timeout = TimeSpan.FromSeconds(30);
            });

            builder.Services.AddHttpClient<DepartmentApiService>(client =>
            {
                client.BaseAddress = new Uri(baseUrl);
                client.Timeout = TimeSpan.FromSeconds(30);
            });

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
                pattern: "{controller=employees}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
