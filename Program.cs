using Sempi5.Domain.Shared;
using Sempi5.Infrastructure.Shared;
using Sempi5.Infrastructure.TodoItemRepository;
using IDatabase = Sempi5.Infrastructure.Databases.IDatabase;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using Sempi5.Infrastructure.StaffRepository;
using Sempi5.Domain.User;
using Sempi5.Domain.Staff;
using Sempi5.Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;
using Sempi5.Services;
using Sempi5.Infrastructure.UserRepository;
namespace Sempi5
{

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureMyServices(builder.Services);

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Staff",
                policy => policy.RequireRole("Doctor", "Nurse", "Admin", "Technician"));

                options.AddPolicy("Patient",
                policy => policy.RequireRole("Patient"));
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
            .AddCookie("LocalCookie", options =>
            {
                options.LoginPath = "/Login/local";
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            })
            .AddCookie(options =>
            {
                options.LoginPath = "/Login/login";
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            })
            .AddGoogle(options =>
            {
                options.ClientId = builder.Configuration["Google:ClientId"];
                options.ClientSecret = builder.Configuration["Google:Client_Secret"];
                options.SaveTokens = true;
            });

            builder.Services.AddControllersWithViews();

            CreateDataBase(builder);

            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();


            try
            {
                SeedData(app.Services);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Error seeding data");
            }

            app.Run();

        }

        public static void CreateDataBase(WebApplicationBuilder builder)
        {

            string name = "Sempi5.Infrastructure.Databases." + builder.Configuration["DataBase:Type"];
            Type? dbType = Type.GetType(name);

            if (dbType == null)
            {
                Console.WriteLine("Database Type Invalid. Please check the configuration file!\nApplication will exit");
                Environment.Exit(2);
            }

            try
            {
                ((IDatabase)Activator.CreateInstance(dbType)).connectDB(builder);
            }
            catch (Exception)
            {
                Console.WriteLine("Database not found\nApplication will exit");
                Environment.Exit(3);
            }

        }

        public static void ConfigureMyServices(IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<ITodoItemRepository, TodoItemRepository>();
            services.AddTransient<IStaffRepository, StaffRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ManageStaffService>();
        }

        public static void SeedData(IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var staffRepo = scope.ServiceProvider.GetRequiredService<IStaffRepository>();
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                // Check if there are any staff members already in the database
                if (!staffRepo.GetAllAsync().Result.Any())
                {
                    // Create the system users for staff
                    var doctorUser = new SystemUser("doctor@example.com", "Doctor");
                    var nurseUser = new SystemUser("nurse@example.com", "Nurse");
                    var adminUser = new SystemUser("admin@example.com", "Admin");

                    // Create staff members
                    var doctor = new Staff
                    {
                        FirstName = "John",
                        LastName = "Doe",
                        FullName = "Dr. John Doe",
                        LicenseNumber = "DR12345",
                        Specialization = "Cardiology",
                        ContactInfo = "doctor@example.com",
                        AvailabilitySlots = new List<string> { "Monday 9am-12pm", "Wednesday 1pm-4pm" },
                        Password = "DoctorPassword123", // Consider storing hashed passwords
                        User = doctorUser
                    };

                    var nurse = new Staff
                    {
                        FirstName = "Jane",
                        LastName = "Smith",
                        FullName = "Nurse Jane Smith",
                        LicenseNumber = "NR67890",
                        Specialization = "General",
                        ContactInfo = "nurse@example.com",
                        AvailabilitySlots = new List<string> { "Tuesday 10am-3pm", "Thursday 9am-12pm" },
                        Password = "NursePassword123",
                        User = nurseUser
                    };

                    var admin = new Staff
                    {
                        FirstName = "Alice",
                        LastName = "Johnson",
                        FullName = "Admin Alice Johnson",
                        LicenseNumber = "AD54321",
                        Specialization = "Administration",
                        ContactInfo = "admin@example.com",
                        AvailabilitySlots = new List<string> { "Monday-Friday 9am-5pm" },
                        Password = "AdminPassword123",
                        User = adminUser
                    };

                    // Add staff to repository
                    staffRepo.AddAsync(doctor).Wait();
                    staffRepo.AddAsync(nurse).Wait();
                    staffRepo.AddAsync(admin).Wait();

                    // Save changes
                    unitOfWork.CommitAsync().Wait();

                    Console.WriteLine("Seeded 3 staff members: Doctor, Nurse, Admin");
                }
                else
                {
                    Console.WriteLine("Staff members already exist. Skipping seeding.");
                }
            }
        }
    }
}
