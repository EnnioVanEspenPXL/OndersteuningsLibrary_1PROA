using System.ComponentModel.Design;
using System;
using System.Text;

namespace Galactic_Express
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            bool isValid;
            string travelerName;
            string travelerDestination;
            DateTime departureDate;
            double weightOfBagage;
            string destinationCode;
            string bookingCode;
            TimeSpan daysUntilDeparture;
            double basePrice;
            double bagagePrice;
            double totalPrice;
            string gate;
            string row;
            string seat;
            string controleCode;
            Random random = new Random();

           // Opvragen informatie van user
            do
            {
                isValid = true;
                Console.Write("Naam: ");
                travelerName = Console.ReadLine().Trim();
                if (travelerName.Length == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Geef een geldige naam in");
                    Console.ResetColor();
                    isValid = false;
                }
            }
            while (!isValid);

            do
            {
                isValid = true;
                Console.Write("Bestemming: ");
                travelerDestination = Console.ReadLine().Trim();
                if (travelerDestination.Length < 3)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Geef een bestemming naam met minstens 3 tekens.");
                    Console.ResetColor();
                    isValid = false;

                }
            }
            while (!isValid);

            do
            {
                isValid = true;
                Console.Write("Vertrek datum (yyyy-mm-dd): ");
                bool isValidDepartureDate = DateTime.TryParse(Console.ReadLine(), out departureDate);
                if (!isValidDepartureDate || departureDate <= DateTime.Today)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Geef een geldige datum in.");
                    Console.ResetColor();
                    isValid = false;

                }
            }
            while (!isValid);

            do
            {
                isValid = true;
                Console.Write("Gewicht van bagage (kg): ");
                bool isValidBagageWeight = double.TryParse(Console.ReadLine(), out weightOfBagage);
                if (!isValidBagageWeight)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Geef een geldig gewicht in");
                    Console.ResetColor();
                    isValid = false;

                }
            }
            while (!isValid);

            // Opschooning/berekening gegevens
            destinationCode = travelerDestination.Substring(0, 3).ToUpper();
            bookingCode = travelerName.Replace(" ", "-");
            daysUntilDeparture = departureDate - DateTime.Today;
            basePrice = 89.95;
            bagagePrice = weightOfBagage * 1.75;
            totalPrice = daysUntilDeparture < TimeSpan.FromDays(7)
                ? Math.Round(basePrice + bagagePrice + 12.50, 2, MidpointRounding.AwayFromZero)
                : Math.Round(basePrice + bagagePrice, 2, MidpointRounding.AwayFromZero);
            gate = $"{random.Next(13)}";
            row = $"{random.Next(1, 31)}";
            seat = $"{random.Next(1, 7)}";
            controleCode = $"{random.Next(1000, 10000)}";


            // Print boarding pass
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("===========================================");
            sb.AppendLine("         GALACTIC EXPRESS");
            sb.AppendLine("          BOARDING PASS");
            sb.AppendLine("===========================================");
            sb.AppendLine("");
            sb.AppendLine($"Reiziger\t: {travelerName}");
            sb.AppendLine($"Bestemming\t: {travelerDestination}");
            sb.AppendLine($"Code\t\t: {destinationCode}");
            sb.AppendLine($"Vertrekdatum\t: {departureDate.ToShortDateString()}");
            sb.AppendLine($"Vertrekdag\t: {departureDate.DayOfWeek}");
            sb.AppendLine("");
            sb.AppendLine($"Gate\t\t: {gate}");
            sb.AppendLine($"Stoel\t\t: Rij {row} - Rij {seat}");
            sb.AppendLine($"Controlecode\t: {controleCode}");
            sb.AppendLine("");
            sb.AppendLine($"Bagage\t\t: {weightOfBagage} kg");
            sb.AppendLine($"Totale prijs\t: {totalPrice:c}");
            sb.AppendLine($"Boekingscode\t: {bookingCode}");
            sb.AppendLine("");
            sb.AppendLine("===========================================");

            Console.WriteLine(sb);












        }
    }
}
