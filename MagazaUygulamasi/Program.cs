using MagazaUygulamasi.Entities.Concrete;
using MagazaUygulamasi.Enums;
using MagazaUygulamasi.Repositories.Concrete;
using Spectre.Console;
using System;
using System.Linq;
using System.Threading;

namespace MagazaUygulamasi
{
    internal class Program
    {
        private static readonly CustomerRepositories CustomerRepositories = new CustomerRepositories();
        private static readonly EmployeeRepositories EmployeeRepositories = new EmployeeRepositories();
        private static readonly ProductRepositories ProductRepositories = new ProductRepositories();

        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    // Generate welcome method
                    Welcome();
                    // Generate menu method
                    string menuChoise = Menu();
                    switch (menuChoise)
                    {
                        case "Customers":
                            CustomerMenu();
                            break;
                        case "Employees":
                            EmployeeMenu();
                            break;
                        case "Products":
                            ProductMenu();
                            break;
                        default:
                            Console.WriteLine("Please press any key for exit.");
                            Console.ReadLine();
                            Environment.Exit(0);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    AnsiConsole.WriteException(ex);
                }
            }
        }

        private static void ProductMenu()
        {
            while (true)
            {
                try
                {
                    var productMenu = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Okay, so, uh what you want to do [green]product[/]?")
                            .PageSize(10)
                            .AddChoices("Add Product", "Update Product", "Delete Product", "List All Products", "Go Back"));
                    switch (productMenu)
                    {
                        case "Add Product":
                            AddProduct();
                            break;
                        case "Update Product":
                            UpdateProduct();
                            break;
                        case "Delete Product":
                            DeleteProduct();
                            break;
                        case "List All Product":
                            ListAllProduct();
                            break;
                        default:
                            Console.WriteLine("Please press any key for exit.");
                            Console.ReadLine();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    AnsiConsole.WriteException(ex);
                }
            }
        }

        private static void ListAllProduct()
        {
            throw new NotImplementedException();
        }

        private static void DeleteProduct()
        {
            throw new NotImplementedException();
        }

        private static void UpdateProduct()
        {
            throw new NotImplementedException();
        }

        private static void AddProduct()
        {
            throw new NotImplementedException();
        }

        private static void EmployeeMenu()
        {
            while (true)
            {
                try
                {
                    var employeeMenu = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Okay, so, uh what you want to do [green]employees[/]?")
                            .PageSize(10)
                            .AddChoices("Add Employee", "Update Employee", "Delete Employee", "List All Employee", "Go Back"));
                    switch (employeeMenu)
                    {
                        case "Add Employee":
                            AddEmployee();
                            break;
                        case "Update Employee":
                            UpdateEmployee();
                            break;
                        case "Delete Employee":
                            DeleteEmployee();
                            break;
                        case "List All Employee":
                            ListAllEmployee();
                            break;
                        default:
                            Console.WriteLine("Please press any key for exit.");
                            Console.ReadLine();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    AnsiConsole.WriteException(ex);
                }
            }
        }

        private static void ListAllEmployee()
        {
            var table = new Table().Centered()
                .AddColumn("ID")
                .AddColumn("First Name")
                .AddColumn("Last Name")
                .AddColumn("ID Number")
                .AddColumn("Birth Date")
                .AddColumn("Hire Date")
                .AddColumn("Job Title")
                .AddColumns("Salary");
            foreach (var employee in SeedData.Employees)
            {
                table.AddRow(
                    employee.Id.ToString(), 
                    employee.FirstName, 
                    employee.LastName,
                    employee.IdentificationNumber,
                    employee.BirthDate.ToString("d"),
                    employee.DateOfStart.ToString("d"),
                    employee.Position.ToString(),
                    employee.Salary.ToString("C")
                    );
            }
            AnsiConsole.Write(table);
        }

        private static void DeleteEmployee()
        {
            var customerId = AnsiConsole.Ask<int>("Please enter employee ID to delete: ");
            try
            {
                EmployeeRepositories.Delete(customerId);
                AnsiConsole.WriteLine("Employee deleted successfully !");
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
            }
        }

        private static void UpdateEmployee()
        {
            var employeeId = AnsiConsole.Ask<int>("Please enter employee ID to update: ");
            try
            {
                var employee = EmployeeRepositories.GetById(employeeId);
                if (employee == null)
                {
                    AnsiConsole.WriteLine("Employee not found !");
                    return;
                }
                employee.FirstName = AnsiConsole.Ask<string>("What's Employee Name? ", employee.FirstName);
                employee.LastName = AnsiConsole.Ask<string>("What's Employee Last Name? ", employee.LastName);
                employee.IdentificationNumber = AnsiConsole.Ask<string>("What's Employee ID Number? ", employee.IdentificationNumber);
                employee.BirthDate = AnsiConsole.Ask<DateTime>("What's Employee Birth Date (mm/dd/yyyy)? ", employee.BirthDate);
                employee.Gender = AnsiConsole.Prompt(
                    new SelectionPrompt<Genders>()
                        .Title($"What's Customer Gender?")
                        .PageSize(10)
                        .AddChoices(Genders.Male, Genders.Female)
                );
                AnsiConsole.WriteLine($"What's Customer Gender? {employee.Gender}");
                employee.City = (Cities)AnsiConsole.Ask<int>("What's Employee City Plate Code? ", Convert.ToInt32(employee.City));
                employee.Address = AnsiConsole.Ask<string>("What's Employee Address? ", employee.Address);
                employee.PhoneNumber = AnsiConsole.Ask<string>("What's Employee Phone Number? ", employee.PhoneNumber);
                employee.Email = AnsiConsole.Ask<string>("What's Employee Email? ", employee.Email);

                EmployeeRepositories.Update(employee);
                AnsiConsole.WriteLine("Employee updated successfully !");
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
            }
        }

        private static void AddEmployee()
        {
            var firstName = AnsiConsole.Ask<string>("What's Employee Name? ");
            var lastName = AnsiConsole.Ask<string>("What's Employee Last Name? ");
            var idNumber = AnsiConsole.Ask<string>("What's Employee ID Number? ");
            var birthDate = AnsiConsole.Ask<DateTime>("What's Employee Birth Date (mm/dd/yyyy)? ");
            var gender = AnsiConsole.Prompt(
                new SelectionPrompt<Genders>()
                    .Title("What's Employee Gender? ")
                    .PageSize(10)
                    .AddChoices(Genders.Male, Genders.Female)
            );
            AnsiConsole.WriteLine($"What's Employee Gender? {gender}");
            var city = (Cities)AnsiConsole.Ask<int>("What's Employee City Plate Code? ");
            var address = AnsiConsole.Ask<string>("What's Employee Address? ");
            var phoneNumber = AnsiConsole.Ask<string>("What's Employee Phone Number? ");
            var email = AnsiConsole.Ask<string>("What's Employee Email? ");
            try
            {
                var lastEmployeeId = EmployeeRepositories.GetAll().Max(x => x.Id);
                EmployeeRepositories.Add(new Employee(lastEmployeeId + 1)
                {
                    FirstName = firstName,
                    LastName = lastName,
                    IdentificationNumber = idNumber,
                    BirthDate = birthDate,
                    Gender = gender,
                    City = city,
                    Address = address,
                    PhoneNumber = phoneNumber,
                    Email = email
                });
                AnsiConsole.WriteLine("Employee added successfully.");
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
            }
        }

        private static void CustomerMenu()
        {
            while (true)
            {
                try
                {
                    var customersMenu = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Okay, so, uh what you want to do [green]customers[/]?")
                            .PageSize(10)
                            .AddChoices("Add Customer", "Update Customer", "Delete Customer", "List All Customers", "Go Back"));
                    switch (customersMenu)
                    {
                        case "Add Customer":
                            AddCustomer();
                            break;
                        case "Update Customer":
                            UpdateCustomer();
                            break;
                        case "Delete Customer":
                            DeleteCustomer();
                            break;
                        case "List All Customers":
                            ListAllCustomers();
                            break;
                        default:
                            Console.WriteLine("Please press any key for exit.");
                            Console.ReadLine();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    AnsiConsole.WriteException(ex);
                }
            }
        }

        private static void ListAllCustomers()
        {
            var table = new Table();
            table.AddColumn("ID");
            foreach (var prop in typeof(Customer).GetProperties())
            {
                if (prop.Name == "Id" || prop.Name == "CreateTime")
                    continue;
                table.AddColumn(prop.Name);
            }
            foreach (var customer in SeedData.Customers)
            {
                table.AddRow(customer.Id.ToString(), customer.FirstName, customer.LastName, customer.IdentificationNumber, customer.BirthDate.ToString("d"), customer.City.ToString(), customer.Gender.ToString(), customer.Address, customer.PhoneNumber, customer.Email);
            }
            AnsiConsole.Write(table);
        }

        private static void DeleteCustomer()
        {
            var customerId = AnsiConsole.Ask<int>("Please enter customer ID to delete: ");
            try
            {
                CustomerRepositories.Delete(customerId);
                AnsiConsole.WriteLine("Customer deleted successfully !");
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
            }
        }

        private static void UpdateCustomer()
        {
            var customerId = AnsiConsole.Ask<int>("Please enter customer ID to update: ");
            try
            {
                var customer = CustomerRepositories.GetById(customerId);
                if (customer == null)
                {
                    AnsiConsole.WriteLine("Customer not found !");
                    return;
                }
                customer.FirstName = AnsiConsole.Ask<string>("What's Customer Name? ", customer.FirstName);
                customer.LastName = AnsiConsole.Ask<string>("What's Customer Last Name? ", customer.LastName);
                customer.IdentificationNumber = AnsiConsole.Ask<string>("What's Customer ID Number? ", customer.IdentificationNumber);
                customer.BirthDate = AnsiConsole.Ask<DateTime>("What's Customer Birth Date (mm/dd/yyyy)? ", customer.BirthDate);
                customer.Gender = AnsiConsole.Prompt(
                    new SelectionPrompt<Genders>()
                        .Title($"What's Customer Gender?")
                        .PageSize(10)
                        .AddChoices(Genders.Male, Genders.Female)
                );
                AnsiConsole.WriteLine($"What's Customer Gender? {customer.Gender}");
                customer.City = (Cities)AnsiConsole.Ask<int>("What's Customer City Plate Code? ", Convert.ToInt32(customer.City));
                customer.Address = AnsiConsole.Ask<string>("What's Customer Address? ", customer.Address);
                customer.PhoneNumber = AnsiConsole.Ask<string>("What's Customer Phone Number? ", customer.PhoneNumber);
                customer.Email = AnsiConsole.Ask<string>("What's Customer Email? ", customer.Email);

                CustomerRepositories.Update(customer);
                AnsiConsole.WriteLine("Customer updated successfully !");
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
            }
        }

        private static void AddCustomer()
        {
            var firstName = AnsiConsole.Ask<string>("What's Customer Name? ");
            var lastName = AnsiConsole.Ask<string>("What's Customer Last Name? ");
            var idNumber = AnsiConsole.Ask<string>("What's Customer ID Number? ");
            var birthDate = AnsiConsole.Ask<DateTime>("What's Customer Birth Date (mm/dd/yyyy)? ");
            var gender = AnsiConsole.Prompt(
                new SelectionPrompt<Genders>()
                    .Title("What's Customer Gender? ")
                    .PageSize(10)
                    .AddChoices(Genders.Male, Genders.Female)
                );
            AnsiConsole.WriteLine($"What's Customer Gender? {gender}");
            var city = (Cities)AnsiConsole.Ask<int>("What's Customer City Plate Code? ");
            var address = AnsiConsole.Ask<string>("What's Customer Address? ");
            var phoneNumber = AnsiConsole.Ask<string>("What's Customer Phone Number? ");
            var email = AnsiConsole.Ask<string>("What's Customer Email? ");
            try
            {
                var lastCustomerId = CustomerRepositories.GetAll().Max(x => x.Id);
                CustomerRepositories.Add(new Customer(lastCustomerId + 1)
                {
                    FirstName = firstName,
                    LastName = lastName,
                    IdentificationNumber = idNumber,
                    BirthDate = birthDate,
                    Gender = gender,
                    City = city,
                    Address = address,
                    PhoneNumber = phoneNumber,
                    Email = email
                });
                AnsiConsole.WriteLine("Customer added successfully.");
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
            }
        }

        private static string Menu()
        {
            var categories = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]Okay, so, uh what you want to do here?[/]")
                    .PageSize(10)
                    .AddChoices("Customers", "Employees", "Products", "Exit Program"));
            return categories;
        }

        private static void Welcome()
        {
            AnsiConsole.Status().Start("Running...", ctx =>
            {
                AnsiConsole.MarkupLine("Loading data sets...");
                SeedData.Generate();
                Thread.Sleep(1000);
                AnsiConsole.MarkupLine("Data sets loaded...");
            });
        }
    }
}
