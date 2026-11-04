using System;
using System.Collections.Generic;
using System.Linq;

namespace CarBookingSystem.Car
{
    /// <summary>
    /// Service Layer - Contains business logic.
    /// </summary>
    public class CarService
    {
        private readonly CarRepository _repository;

        public int TotalSales { get; private set; }
        public int TotalRevenue { get; private set; }

        public CarService(CarRepository repository)
        {
            _repository = repository;
            _repository.SeedIfEmpty();
            RecalculateTotals();
        }

        private void RecalculateTotals()
        {
            var bookedCars = _repository.GetAll()
                .Where(c => c.Status.Equals("Booked", StringComparison.OrdinalIgnoreCase));
            TotalSales = bookedCars.Count();
            TotalRevenue = bookedCars.Sum(c => c.Price);
        }

        public List<CarModel> GetAllCars()
        {
            return _repository.GetAll();
        }

        public List<CarModel> GetAvailableCars()
        {
            return _repository.GetAll()
                .Where(c => c.Status.Equals("Available", StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public bool AddCar(string name, string number, int price, int model, string colour, string status)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(number))
                return false;

            if (price <= 0)
                return false;

            if (_repository.GetAll().Any(c => c.Number.Equals(number, StringComparison.OrdinalIgnoreCase)))
                return false;

            string safeStatus = string.IsNullOrWhiteSpace(status) ? "Available" : status.Trim();
            var car = new CarModel(name.Trim(), number.Trim(), price, model, colour.Trim(), safeStatus);
            _repository.Add(car);
            return true;
        }

        public CarModel SearchByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;
            return _repository.FindByName(name.Trim());
        }

        public List<CarModel> SearchByModel(int model)
        {
            return _repository.FindByModel(model);
        }

        public bool BookCar(string carName)
        {
            var car = _repository.FindByName(carName);
            if (car == null)
                return false;

            if (!car.Status.Equals("Available", StringComparison.OrdinalIgnoreCase))
                return false;

            car.Status = "Booked";
            bool updated = _repository.Update(car);

            if (updated)
            {
                TotalSales++;
                TotalRevenue += car.Price;
            }
            return updated;
        }
        public List<CarStatus> QuickFilterByStatus(string status)
        {
            foreach (string line in lines)
            {
                return GetAll().Where(c => c.Status == "Booked").ToList()
                }
        }

        public bool AuthenticateAdmin(string username, string password)
        {
            return username == "1234" && password == "admin";
        }
    }
}
