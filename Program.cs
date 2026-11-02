using System;
using CarBookingSystem.Car;

namespace CarBookingSystem
{
    /// <summary>
    /// Driver Code - Entry point of the application.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Create Repository
            CarRepository carRepository = new CarRepository("Data/cars.txt");

            // Create Service and inject Repository
            CarService carService = new CarService(carRepository);

            // Create UI and inject Service
            CarUI carUI = new CarUI(carService);

            // Start the application
            carUI.Run();
        }
    }
}
