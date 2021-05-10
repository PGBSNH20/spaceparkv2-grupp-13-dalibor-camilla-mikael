using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Utility_Classes
{
    public class ArrayBuilder
    {
        public static string[] ParkArray(Task<List<Parkings>> parkings)
        {
            var tempList = new List<string>();
            for (int i = 0; i < parkings.Result.Count; i++)
            {
                tempList.Add(
                    $"Parking Spot {parkings.Result[i].Id}. Max ship length: {parkings.Result[i].MaxLength}m. Fee: {parkings.Result[i].Fee} credits. Occupied? {parkings.Result[i].Occupied}");
            }
            string[] parks = tempList.ToArray();
            return parks;
        }
        public static string[] ShipArray(List<Starship> ship)
        {
            var tempList = new List<string>();
            for (int i = 0; i < ship.Count; i++)
            {
                tempList.Add($"{ship[i].Name} ({ship[i].Length}m)");
            }
            string[] parks = tempList.ToArray();
            return parks;
        }
        public static string[] OnLeaveArray(Task<List<Parkings>> parkings)
        {
            var tempList = new List<string>();
            for (int i = 0; i < parkings.Result.Count; i++)
            {
                tempList.Add(parkings.Result[i].Occupied == false
                    ? $"Parking Spot {parkings.Result[i].Id}."
                    : $"Parking Spot {parkings.Result[i].Id}. Occupied by {parkings.Result[i].ShipName}");
            }
            string[] parks = tempList.ToArray();
            return parks;
        }
    }
}
