using System;

namespace CarBookingSystem.Car
{
    /// <summary>
    /// Model class for Car entity.
    /// </summary>
    public class CarModel
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public int Price { get; set; }
        public int Model { get; set; }
        public string Colour { get; set; }
        public string Status { get; set; }

        public CarModel()
        {
            Name = string.Empty;
            Number = string.Empty;
            Colour = string.Empty;
            Status = "Available";
        }

        public CarModel(string name, string number, int price, int model, string colour, string status)
        {
            Name = name;
            Number = number;
            Price = price;
            Model = model;
            Colour = colour;
            Status = status;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Number: {Number}, Price: {Price}, Model: {Model}, Colour: {Colour}, Status: {Status}";
        }
    }
}
