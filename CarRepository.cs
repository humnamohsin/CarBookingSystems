using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CarBookingSystem.Car
{
    /// <summary>
    /// Repository Layer - Handles all file operations for Car.
    /// </summary>
    public class CarRepository
    {
        private readonly string _filePath;

        public CarRepository(string filePath = "Data/cars.txt")
        {
            _filePath = filePath;
            EnsureDataFolderAndFile();
        }

        private void EnsureDataFolderAndFile()
        {
            string directory = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(_filePath))
            {
                File.Create(_filePath).Close();
            }
        }

        public List<CarModel> GetAll()
        {
            var cars = new List<CarModel>();

            try
            {
                if (!File.Exists(_filePath))
                    return cars;

                string[] lines = File.ReadAllLines(_filePath);

                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] parts = line.Split('|');
                    if (parts.Length != 6)
                        continue;

                    if (int.TryParse(parts[2], out int price) &&
                        int.TryParse(parts[3], out int model))
                    {
                        cars.Add(new CarModel(
                            parts[0].Trim(),
                            parts[1].Trim(),
                            price,
                            model,
                            parts[4].Trim(),
                            parts[5].Trim()
                        ));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading file: " + ex.Message);
            }

            return cars;
        }

        public void SaveAll(List<CarModel> cars)
        {
            try
            {
                EnsureDataFolderAndFile();
                var lines = new List<string>();
                foreach (var c in cars)
                {
                    lines.Add(c.Name + "|" + c.Number + "|" + c.Price + "|" + c.Model + "|" + c.Colour + "|" + c.Status);
                }
                File.WriteAllLines(_filePath, lines);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving file: " + ex.Message);
            }
        }

        public void Add(CarModel car)
        {
            var cars = GetAll();
            cars.Add(car);
            SaveAll(cars);
        }

        public CarModel FindByName(string name)
        {
            return GetAll().FirstOrDefault(c =>
                c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public List<CarModel> FindByModel(int model)
        {
            return GetAll().Where(c => c.Model == model).ToList();
        }

        public bool Update(CarModel updatedCar)
        {
            var cars = GetAll();
            int index = cars.FindIndex(c =>
                c.Number.Equals(updatedCar.Number, StringComparison.OrdinalIgnoreCase));

            if (index >= 0)
            {
                cars[index] = updatedCar;
                SaveAll(cars);
                return true;
            }
            return false;
        }

        public void SeedIfEmpty()
        {
            if (GetAll().Count > 0)
                return;

            var sampleCars = new List<CarModel>
            {
                new CarModel("civic",  "ABC123", 5000000, 2022, "Black",  "Available"),
                new CarModel("audi",   "XYZ456", 4500000, 2021, "White",  "Booked"),
                new CarModel("Mehran", "LMN789", 1500000, 2018, "Silver", "Available"),
                new CarModel("City",   "DEF321", 4000000, 2020, "brown",  "Booked"),
                new CarModel("Alto",   "GHI654", 3000000, 2023, "green",  "Available"),
                new CarModel("jeep",   "JKL987", 8000000, 2022, "olive",  "Available")
            };

            SaveAll(sampleCars);
        }
    }
}
