using MagazaUygulamasi.Entities.Concrete;
using MagazaUygulamasi.Enums;
using MagazaUygulamasi.Repositories.Concrete;
using Spectre.Console;
using System;
using System.Linq;
using System.Net.Mail;
using System.Threading;

namespace MagazaUygulamasi
{
    internal class Program
    {
        private static readonly CustomerRepositories CustomerRepositories = new CustomerRepositories();
        private static readonly EmployeeRepositories EmployeeRepositories = new EmployeeRepositories();
        private static readonly ProductRepositories ProductRepositories = new ProductRepositories();
        private static readonly SaleRepositories SaleRepositories = new SaleRepositories();

        private static void Main(string[] args)
        {
            Welcome();
            while (true)
            {
                try
                {
                    var menuChoise = Menu();
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
                        case "Sales":
                            SalesMenu();
                            break;
                        default:
                            Exit();
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

        private static void SalesMenu()
        {
            try
            {
                var salesMenu = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Okay, so, uh what you want to do [green]sales[/]")
                        .PageSize(10)
                        .AddChoices("Add Sale(s)", "Delete Sale", "List All Sales", "Go Back")
                );
                switch (salesMenu)
                {
                    case "Add Sale(s)":
                        AddSales();
                        break;
                    case "Delete Sale":
                        DeleteSale();
                        break;
                    case "List All Sales":
                        ListAllSales();
                        break;
                    case "Go Back":
                        return;
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex);
            }
        }

        private static void AddSales()
        {
            // TODO: Add Sales
            var salesId = AnsiConsole.Ask<string>("Please enter the sale id: ");
            throw new NotImplementedException();
        }

        private static void ListAllSales()
        {
            var table = new Table().Centered()
                .AddColumn("ID")
                .AddColumn("Product ID")
                .AddColumn("Customer ID")
                .AddColumn("Employee ID")
                .AddColumn("Quantity")
                .AddColumn("Unit Price")
                .AddColumns("Total Profit");
            foreach (var sale in SeedData.Sales)
            {
                var unitPrice = SeedData.Products.FirstOrDefault(x => x.Id == sale.ProductId).UnitPrice;
                var totalProfit = unitPrice * sale.Quantity;
                table.AddRow(
                    sale.Id.ToString(),
                    sale.ProductId.ToString(),
                    sale.CustomerId.ToString(),
                    sale.EmployeeId.ToString(),
                    sale.Quantity.ToString(),
                    unitPrice.ToString("C"),
                    totalProfit.ToString("C")
                );
            }

            AnsiConsole.Write(table);
        }

        private static void DeleteSale()
        {
            var saleId = AnsiConsole.Ask<int>("Please enter sale ID to delete: ");
            try
            {
                SaleRepositories.Delete(saleId);
                AnsiConsole.WriteLine("Sale deleted successfully !");
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
            }
        }

        private static void ProductMenu()
        {
            try
            {
                var productMenu = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Okay, so, uh what you want to do [green]product[/]?")
                        .PageSize(10)
                        .AddChoices("Add Product", "Sell Product", "Update Product", "Delete Product",
                            "List All Products",
                            "Go Back"));
                switch (productMenu)
                {
                    case "Add Product":
                        AddProduct();
                        break;
                    case "Sell Product":
                        SellProduct();
                        break;
                    case "Update Product":
                        UpdateProduct();
                        break;
                    case "Delete Product":
                        DeleteProduct();
                        break;
                    case "List All Products":
                        ListAllProduct();
                        break;

                }
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex);
            }
        }

        private static void SellProduct()
        {
            var productId = AnsiConsole.Ask<int>("Please enter product ID to sell: ");
            try
            {
                var product = ProductRepositories.GetById(productId);
                if (product == null)
                    throw new Exception("Product not found!");
                var employeeId = AnsiConsole.Ask<int>("Please enter employee ID to sell: ");
                var employee = EmployeeRepositories.GetById(employeeId);
                if (employee == null)
                    throw new Exception("Employee not found!");
                var customerId = AnsiConsole.Ask<int>("Please enter customer ID to sell: ");
                var customer = CustomerRepositories.GetById(customerId);
                if (customer == null)
                    throw new Exception("Customer not found!");
                var quantity = AnsiConsole.Ask<int>("Please enter quantity to sell: ");
                if (quantity > product.UnitsInStock)
                    throw new Exception("Not enough quantity!");
                var unitPrice = AnsiConsole.Prompt(
                    new TextPrompt<decimal>("Please enter unit price: ")
                        .DefaultValue(product.UnitPrice)
                        .PromptStyle("green")
                        .ValidationErrorMessage($"[red]Unit price must be between 0 and {product.UnitPrice} ![/]")
                        .Validate(price => price > 0 && price <= product.UnitPrice)
                );

                ProductRepositories.Sell(productId, quantity);
                var lastSaleId = SaleRepositories.GetAll().Max(x => x.Id);
                SaleRepositories.Add(new Sale(lastSaleId + 1)
                {
                    ProductId = productId,
                    CustomerId = customerId,
                    EmployeeId = employeeId,
                    UnitPrice = unitPrice,
                });
                AnsiConsole.WriteLine("Product sold successfully.");
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
            }
            throw new NotImplementedException();
        }

        private static void ListAllProduct()
        {
            var table = new Table().Centered()
                .AddColumn("ID")
                .AddColumn("Product Name")
                .AddColumn("Product Description")
                .AddColumn("Category")
                .AddColumn("Unit Price")
                .AddColumn("Units In Stock")
                .AddColumns("Expiration Date");
            foreach (var product in SeedData.Products)
                table.AddRow(
                    product.Id.ToString(),
                    product.ProductName,
                    product.ProductDescription,
                    product.CategoryId.ToString(),
                    product.UnitPrice.ToString("C"),
                    product.UnitsInStock.ToString(),
                    product.ExpirationDate.ToString("d")
                );
            AnsiConsole.Write(table);
        }

        private static void DeleteProduct()
        {
            var productId = AnsiConsole.Ask<int>("Please enter product ID to delete: ");
            try
            {
                ProductRepositories.Delete(productId);
                AnsiConsole.WriteLine("Product deleted successfully !");
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
            }
        }

        private static void UpdateProduct()
        {
            var productId = AnsiConsole.Ask<int>("Please enter product ID to update: ");
            try
            {
                var product = ProductRepositories.GetById(productId);
                if (product == null)
                {
                    AnsiConsole.WriteLine("Product not found !");
                    return;
                }

                product.ProductName = AnsiConsole.Prompt(
                    new TextPrompt<string>("What's Product Name?")
                        .DefaultValue(product.ProductName)
                        .PromptStyle("green")
                        .ValidationErrorMessage("[red]Product name must be at least 3 characters ![/]")
                        .Validate(name => name.Length >= 3)
                );
                product.ProductDescription = AnsiConsole.Prompt(
                    new TextPrompt<string>("What's Product Description?")
                        .DefaultValue(product.ProductDescription)
                        .PromptStyle("green")
                        .ValidationErrorMessage("[red]Product description must be at least 10 characters ![/]")
                        .Validate(name => name.Length >= 3)
                );
                var productCategory = AnsiConsole.Prompt(
                    new MultiSelectionPrompt<string>()
                        .PageSize(10)
                        .Title("What are your product [green]category[/]?")
                        .MoreChoicesText("[grey](Move up and down to reveal more categories)[/]")
                        .InstructionsText("[grey](Press [blue][/] to toggle a category, [green][/] to accept)[/]")
                        .AddChoiceGroup("All", Categories.Shoes.ToString(), Categories.Clothes.ToString(),
                            Categories.Accessories.ToString(), Categories.Electronics.ToString(),
                            Categories.Books.ToString(), Categories.Sport.ToString(), Categories.Home.ToString(),
                            Categories.Health.ToString(), Categories.Baby.ToString(), Categories.Travel.ToString(),
                            Categories.Food.ToString(), Categories.Automobile.ToString(), Categories.Other.ToString())
                );
                var categoryId = productCategory.Count == 1
                    ? Enum.Parse(typeof(Categories), productCategory[0])
                    : Enum.Parse(typeof(Categories), "All");
                AnsiConsole.MarkupLine("Your selected: [yellow]{0}[/]", categoryId);
                product.CategoryId = (Categories)categoryId;
                product.UnitPrice = AnsiConsole.Prompt(
                    new TextPrompt<decimal>("What's Product Unit Price?")
                        .DefaultValue(product.UnitPrice)
                        .PromptStyle("green")
                        .ValidationErrorMessage("[red]Product unit price must be at least 0.01 ![/]")
                        .Validate(price => price >= 0.01m)
                );
                product.UnitsInStock = AnsiConsole.Prompt(
                    new TextPrompt<int>("What's Product Unit In Stock?")
                        .DefaultValue(product.UnitsInStock)
                        .PromptStyle("green")
                        .ValidationErrorMessage("[red]Product unit in stock must be at least 1 ![/]")
                        .Validate(stock => stock >= 1)
                );
                product.ExpirationDate = AnsiConsole.Prompt(
                    new TextPrompt<DateTime>("What's Product Expiration Date?")
                        .DefaultValue(product.ExpirationDate)
                        .PromptStyle("green")
                        .ValidationErrorMessage("[red]Product expiration date must be at least 1 day ![/]")
                        .Validate(date => date >= DateTime.Now.AddDays(1))
                );
                var lastProductId = ProductRepositories.GetAll().Max(x => x.Id);
                ProductRepositories.Update(lastProductId, product);
                AnsiConsole.WriteLine("Product updated successfully.");
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
            }
        }

        private static void AddProduct()
        {
            var productName = AnsiConsole.Prompt(
                new TextPrompt<string>("What's Product Name?")
                    .PromptStyle("green")
                    .ValidationErrorMessage("[red]Product name must be at least 3 characters ![/]")
                    .Validate(name => name.Length >= 3)
            );
            var productDescription = AnsiConsole.Prompt(
                new TextPrompt<string>("What's Product Description?")
                    .PromptStyle("green")
                    .ValidationErrorMessage("[red]Product description must be at least 10 characters ![/]")
                    .Validate(name => name.Length >= 3)
            );
            var productCategory = AnsiConsole.Prompt(
                new MultiSelectionPrompt<string>()
                    .PageSize(10)
                    .Title("What are your product [green]category[/]?")
                    .MoreChoicesText("[grey](Move up and down to reveal more categories)[/]")
                    .InstructionsText("[grey](Press [blue][/] to toggle a category, [green][/] to accept)[/]")
                    .AddChoiceGroup("All", Categories.Shoes.ToString(), Categories.Clothes.ToString(),
                        Categories.Accessories.ToString(), Categories.Electronics.ToString(),
                        Categories.Books.ToString(), Categories.Sport.ToString(), Categories.Home.ToString(),
                        Categories.Health.ToString(), Categories.Baby.ToString(), Categories.Travel.ToString(),
                        Categories.Food.ToString(), Categories.Automobile.ToString(), Categories.Other.ToString())
            );
            var categoryId = productCategory.Count == 1
                ? Enum.Parse(typeof(Categories), productCategory[0])
                : Enum.Parse(typeof(Categories), "All");
            AnsiConsole.MarkupLine("Your selected: [yellow]{0}[/]", categoryId);
            var unitPrice = AnsiConsole.Prompt(
                new TextPrompt<decimal>("What's Product Unit Price?")
                    .PromptStyle("green")
                    .ValidationErrorMessage("[red]Product unit price must be at least 0.01 ![/]")
                    .Validate(price => price >= 0.01m)
            );
            var unitsInStock = AnsiConsole.Prompt(
                new TextPrompt<int>("What's Product Unit In Stock?")
                    .PromptStyle("green")
                    .ValidationErrorMessage("[red]Product unit in stock must be at least 1 ![/]")
                    .Validate(stock => stock >= 1)
            );
            var expirationDate = AnsiConsole.Prompt(
                new TextPrompt<DateTime>("What's Product Expiration Date?")
                    .PromptStyle("green")
                    .ValidationErrorMessage("[red]Product expiration date must be at least 1 day ![/]")
                    .Validate(date => date >= DateTime.Now.AddDays(1))
            );
            try
            {
                var lastProductId = ProductRepositories.GetAll().Max(x => x.Id);
                ProductRepositories.Add(new Product(lastProductId + 1)
                {
                    ProductName = productName,
                    ProductDescription = productDescription,
                    CategoryId = (Categories)categoryId,
                    UnitPrice = unitPrice,
                    UnitsInStock = unitsInStock,
                    ExpirationDate = expirationDate
                });
                AnsiConsole.WriteLine("Product added successfully.");
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
            }
        }

        private static void EmployeeMenu()
        {
            try
            {
                var employeeMenu = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Okay, so, uh what you want to do [green]employees[/]?")
                        .PageSize(10)
                        .AddChoices("Add Employee", "Update Employee", "Delete Employee", "List All Employee",
                            "Go Back"));
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
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex);
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
                .AddColumns("Salary")
                .AddColumns("Total Sales");
            foreach (var employee in SeedData.Employees)
                table.AddRow(
                    employee.Id.ToString(),
                    employee.FirstName,
                    employee.LastName,
                    employee.IdentificationNumber,
                    employee.BirthDate.ToString("d"),
                    employee.DateOfStart.ToString("d"),
                    employee.Position.ToString(),
                    employee.Salary.ToString("C"),
                    employee.TotalSales.ToString("C")
                );
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

                employee.FirstName = AnsiConsole.Prompt(
                    new TextPrompt<string>("What's Employee Name?")
                        .DefaultValue(employee.FirstName)
                        .PromptStyle("green")
                        .ValidationErrorMessage("[red]First name must be at least 3 characters ![/]")
                        .Validate(name => name.Length >= 3)
                );
                employee.LastName = AnsiConsole.Prompt(
                    new TextPrompt<string>("What's Employee Last Name?")
                        .DefaultValue(employee.LastName)
                        .PromptStyle("green")
                        .ValidationErrorMessage("[red]Last name must be at least 3 characters ![/]")
                        .Validate(name => name.Length >= 3)
                );
                employee.IdentificationNumber = AnsiConsole.Prompt(
                    new TextPrompt<string>("What's Employee ID Number?")
                        .DefaultValue(employee.IdentificationNumber)
                        .PromptStyle("green")
                        .ValidationErrorMessage("[red]ID Number must be 11 digits ![/]")
                        .Validate(IsValidId)
                );
                employee.BirthDate = AnsiConsole.Prompt(
                    new TextPrompt<DateTime>("What's Employee Birth Date (mm/dd/yyyy)? ")
                        .DefaultValue(employee.BirthDate)
                        .PromptStyle("green")
                        .ValidationErrorMessage(
                            "[red]Date of birth must be a valid date and not today. Make sure you enter a date that is over 18 years old.[/]")
                        .Validate(date => date < DateTime.Now || DateTime.Now.Year - date.Year > 18)
                );
                employee.Gender = AnsiConsole.Prompt(
                    new SelectionPrompt<Genders>()
                        .Title("What's Customer Gender?")
                        .PageSize(10)
                        .AddChoices(Genders.Male, Genders.Female)
                );
                AnsiConsole.WriteLine($"What's Customer Gender? {employee.Gender}");
                employee.City = AnsiConsole.Prompt(
                    new TextPrompt<Cities>("What's Employee City Plate Code? ")
                        .DefaultValue(employee.City)
                        .PromptStyle("green")
                        .ValidationErrorMessage("[red]Plate code must be between 1 and 81 ![/]")
                        .Validate(cityP => cityP > 0 && (int)cityP < 81)
                );
                employee.Address = AnsiConsole.Ask("What's Employee Address? ", employee.Address);
                employee.PhoneNumber = AnsiConsole.Prompt(
                    new TextPrompt<string>("What's Employee Phone Number? ")
                        .DefaultValue(employee.PhoneNumber)
                        .PromptStyle("green")
                        .ValidationErrorMessage("[red]Phone Number must be 11 digits ![/]")
                        .Validate(IsValidPhoneNumber)
                );
                employee.Email = AnsiConsole.Prompt(
                    new TextPrompt<string>("What's Employee Email? ")
                        .DefaultValue(employee.Email)
                        .PromptStyle("green")
                        .ValidationErrorMessage("[red]Invalid email format ![/]")
                        .Validate(IsvalidEmail)
                );
                employee.Position = AnsiConsole.Prompt(
                    new SelectionPrompt<Positions>()
                        .Title("What's Employee Position? ")
                        .PageSize(10)
                        .AddChoices(Positions.Admin, Positions.Manager, Positions.SalesPerson, Positions.CashierPerson)
                );
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
            var firstName = AnsiConsole.Prompt(
                new TextPrompt<string>("What's Employee Name?")
                    .PromptStyle("green")
                    .ValidationErrorMessage("[red]First name must be at least 3 characters ![/]")
                    .Validate(name => name.Length >= 3)
            );
            var lastName = AnsiConsole.Prompt(
                new TextPrompt<string>("What's Employee Last Name?")
                    .PromptStyle("green")
                    .ValidationErrorMessage("[red]Last name must be at least 3 characters ![/]")
                    .Validate(name => name.Length >= 3)
            );
            var idNumber = AnsiConsole.Prompt(
                new TextPrompt<string>("What's Employee ID Number?")
                    .PromptStyle("green")
                    .ValidationErrorMessage("[red]ID Number must be 11 digits ![/]")
                    .Validate(IsValidId)
            );
            var birthDate = AnsiConsole.Prompt(
                new TextPrompt<DateTime>("What's Employee Birth Date (mm/dd/yyyy)? ")
                    .PromptStyle("green")
                    .ValidationErrorMessage(
                        "[red]Date of birth must be a valid date and not today. Make sure you enter a date that is over 18 years old.[/]")
                    .Validate(date => date < DateTime.Now || DateTime.Now.Year - date.Year > 18)
            );
            var dateOfStart = AnsiConsole.Prompt(
                new TextPrompt<DateTime>("What's Employee Date of Start (mm/dd/yyyy)? ")
                    .PromptStyle("green")
                    .ValidationErrorMessage("[red]Date of start must be a valid date ![/]")
                    .Validate(date => date >= DateTime.Now)
            );
            var gender = AnsiConsole.Prompt(
                new SelectionPrompt<Genders>()
                    .Title("What's Employee Gender? ")
                    .PageSize(10)
                    .AddChoices(Genders.Male, Genders.Female)
            );
            AnsiConsole.WriteLine($"What's Employee Gender? {gender}");
            var city = AnsiConsole.Prompt(
                new TextPrompt<int>("What's Employee City Plate Code? ")
                    .PromptStyle("green")
                    .ValidationErrorMessage("[red]Plate code must be between 1 and 81 ![/]")
                    .Validate(cityP => cityP > 0 && cityP < 82)
            );
            var address = AnsiConsole.Ask<string>("What's Employee Address? ");
            var phoneNumber = AnsiConsole.Prompt(
                new TextPrompt<string>("What's Employee Phone Number? ")
                    .PromptStyle("green")
                    .ValidationErrorMessage("[red]Phone Number must be 11 digits ![/]")
                    .Validate(IsValidPhoneNumber)
            );
            var email = AnsiConsole.Prompt(
                new TextPrompt<string>("What's Employee Email? ")
                    .PromptStyle("green")
                    .ValidationErrorMessage("[red]Invalid email format ![/]")
                    .Validate(IsvalidEmail)
            );
            var position = AnsiConsole.Prompt(
                new SelectionPrompt<Positions>()
                    .Title("What's Employee Position? ")
                    .PageSize(10)
                    .AddChoices(Positions.Admin, Positions.Manager, Positions.SalesPerson, Positions.CashierPerson)
            );
            try
            {
                var lastEmployeeId = EmployeeRepositories.GetAll().Max(x => x.Id);
                EmployeeRepositories.Add(new Employee(lastEmployeeId + 1)
                {
                    FirstName = firstName,
                    LastName = lastName,
                    IdentificationNumber = idNumber,
                    BirthDate = birthDate,
                    DateOfStart = dateOfStart,
                    Gender = gender,
                    City = (Cities)city,
                    Address = address,
                    PhoneNumber = phoneNumber,
                    Email = email,
                    Position = position,
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
            try
            {
                var customersMenu = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Okay, so, uh what you want to do [green]customers[/]?")
                        .PageSize(10)
                        .AddChoices("Add Customer", "Update Customer", "Delete Customer", "List All Customers",
                            "Go Back"));
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
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex);
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
                table.AddRow(customer.Id.ToString(), customer.FirstName, customer.LastName,
                    customer.IdentificationNumber, customer.BirthDate.ToString("d"), customer.City.ToString(),
                    customer.Gender.ToString(), customer.Address, customer.PhoneNumber, customer.Email);
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

                customer.FirstName = AnsiConsole.Ask("What's Customer Name? ", customer.FirstName);
                customer.LastName = AnsiConsole.Ask("What's Customer Last Name? ", customer.LastName);
                customer.IdentificationNumber =
                    AnsiConsole.Ask("What's Customer ID Number? ", customer.IdentificationNumber);
                customer.BirthDate = AnsiConsole.Ask("What's Customer Birth Date (mm/dd/yyyy)? ", customer.BirthDate);
                customer.Gender = AnsiConsole.Prompt(
                    new SelectionPrompt<Genders>()
                        .Title("What's Customer Gender?")
                        .PageSize(10)
                        .AddChoices(Genders.Male, Genders.Female)
                );
                AnsiConsole.WriteLine($"What's Customer Gender? {customer.Gender}");
                customer.City =
                    (Cities)AnsiConsole.Ask("What's Customer City Plate Code? ", Convert.ToInt32(customer.City));
                customer.Address = AnsiConsole.Ask("What's Customer Address? ", customer.Address);
                customer.PhoneNumber = AnsiConsole.Ask("What's Customer Phone Number? ", customer.PhoneNumber);
                customer.Email = AnsiConsole.Ask("What's Customer Email? ", customer.Email);

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
            var firstName = AnsiConsole.Prompt(
                new TextPrompt<string>("What's Customer Name?")
                    .PromptStyle("green")
                    .ValidationErrorMessage("[red]First name must be at least 3 characters ![/]")
                    .Validate(name => name.Length >= 3)
            );
            var lastName = AnsiConsole.Prompt(
                new TextPrompt<string>("What's Customer Last Name?")
                    .PromptStyle("green")
                    .ValidationErrorMessage("[red]Last name must be at least 3 characters ![/]")
                    .Validate(name => name.Length >= 3)
            );
            var idNumber = AnsiConsole.Prompt(
                new TextPrompt<string>("What's Customer ID Number?")
                    .PromptStyle("green")
                    .ValidationErrorMessage("[red]ID Number must be 11 digits ![/]")
                    .Validate(IsValidId)
            );
            var birthDate = AnsiConsole.Prompt(
                new TextPrompt<DateTime>("What's Employee Birth Date (mm/dd/yyyy)? ")
                    .PromptStyle("green")
                    .ValidationErrorMessage(
                        "[red]Date of birth must be a valid date and not today. Make sure you enter a date that is over 18 years old.[/]")
                    .Validate(date => date < DateTime.Now || DateTime.Now.Year - date.Year > 18)
            );
            var gender = AnsiConsole.Prompt(
                new SelectionPrompt<Genders>()
                    .Title("What's Customer Gender? ")
                    .PageSize(10)
                    .AddChoices(Genders.Male, Genders.Female)
            );
            AnsiConsole.WriteLine($"What's Customer Gender? {gender}");
            var city = AnsiConsole.Prompt(
                new TextPrompt<int>("What's Employee City Plate Code? ")
                    .PromptStyle("green")
                    .ValidationErrorMessage("[red]Plate code must be between 1 and 81 ![/]")
                    .Validate(cityP => cityP > 0 && cityP < 82)
            );
            var address = AnsiConsole.Ask<string>("What's Customer Address? ");
            var phoneNumber = AnsiConsole.Prompt(
                new TextPrompt<string>("What's Customer Phone Number? ")
                    .PromptStyle("green")
                    .ValidationErrorMessage("[red]Phone Number must be 11 digits ![/]")
                    .Validate(IsValidPhoneNumber)
            );
            var email = AnsiConsole.Prompt(
                new TextPrompt<string>("What's Customer Email? ")
                    .PromptStyle("green")
                    .ValidationErrorMessage("[red]Invalid email format ![/]")
                    .Validate(IsvalidEmail)
            );
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
                    City = (Cities)city,
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
                    .AddChoices("Customers", "Employees", "Products", "Sales", "Exit Program"));
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

        private static void Exit()
        {
            AnsiConsole.Status().Start("Exiting...", ctx => { Thread.Sleep(1000); });
        }

        private static bool IsValidId(string identificationNumber)
        {
            return identificationNumber != null && identificationNumber.Length == 11 &&
                   identificationNumber.All(char.IsDigit);
        }

        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            return !string.IsNullOrEmpty(phoneNumber) && phoneNumber.Length == 11 && phoneNumber.All(char.IsDigit);
        }

        public static bool IsvalidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;
            var trimmedEmail = email.Trim();
            if (trimmedEmail.EndsWith("."))
                return false;
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
    }
}