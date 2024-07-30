using Microsoft.EntityFrameworkCore;
using TokenProject.Entities;
namespace TokenProject
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<LoginModel> LoginModels { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<LoginUser> LoginUsers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<UserRole>()
           .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
               .HasOne(ur => ur.User)
               .WithMany(u => u.UserRoles)
               .HasForeignKey(ur => ur.UserId)
               .OnDelete(DeleteBehavior.Restrict); // Add this line

            modelBuilder.Entity<UserRole>()
               .HasOne(ur => ur.Role)
               .WithMany(r => r.UserRoles)
               .HasForeignKey(ur => ur.RoleId)
               .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<LoginModel>().HasData(
            new LoginModel
            {
                Id = 1,
                Email = "narinder@gmail.com",
                Password = "ns1234"
            },
             new LoginModel
             {
                 Id = 2,
                 Email = "sukhi@gmail.com",
                 Password = "sukh1234"
             },
              new LoginModel
              {
                  Id = 3,
                  Email = "summer@gmail.com",
                  Password = "sam1234"
              }
            );
            modelBuilder.Entity<Role>().HasData(
              new Role
              {
                  Id = 1,
                  Name = "Super Admin",
              },
              new Role
              {
                  Id = 2,
                  Name = "Admin",
              },
              new Role
              {
                  Id = 3,
                  Name = "Manager",
              }
              );

            modelBuilder.Entity<LoginUser>()
                .HasKey(lu => lu.Id);

            modelBuilder.Entity<LoginUser>()
                .Property(lu => lu.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
