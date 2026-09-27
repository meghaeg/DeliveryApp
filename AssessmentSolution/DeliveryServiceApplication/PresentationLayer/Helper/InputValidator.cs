using System.Linq;

namespace DeliveryServiceApplication.PresentationLayer.Helper
{
    public static class InputValidator
    {
        public static bool ValidateInteger(string input, out int number)
        {
            return int.TryParse(input, out number);
        }

        public static bool ValidateString(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            return input.All(ch => char.IsLetter(ch));
        }
    }
}
