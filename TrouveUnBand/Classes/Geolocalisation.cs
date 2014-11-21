using System;
using System.Collections.Generic;
using TrouveUnBand.Models;
using System.Net.Http;
using System.Web.Script.Serialization;

namespace TrouveUnBand.Classes
{
    public static class Geolocalisation
    {
        public static User SetUserLocation(User user)
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri("https://maps.googleapis.com");

            var response = client.GetAsync("/maps/api/geocode/json?address="
                                            + user.Location
                                            + ",Canada,+CA&key=AIzaSyAzPU-uqEi7U9Ry15EgLAVZ03_4rbms8Ds"
                                            ).Result;

            if (response.IsSuccessStatusCode)
            {
                string responseBody = response.Content.ReadAsStringAsync().Result;

                var location = new JavaScriptSerializer().Deserialize<LocationModels>(responseBody);
                user.Latitude = location.results[location.results.Count - 1].geometry.location.lat;
                user.Longitude = location.results[location.results.Count - 1].geometry.location.lng;
                return user;
            }
            user.Latitude = 0.0;
            user.Longitude = 0.0;
            return user;
        }

        public static bool CheckIfInRange(string locationA, string locationB, int radius)
        {
            var coordA = GetCoordinatesByLocation(locationA);
            var coordB = GetCoordinatesByLocation(locationB);
            var distance = GetDistance(coordA.latitude, coordA.longitude, coordB.latitude, coordB.longitude);

            if (distance > radius)
            {
                return false;
            }

            return true;
        }

        public static Coordinates GetCoordinatesByLocation(string location)
        {
            var coordinates = new Coordinates();
            var client = new HttpClient {BaseAddress = new Uri("https://maps.googleapis.com")};
            var response = client.GetAsync("/maps/api/geocode/json?address="
                                            + location
                                            + ",Canada,+CA&key=AIzaSyAzPU-uqEi7U9Ry15EgLAVZ03_4rbms8Ds"
                                            ).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseBody = response.Content.ReadAsStringAsync().Result;
                var returnedCoordinates = new JavaScriptSerializer().Deserialize<LocationModels>(responseBody);

                coordinates.latitude = returnedCoordinates.results[returnedCoordinates.results.Count - 1].geometry.location.lat;
                coordinates.longitude = returnedCoordinates.results[returnedCoordinates.results.Count - 1].geometry.location.lng;
                coordinates.formattedAddress = returnedCoordinates.results[returnedCoordinates.results.Count - 1].formatted_address;
            }

            return coordinates;
        }

        public static int GetDistance(double latitudeP1, double longitudeP1, double latitudeP2, double longitudeP2)
        {
            double EARTHS_MEAN_RADIUS_IN_KM = 6378.137;
            var lat = ToRadians(latitudeP2 - latitudeP1);
            var lng = ToRadians(longitudeP2 - longitudeP1);
            var h1 = Math.Sin(lat / 2) * Math.Sin(lat / 2) +
                          Math.Cos(ToRadians(latitudeP1)) * Math.Cos(ToRadians(latitudeP2)) *
                          Math.Sin(lng / 2) * Math.Sin(lng / 2);
            var h2 = 2 * Math.Asin(Math.Min(1, Math.Sqrt(h1)));
            int distance = (int)(EARTHS_MEAN_RADIUS_IN_KM * h2);

            return distance;
        }

        private static double ToRadians(double val)
        {
            return (Math.PI / 180) * val;
        }
    }

    public class Coordinates
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string formattedAddress { get; set; }

        public Coordinates()
        {
            latitude = 0.0;
            longitude = 0.0;
            formattedAddress = String.Empty;
        }
    }
}
