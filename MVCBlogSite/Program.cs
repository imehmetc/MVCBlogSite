using BLL.AbstractServices;
using BLL.ConcreteServices;
using DAL.AbstractRepositories;
using DAL.ConcreteRepositories;
using DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace MVCBlogSite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            
            builder.Services.AddSession(); // session için gerekli.

            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); // 
            builder.Services.AddScoped(typeof(IUserService), typeof(UserService)); // 
            builder.Services.AddScoped(typeof(IPostService), typeof(PostService)); // 
            builder.Services.AddScoped(typeof(IPostLikeService), typeof(PostLikeService)); // 

            builder.Services.AddAutoMapper(typeof(MVCBlogSite.Mappings.AutoMapperProfile), typeof(BLL.Mappings.AutoMapperProfile));

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
            
            app.UseSession(); // session için gerekli
            
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
