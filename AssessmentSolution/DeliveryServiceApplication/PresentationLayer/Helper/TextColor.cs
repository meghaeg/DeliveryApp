using System;

namespace DeliveryServiceApplication.PresentationLayer.Helper
{
    public class TextColor
    {
        public static void WriteColorLine(string input, ConsoleColor consoleColor)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(input);
            Console.ResetColor();
        }
    }
}
