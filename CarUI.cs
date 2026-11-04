using System;
using System.Collections.Generic;

namespace CarBookingSystem.Car
{
    /// <summary>
    /// UI Layer - Handles all user interaction and menus.
    /// </summary>
    public class CarUI
    {
        private readonly CarService _carService;

        public CarUI(CarService carService)
        {
            _carService = carService;
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                ShowWelcomeBanner();
                Console.WriteLine("  ------------------- USER MENU ----------------");
                Console.WriteLine("                     1. Admin");
                Console.WriteLine("                     2. Customer");
                Console.WriteLine("                     3. Exit");
                Console.Write("                    Choose the option: ");

                string input = Console.ReadLine();
                int userOption;
                if (!int.TryParse(input, out userOption))
                {
                    Console.WriteLine("Invalid input. Press any key...");
                    Console.ReadKey();
                    continue;
                }

                if (userOption == 1)
                {
                    HandleAdminLogin();
                }
                else if (userOption == 2)
                {
                    ShowCustomerMenu();
                }
                else if (userOption == 3)
                {
                    Console.WriteLine("You exited this system.");
                    Console.WriteLine("Thanks for choosing our software.");
                    Console.WriteLine("Press any key to close...");
                    Console.ReadKey();
                    return;
                }
                else
                {
                    Console.WriteLine("You chose a wrong option. Press any key...");
                    Console.ReadKey();
                }
            }
        }

        private void HandleAdminLogin()
        {
            Console.Clear();
            for (int attempt = 1; attempt <= 3; attempt++)
            {
                Console.Write("Enter username: ");
                string username = Console.ReadLine() ?? "";
                Console.Write("Enter password: ");
                string password = Console.ReadLine() ?? "";

                if (_carService.AuthenticateAdmin(username, password))
                {
                    Console.WriteLine("You logged in successfully.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    ShowAdminMenu();
                    return;
                }
                else
                {
                    Console.WriteLine("Wrong username or password. Attempts left: " + (3 - attempt));
                    Console.ReadKey();
                }
            }
            Console.WriteLine("Too many failed attempts.");
            Console.ReadKey();
        }

        private void ShowAdminMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=========================================================================");
                Console.WriteLine("================================ ADMIN MENU ==============================");
                Console.WriteLine("=========================================================================");
                Console.WriteLine("1. Add cars");
                Console.WriteLine("2. View total sales");
                Console.WriteLine("3. View all cars");
                Console.WriteLine("4. Total revenue");
                Console.WriteLine("5. Logout");
                Console.Write("Choose your option: ");

                string input = Console.ReadLine();
                int option;
                if (!int.TryParse(input, out option))
                {
                    Console.WriteLine("Invalid input.");
                    Console.ReadKey();
                    continue;
                }

                if (option == 1)
                {
                    AddCarFlow();
                }
                else if (option == 2)
                {
                    Console.WriteLine("TOTAL SALES");
                    Console.WriteLine(_carService.TotalSales);
                    Pause();
                }
                else if (option == 3)
                {
                    DisplayCars(_carService.GetAllCars());
                    Pause();
                }
                else if (option == 4)
                {
                    Console.WriteLine("====================== TOTAL REVENUE =======================");
                    Console.WriteLine("Total earned RS is " + _carService.TotalRevenue);
                    Pause();
                }
                else if (option == 5)
                {
                    Console.WriteLine("You logged out successfully.");
                    Pause();
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid option.");
                    Pause();
                }
            }
        }

        private void AddCarFlow()
        {
            Console.Write("Enter name of car: ");
            string name = Console.ReadLine() ?? "";
            Console.Write("Enter number of car: ");
            string number = Console.ReadLine() ?? "";
            Console.Write("Enter car price: ");
            int price;
            if (!int.TryParse(Console.ReadLine(), out price))
            {
                Console.WriteLine("Invalid price.");
                Pause();
                return;
            }
            Console.Write("Enter car model (year): ");
            int model;
            if (!int.TryParse(Console.ReadLine(), out model))
            {
                Console.WriteLine("Invalid model.");
                Pause();
                return;
            }
            Console.Write("Enter car colour: ");
            string colour = Console.ReadLine() ?? "";
            Console.Write("Enter car status (Available/Booked): ");
            string status = Console.ReadLine() ?? "Available";

            bool success = _carService.AddCar(name, number, price, model, colour, status);
            if (success)
            {
                Console.WriteLine("Your car added successfully.");
                Console.WriteLine("Your data is saved to files too.");
            }
            else
            {
                Console.WriteLine("Failed to add car. Check inputs or duplicate number.");
            }
            Pause();
        }

        private void ShowCustomerMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=========================================================================");
                Console.WriteLine("=============================== CUSTOMER MENU ===========================");
                Console.WriteLine("=========================================================================");
                Console.WriteLine("1. View available cars");
                Console.WriteLine("2. Book a test drive");
                Console.WriteLine("3. Search the cars by name");
                Console.WriteLine("4. Search the car by model");
                Console.WriteLine("5. Car booking");
                Console.WriteLine("6. Exit");
                Console.Write("Choose option: ");

                string input = Console.ReadLine();
                int option;
                if (!int.TryParse(input, out option))
                {
                    Console.WriteLine("Invalid input.");
                    Pause();
                    continue;
                }

                if (option == 1)
                {
                    DisplayCars(_carService.GetAvailableCars());
                    Pause();
                }
                else if (option == 2)
                {
                    BookTestDriveFlow();
                }
                else if (option == 3)
                {
                    SearchByNameFlow();
                }
                else if (option == 4)
                {
                    SearchByModelFlow();
                }
                else if (option == 5)
                {
                    BookCarFlow();
                }
                else if (option == 6)
                {
                    Console.WriteLine("You exit.");
                    Pause();
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid option.");
                    Pause();
                }
            }
        }

        private void BookTestDriveFlow()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine() ?? "";
            Console.Write("Enter your current location: ");
            string loc = Console.ReadLine() ?? "";
            Console.Write("Enter your email: ");
            string email = Console.ReadLine() ?? "";
            Console.Write("Enter your phone number: ");
            string phone = Console.ReadLine() ?? "";
            Console.Write("Enter the name of the car for test drive: ");
            string car = Console.ReadLine() ?? "";

            Console.WriteLine("=========================== THANKS =========================");
            Console.WriteLine("Your test drive is booked.");
            Console.WriteLine("Please bring your licence with you.");
            Console.WriteLine("We will send the date of drive on " + email);
            Pause();
        }

        private void SearchByNameFlow()
        {
            Console.Write("Enter the name of car: ");
            string name = Console.ReadLine() ?? "";
            var car = _carService.SearchByName(name);

            if (car != null)
            {
                Console.WriteLine("Car is available:");
                Console.WriteLine(car);
            }
            else
            {
                Console.WriteLine("Car is not available.");
            }
            Pause();
        }

        private void SearchByModelFlow()
        {
            Console.Write("Enter the model (year) of car: ");
            int model;
            if (!int.TryParse(Console.ReadLine(), out model))
            {
                Console.WriteLine("Invalid model year.");
                Pause();
                return;
            }

            var cars = _carService.SearchByModel(model);
            if (cars.Count > 0)
            {
                Console.WriteLine("Found " + cars.Count + " car(s):");
                DisplayCars(cars);
            }
            else
            {
                Console.WriteLine("No car found with that model.");
            }
            Pause();
        }

        private void BookCarFlow()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine() ?? "";
            Console.Write("Enter your current location: ");
            string loc = Console.ReadLine() ?? "";
            Console.Write("Enter your email: ");
            string email = Console.ReadLine() ?? "";
            Console.Write("Enter your phone number: ");
            string phone = Console.ReadLine() ?? "";
            Console.Write("Enter the name of the car you want to book: ");
            string carName = Console.ReadLine() ?? "";

            bool success = _carService.BookCar(carName);

            if (success)
            {
                Console.WriteLine("============================== CONGRATULATION ==========================");
                Console.WriteLine();
                Console.WriteLine("######################################################################");
                Console.WriteLine("#                        THE KEYS ARE YOURS                          #");
                Console.WriteLine("#                               NOW                                  #");
                Console.WriteLine("#              YOU ARE OFFICIALLY THE OWNER OF THIS CAR              #");
                Console.WriteLine("######################################################################");
                Console.WriteLine("You can receive your car from the showroom next week.");
            }
            else
            {
                Console.WriteLine("Booking failed. The car may not exist or is already booked.");
            }
            Pause();
        }

        private void ShowWelcomeBanner()
        {
            Console.WriteLine("            ##############################################");
            Console.WriteLine("            #               WELCOME TO                   #");
            Console.WriteLine("            #       PREMIUM CAR BOOKING SYSTEM           #");
            Console.WriteLine("            #                                            #");
            Console.WriteLine("            #   .....THE ART OF PREMIUM MOBILITY.....    #");
            Console.WriteLine("            #                                            #");
            Console.WriteLine("            ##############################################");
            Console.WriteLine();
        }

        private void DisplayCars(List<CarModel> cars)
        {
            if (cars.Count == 0)
            {
                Console.WriteLine("No cars to display.");
                return;
            }

            Console.WriteLine("------------------------------------------------------------");
            foreach (var car in cars)
            {
                Console.WriteLine("Name   : " + car.Name);
                Console.WriteLine("Number : " + car.Number);
                Console.WriteLine("Price  : " + car.Price);
                Console.WriteLine("Model  : " + car.Model);
                Console.WriteLine("Colour : " + car.Colour);
                Console.WriteLine("Status : " + car.Status);
                Console.WriteLine("------------------------------------------------------------");
            }
        }
        private void PaginatedViewOptions(int pageSize)
        {
            MenuOptions
                (char FirstPage: 'F';
        char NextPage: 'N';
        char PreviousPage: 'P';
        char LastPage: 'L';
        char ToExit: 'X'
        );
        if(page = 'F' && choice == P || page = 'L' && choice == N)
            {

            }



        private void Pause()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
