using TraceIP.Application.Interfaces;

namespace TraceIP.Application.Services
{
    public class HaversineService : IHaversineService
    {
        public int CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            // Convertir grados a radianes
            double lat1Rad = DegreesToRadians(lat1);
            double lon1Rad = DegreesToRadians(lon1);
            double lat2Rad = DegreesToRadians(lat2);
            double lon2Rad = DegreesToRadians(lon2);

            // Diferencias entre latitudes y longitudes
            double deltaLat = lat2Rad - lat1Rad;
            double deltaLon = lon2Rad - lon1Rad;

            // Fórmula de Haversine
            double a = Math.Pow(Math.Sin(deltaLat / 2), 2) +
                       Math.Cos(lat1Rad) * Math.Cos(lat2Rad) * Math.Pow(Math.Sin(deltaLon / 2), 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            // Distancia final
            double distance = 6371 * c;
            return Convert.ToInt32(distance);
        }

        private double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }
    }
}
