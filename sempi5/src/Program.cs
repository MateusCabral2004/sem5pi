using System.Security.Claims;
using Sempi5.Domain.Shared;
using Sempi5.Infrastructure.Shared;
using Sempi5.Infrastructure.TodoItemRepository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Sempi5.Infrastructure.StaffRepository;
using Sempi5.Domain.User;
using Sempi5.Domain.Staff;
using Sempi5.Infrastructure.Databases;
using Microsoft.IdentityModel.Tokens;
using Sempi5.Domain;
using Sempi5.Services;
using Sempi5.Infrastructure.UserRepository;
using Sempi5.Infrastructure.PatientRepository;
using Sempi5.Domain.Patient;
using Sempi5.Domain.PersonalData;
using Sempi5.Infrastructure.ConfirmationTokenRepository;
using Sempi5.Infrastructure.PersonRepository;

namespace Sempi5
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //teste(args).Wait();

            var builder = WebApplication.CreateBuilder(args);

            CreateDataBase(builder);
            
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
                    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.LoginPath = "/login/login";
                    options.AccessDeniedPath = "/login/logout";
                })
                .AddGoogle(options =>
                {
                    options.ClientId = builder.Configuration["Google:ClientId"];
                    options.ClientSecret = builder.Configuration["Google:Client_Secret"];
                    options.SaveTokens = true;

                    options.Events.OnCreatingTicket = async context =>
                    {
                        var claimsIdentity = (ClaimsIdentity)context.Principal.Identity;
                        var claims = context.Principal.Identities.FirstOrDefault().Claims;
                        var email = claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

                        var repo = context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
                        var user = repo.GetByEmail(email);
                        if (user.Result == null)
                        {
                            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Patient"));
                        }
                        else
                        {
                            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, user.Result.Role));
                        }
                    };
                });

            builder.Services.AddControllersWithViews();
            
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
                SeedPatiens(app.Services);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("Error seeding data");
            }

            app.Run();
        }
        static async Task teste(string[] args)
        {
            // Crie uma instância do EmailService (você pode passar uma instância falsa de IConfiguration se não precisar dela)
            var emailService = new EmailService(null);

            // Email para testar
            string email = "sandroluis720@gmail.com";

            // Gerar um token de verificação (pode ser um GUID, por exemplo)
            string verificationToken = Guid.NewGuid().ToString();

            // Chame o método para enviar o email
            string result = await emailService.SendEmailAsync(email, verificationToken);

            // Exibir o resultado no console
            Console.WriteLine(result);
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
            services.AddTransient<IPatientRepository, PatientRepository>();
            services.AddTransient<IPersonRepository,PersonRepository>();
            services.AddTransient<IConfirmationTokenRepository, ConfirmationTokenRepository>();
            
            services.AddTransient<StaffService>();
            services.AddTransient<LoginService>();
            services.AddTransient<EmailService>();
            services.AddTransient<AdminService>();
            services.AddTransient<PatientService>();
            services.AddTransient<TokenService>();
            
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
                    var administratorUser = new SystemUser(new Email("rpsoares8@gmail.com"), "Admin");
                    var doctorUser = new SystemUser(new Email("mateuscabral2004@gmail.com"), "Admin");
                    var nurseUser = new SystemUser(new Email("nurse@example.com"), "Nurse");
                    var adminUser = new SystemUser(new Email("admin@example.com"), "Admin");

                    // Create staff members

                    var administrator = new Staff
                    (
                        administratorUser,
                        new LicenseNumber(122),
                        new Name("Rui"),
                        new Name("Soares"),
                        "Cardiology",
                        new ContactInfo("rpsoares8@gmail.com", 964666298),
                        new List<string> { "Monday" }

                    );


                var doctor = new Staff
                    (
                        doctorUser,
                        new LicenseNumber(123),
                        new Name("John"),
                        new Name("Doe"),
                        "Cardiology",
                        new ContactInfo("doctor@example.com", 987654321),
                        new List<string> { "Monday 9am-12pm", "Wednesday 1pm-4pm" }
                    );
                    
                    var nurse = new Staff
                    (
                        nurseUser,
                        new LicenseNumber(124),
                        new Name("Jane"),
                        new Name("Smith"),
                        "General",
                        new ContactInfo("nurse@example.com", 988654321),
                        new List<string> { "Tuesday 10am-3pm", "Thursday 9am-12pm" }
                    );

                    var admin = new Staff
                    (
                        adminUser,
                        new LicenseNumber(125),
                        new Name("Alice"),
                        new Name("Johnson"),
                        "Administration",
                        new ContactInfo("admin@example.com", 977654321),
                        new List<string> { "Monday-Friday 9am-5pm" }
                    );

                    // Add staff to repository
                    staffRepo.AddAsync(administrator).Wait();
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

        public static void SeedPatiens(IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var patientRepo = scope.ServiceProvider.GetRequiredService<IPatientRepository>();
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                
                // Check if there are any patients already in the database
                if (!patientRepo.GetAllAsync().Result.Any())
                {
                    Console.WriteLine("Seeding patients...");
                    SystemUser user1 = new SystemUser(new Email("mateuscabral123321@gmail.com"), "Patient");
                    SystemUser user2 = new SystemUser(new Email("mateuscabral20042@gmail.com"), "Patient");
                    // Create patients
                    var patient1 = new Patient
                    (user1,
                        new Person (new Name("Alice"), new Name("Doe"), new ContactInfo(new Email("mateuscabral20042@gmail.com"), new PhoneNumber(987654321))),
                        new DateTime(1990, 1, 10),
                        "Combat Helicopter",
                        new List<string> { "Peanuts", "Asthma" },
                        "456",
                        new List<string> { "01/01/2021 9am-10am", "02/02/2021 10am-11am" }
                    );

                    var patient2 = new Patient
                    (user2,
                        new Person (new Name("Bob"),new Name("Smith"), new ContactInfo(new Email("mateuscabral123321@gmail.com"), new PhoneNumber(987654321))),
                        new DateTime(1990, 1, 10),
                        "Ambulance",
                        new List<string> { "Shellfish", "Diabetes" },
                        "789",
                        new List<string> { "03/03/2021 9am-10am", "04/04/2021 10am-11am" }
                    );

                    // Add patients to repository
                    patientRepo.AddAsync(patient1).Wait();
                    patientRepo.AddAsync(patient2).Wait();

                    // Save changes
                    unitOfWork.CommitAsync().Wait();
                }
            }
        }
    }
}