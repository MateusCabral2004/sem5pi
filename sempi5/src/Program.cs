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
using Sempi5.Domain.OperationRequest;
using Sempi5.Domain.OperationType;
using Sempi5.Services;
using Sempi5.Infrastructure.UserRepository;
using Sempi5.Infrastructure.PatientRepository;
using Sempi5.Domain.Patient;
using Sempi5.Domain.PersonalData;
using Sempi5.Domain.Specialization;
using Sempi5.Domain.SurgeryRoomAggregate;
using Sempi5.Infrastructure.AppointmentRepository;
using Sempi5.Infrastructure.ConfirmationTokenRepository;
using Sempi5.Infrastructure.OperationRequest;
using Sempi5.Infrastructure.OperationTypeRepository;
using Sempi5.Infrastructure.PersonRepository;
using Sempi5.Infrastructure.RequiredStaffRepository;
using Sempi5.Infrastructure.SpecializationRepository;
using Sempi5.Infrastructure.SurgeryRoomRepository;

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
                    options.ExpireTimeSpan = TimeSpan.FromHours(2);
                    options.SlidingExpiration = true;
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
                SeedStaffProfiles(app.Services);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
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
            services.AddTransient<ISpecializationRepository, SpecializationRepository>();
            services.AddTransient<IStaffRepository, StaffRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IPatientRepository, PatientRepository>();
            services.AddTransient<IRequiredStaffRepository, RequiredStaffRepository>();
            services.AddTransient<IOperationTypeRepository, OperationTypeRepository>();
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<IConfirmationTokenRepository, ConfirmationTokenRepository>();
            services.AddTransient<IOperationRequestRepository, OperationRequestRepository>();
            services.AddTransient<ISurgeryRoomRepository, SurgeryRoomRepository>();
            services.AddTransient<IAppointmentRepository, AppointmentRepository>();


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

                var specializationRepo = scope.ServiceProvider.GetRequiredService<ISpecializationRepository>();
                
                // Check if there are any staff members already in the database
                if (!staffRepo.GetAllAsync().Result.Any())
                {
                    // Create the system users for staff
                    var administratorUser = new SystemUser(new Email("rpsoares8@gmail.com"), "Admin");
                    var doctorUser = new SystemUser(new Email("mateuscabral2004@gmail.com"), "Admin");
                    var nurseUser = new SystemUser(new Email("nurse@example.com"), "Nurse");
                    var adminUser = new SystemUser(new Email("admin@example.com"), "Admin");

                    // Create staff members

                    var specialization1 = new Specialization(new SpecializationName("Cardiology"));
                    var specialization2 = new Specialization(new SpecializationName("Administration"));

                    var administrator = new Staff
                    (
                        administratorUser,
                        new LicenseNumber(122),
                        new Name("Rui"),
                        new Name("Soares"),
                        specialization1,
                        new ContactInfo("rpsoares8@gmail.com", 964666298),
                        new List<string> { "Monday" }
                    );


                    var doctor = new Staff
                    (
                        doctorUser,
                        new LicenseNumber(123),
                        new Name("John"),
                        new Name("Doe"),
                        specialization1,
                        new ContactInfo("doctor@example.com", 987654321),
                        new List<string> { "Monday 9am-12pm", "Wednesday 1pm-4pm" }
                    );

                    var nurse = new Staff
                    (
                        nurseUser,
                        new LicenseNumber(124),
                        new Name("Jane"),
                        new Name("Smith"),
                        specialization1,
                        new ContactInfo("nurse@example.com", 988654321),
                        new List<string> { "Tuesday 10am-3pm", "Thursday 9am-12pm" }
                    );

                    var admin = new Staff
                    (
                        adminUser,
                        new LicenseNumber(125),
                        new Name("Alice"),
                        new Name("Johnson"),
                        specialization2,
                        new ContactInfo("admin@example.com", 977654321),
                        new List<string> { "Monday-Friday 9am-5pm" }
                    );

                    specializationRepo.AddAsync(specialization1).Wait();
                    specializationRepo.AddAsync(specialization2).Wait();
                    
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
                var specRepo = scope.ServiceProvider.GetRequiredService<ISpecializationRepository>();
                var requiredRepo = scope.ServiceProvider.GetRequiredService<IRequiredStaffRepository>();
                var opTypeRepo = scope.ServiceProvider.GetRequiredService<IOperationTypeRepository>();
                var requestRepo = scope.ServiceProvider.GetRequiredService<IOperationRequestRepository>();
                var surgeryRoomRepo = scope.ServiceProvider.GetRequiredService<ISurgeryRoomRepository>();
                var appointmentRepo = scope.ServiceProvider.GetRequiredService<IAppointmentRepository>();

                // Check if there are any patients already in the database
                if (!patientRepo.GetAllAsync().Result.Any())
                {
                    Console.WriteLine("Seeding patients...");
                    SystemUser user1 = new SystemUser(new Email("mateuscabral123321@gmail.com"), "Patient");
                    SystemUser user2 = new SystemUser(new Email("mateuscabral20042@gmail.com"), "Patient");
                    // Create patients
                    var patient1 = new Patient
                    (user1,
                        new Person(new Name("Alice"), new Name("Doe"),
                            new ContactInfo(new Email("mateuscabral20042@gmail.com"), new PhoneNumber(987654321))),
                        new DateTime(1990, 1, 10),
                        "Combat Helicopter",
                        new List<string> { "Peanuts", "Asthma" },
                        "456",
                        new List<string> { "01/01/2021 9am-10am", "02/02/2021 10am-11am" }
                    );

                    var patient2 = new Patient
                    (user2,
                        new Person(new Name("Bob"), new Name("Smith"),
                            new ContactInfo(new Email("mateuscabral123321@gmail.com"), new PhoneNumber(987654321))),
                        new DateTime(1990, 1, 10),
                        "Ambulance",
                        new List<string> { "Shellfish", "Diabetes" },
                        "789",
                        new List<string> { "03/03/2021 9am-10am", "04/04/2021 10am-11am" }
                    );

                    var specialization1 = new Specialization(new SpecializationName("Nurse")
                    );

                    var specialization2 = new Specialization(new SpecializationName("Operation"));

                    var requiredStaff1 = new RequiredStaff(new NumberOfStaff(10), specialization1);
                    var requiredStaff2 = new RequiredStaff(new NumberOfStaff(20), specialization1);
                    var requiredStaff3 = new RequiredStaff(new NumberOfStaff(30), specialization1);
                    var requiredStaff4 = new RequiredStaff(new NumberOfStaff(40), specialization2);

                    var operationType1 = new OperationType(new OperationName("Heart Surgery"),
                        new List<RequiredStaff> { requiredStaff1, requiredStaff2 }, new TimeSpan(2, 0, 0));
                    var operationType2 = new OperationType(new OperationName("Brain Surgery"),
                        new List<RequiredStaff> { requiredStaff3, requiredStaff4 }, new TimeSpan(3, 0, 0));

                    var doctorUser = new SystemUser(new Email("mateuscabral22004@gmail.com"), "Admin");

                    var doctor = new Staff
                    (
                        doctorUser,
                        new LicenseNumber(213),
                        new Name("Johnnnnn"),
                        new Name("Doe"),
                        specialization1,
                        new ContactInfo("doctor@example.com", 987254321),
                        new List<string> { "Monday 9am-12pm", "Wednesday 1pm-4pm" }
                    );

                    var request1 = new OperationRequest(doctor, patient1, operationType1, new DateTime(2021, 1, 1),
                        PriorityEnum.HIGH);

                    var surgeryRoom = new SurgeryRoom(RoomTypeEnum.CONSULTATION_ROOM, new RoomCapacity(10),
                        new List<string> { "Tesoura" }, RoomStatusEnum.AVAILABLE, new List<string> { "???????" });

                    var appointment1 = new Appointment(request1, surgeryRoom, new DateTime(2024, 10, 23),
                        StatusEnum.SCHEDULED);

                    // Add patients to repository
                    patientRepo.AddAsync(patient1).Wait();
                    patientRepo.AddAsync(patient2).Wait();

                    specRepo.AddAsync(specialization1).Wait();
                    specRepo.AddAsync(specialization2).Wait();

                    requiredRepo.AddAsync(requiredStaff1).Wait();
                    requiredRepo.AddAsync(requiredStaff2).Wait();
                    requiredRepo.AddAsync(requiredStaff3).Wait();
                    requiredRepo.AddAsync(requiredStaff4).Wait();

                    opTypeRepo.AddAsync(operationType1);
                    opTypeRepo.AddAsync(operationType2);

                    requestRepo.AddAsync(request1);

                    surgeryRoomRepo.AddAsync(surgeryRoom);

                    appointmentRepo.AddAsync(appointment1);

                    // Save changes
                    unitOfWork.CommitAsync().Wait();
                }
            }
        }

        public static void SeedStaffProfiles(IServiceProvider services)
        {
            
            var specialization = new Specialization(new SpecializationName("Doctor"));
            
            
            using (var scope = services.CreateScope())
            {
                var staffRepo = scope.ServiceProvider.GetRequiredService<IStaffRepository>();
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var specializationRepo = scope.ServiceProvider.GetRequiredService<ISpecializationRepository>();

                // Check if there are any staff members already in the database
                var staffProfile1 = new Staff(
                    new LicenseNumber(217),
                    new Name("John"),
                    new Name("Stuart"),
                    specialization,
                    new ContactInfo("mateuscabral2004@gmail.com", 987254321),
                    new List<string> { "Monday 9am-12pm", "Wednesday 1pm-4pm" }
                );
                
                
                specializationRepo.AddAsync(specialization).Wait();
                
                staffRepo.AddAsync(staffProfile1).Wait();
                
                unitOfWork.CommitAsync().Wait();
            }
        }
    }
}