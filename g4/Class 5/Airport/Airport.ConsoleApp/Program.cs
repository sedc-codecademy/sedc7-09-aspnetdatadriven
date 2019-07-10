using Airport.Entities;
using Airport.Repositories.Contracts;
using Airport.Repositories.Implementations;
using Airport.UnitOfWork.Contracts;
using Airport.UnitOfWork.Implementations;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Airport.ConsoleApp
{
    class Program
    {
        private static ServiceProvider _serviceProvider;

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            RegisterServices();
            //using (var businessObjectRepository = _serviceProvider.GetService<IBaseRepository<BusinessObject>>())
            //using (var employeeRepository = _serviceProvider.GetService<IBaseRepository<Employee>>())
            //using (var offerRepository = _serviceProvider.GetService<IBaseRepository<Offer>>())
            //using (var boeRepository = _serviceProvider.GetService<IBaseRepository<BusinessObjectEmployee>>())
            //{
            //    TestAdd(businessObjectRepository, offerRepository, employeeRepository, boeRepository);
            //    TestGetAll(businessObjectRepository);
            //}

            var unitOfWork = _serviceProvider.GetService<IAirportUnitOfWork>();
            TestAddWithUnitOfWOrk(unitOfWork);
            TestGetAllWithUnitOfWork(unitOfWork);

            Console.ReadLine();
        }

        private static void TestAdd(IBaseRepository<BusinessObject> businessObjectRepository,
            IBaseRepository<Offer> offerRepository,
            IBaseRepository<Employee> employeeRepository,
            IBaseRepository<BusinessObjectEmployee> boeRepository)
        {
            var employee = new Employee
            {
                FullName = "Стоилко Стоилковски"
            };
            employeeRepository.Add(employee);

            var businessObject = new BusinessObject
            {
                Name = "Чичко Стоилко",
                OpeningTime = new DateTime(1, 1, 1, 6, 0, 0),
                WorkingTime = new TimeSpan(18, 0, 0),
                Type = Entities.Enums.BusinessObjectType.Other,
                //ResponsibleEmployees = new 
            };
            businessObjectRepository.Add(businessObject);

            var boe = new BusinessObjectEmployee
            {
                BusinessObject = businessObject,
                ResponsibleEmployee = employee
            };
            boeRepository.Add(boe);
        }

        private static void TestAddWithUnitOfWOrk(IAirportUnitOfWork unitOfWork)
        {
            try
            {
                var employee = new Employee
                {
                    FullName = "Стоилко Стоилковски"
                };
                unitOfWork.EmployeeRepository.Add(employee);

                var businessObject = new BusinessObject
                {
                    Name = "Чичко Стоилко",
                    OpeningTime = new DateTime(1, 1, 1, 6, 0, 0),
                    WorkingTime = new TimeSpan(18, 0, 0),
                    Type = Entities.Enums.BusinessObjectType.Other,
                    //ResponsibleEmployees = new 
                };
                unitOfWork.BusinessObjectRepository.Add(businessObject);

                var boe = new BusinessObjectEmployee
                {
                    BusinessObject = businessObject,
                    ResponsibleEmployee = employee
                };
                unitOfWork.BusinessObjectEmployeeRepository.Add(boe);

                unitOfWork.CommitChanges();
            }
            catch (Exception)
            {
                unitOfWork.RevertChanges();
            }
            
        }

        private static void TestGetAll(IBaseRepository<BusinessObject> businessObjectRepository)
        {
            var allBusinessObjects = businessObjectRepository.GetAll();
            foreach (var businessObject in allBusinessObjects)
            {
                Console.WriteLine($"{businessObject.Id} : {businessObject.Name} : {businessObject.Type}");
            }
        }

        private static void TestGetAllWithUnitOfWork(IAirportUnitOfWork unitOfWork)
        {
            var allBusinessObjects = unitOfWork.BusinessObjectRepository.GetAll();
            foreach (var businessObject in allBusinessObjects)
            {
                Console.WriteLine($"{businessObject.Id} : {businessObject.Name} : {businessObject.Type}");
            }
        }

        private static void RegisterServices()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<AirportDbContext>();

            serviceCollection.AddScoped<IBaseRepository<BusinessObject>, BaseRepository<BusinessObject>>();
            serviceCollection.AddScoped<IBaseRepository<Employee>, BaseRepository<Employee>>();
            serviceCollection.AddScoped<IBaseRepository<Offer>, BaseRepository<Offer>>();
            serviceCollection.AddScoped<IBaseRepository<BusinessObjectEmployee>, BaseRepository<BusinessObjectEmployee>>();

            serviceCollection.AddScoped<IAirportUnitOfWork, AirportUnitOfWork>();

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
