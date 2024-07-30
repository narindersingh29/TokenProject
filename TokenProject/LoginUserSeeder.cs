using Microsoft.EntityFrameworkCore;
using TokenProject.Entities;

namespace TokenProject
{
    public class LoginUserSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            await context.Database.EnsureCreatedAsync();

            if (!await context.LoginUsers.AnyAsync())
            {
                await context.LoginUsers.AddRangeAsync(
                    new LoginUser
                    {
                        UserName = "narinder@gmail.com",
                        Password = "ns1234"
                    },
                    new LoginUser
                    {
                        UserName = "sukhi@gmail.com",
                        Password = "sukh1234"
                    },
                    new LoginUser
                    {
                        UserName = "summer@gmail.com",
                        Password = "sam1234"
                    }
                );

                await context.SaveChangesAsync();
            }
        }
        }
    }

