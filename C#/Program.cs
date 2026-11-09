using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace FunParkKassa
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Random rng = new Random();
            int totalTicketsSold = 0;
            int ticketCount;
            DateTime now = DateTime.Now;
            DateTime visitDate;
            bool running = true;

            do
            {
                Console.Clear();
                Console.WriteLine("Welkom bij FunPark");
                Console.WriteLine(now.ToShortDateString() + " " + now.ToShortTimeString());
                Console.WriteLine($"Totaal aantal tickets verkocht: {totalTicketsSold}");
                Console.WriteLine("____________________________________________________");
                Console.WriteLine("1) Nieuwe Bestelling");
                Console.WriteLine("0) Afsluiten");
                Console.Write("Maak uw keuze: ");

                string optionChoice = Console.ReadLine();

                switch (optionChoice)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Nieuwe bestelling");
                        Console.WriteLine("____________________________________________________");

                        visitDate = GetDateTimeInRange("Bezoekdatum: ", DateTime.Today, DateTime.Today + TimeSpan.FromDays(30));

                        ticketCount = GetIntegerBetween("Aantal tickets: ", 1, 10);

                        PlaceOrder(rng, visitDate, ticketCount);
                        totalTicketsSold += ticketCount;
                        PressEnterToContinue();

                        break;
                    case "0":
                        Console.WriteLine("Tot ziens!");
                        running = false;
                        break;
                    default:
                        Showtext("Ongeldige keuze!", ConsoleColor.Red);
                        PressEnterToContinue();
                        break;
                } 
            } while (running);

        }

        static void Showtext(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        static decimal CalculatePrice(int age, DateTime date)
        {
            decimal price;
            if (age <= 2)
            {
                price = 0.00m;
            }
            else if (age <= 11)
            {
                price = 15.00m;
            }
            else if (age <= 64)
            {
                price = 25.00m;
            }
            else
            {
                price = 18.00m;
            }

            return (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) ? price * 1.2m : price;
        }

        static string GenerateOrderCode(Random rng)
        {
            return $"FP-{rng.Next(1000, 10000)}";
        }

        static void PlaceOrder(Random random, DateTime visitDate, int ticketCount)
        {
            decimal runningTotal = 0;
            Console.WriteLine($"Bezoekdatum: {visitDate.ToShortDateString()}");
            Console.WriteLine($"Aantal tickets: {ticketCount}");
            for (int i = 1; i <= ticketCount; i++)
            {
                int age = GetIntegerBetween($"Leeftijd van bezoeker {i}: ", 0, 120);
                runningTotal += CalculatePrice(age, visitDate);
            }

            StringBuilder receit = new StringBuilder();
            receit.AppendLine("\n==== FUNPARK ====");
            receit.AppendLine($"Bestelcode: {GenerateOrderCode(random)}");
            receit.AppendLine($"Bezoekdatum: {visitDate.ToLongDateString()}");
            receit.AppendLine($"Aantal tickets: {ticketCount}");
            receit.Append($"Totaal: {runningTotal:c}");
            Console.WriteLine(receit);
        }

        static DateTime GetDateTimeInRange(string text, DateTime earliest, DateTime latest)
        {
            DateTime input;
            while (true)
            {
                Console.Write(text);
                if (!DateTime.TryParse(Console.ReadLine(), out input) || input < earliest || input > latest + TimeSpan.FromHours(1))
                {
                    Showtext($"Geef een datum tussen {earliest.ToShortDateString()} en {latest.ToShortDateString()}", ConsoleColor.Red);
                }
                else
                {
                    return input;
                }
            }
        }

        static int GetIntegerBetween(string text, int min, int max)
        {
            int input;
            while (true)
            {
                Console.Write(text);
                if (!int.TryParse(Console.ReadLine(), out input) || input < min || input > max)
                {
                    Showtext($"Geef een getal tussen {min} en {max}", ConsoleColor.Red);
                }
                else
                {
                    return input;
                }
            }
        }

        static void PressEnterToContinue()
        {
            Console.WriteLine("Druk enter om door te gaan...");
            while (true)
            {
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    return;
                }
            }
        }
    }
}
