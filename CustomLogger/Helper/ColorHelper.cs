namespace CustomLogger.Helper
{
    internal class ColorHelper
    {
        public static string Red(string message)
        {
            return $"\u001b[31m{message}\u001b[0m";
        }

        public static string Yellow(string message)
        {
            return $"\u001b[33m{message}\u001b[0m";
        }

        public static string Green(string message)
        {
            return $"\u001b[32m{message}\u001b[0m";
        }

        public static string Orange(string message)
        {
            return $"\u001b[38;5;208m{message}\u001b[0m";
        }

        public static string Grey(string message)
        {
            return $"\u001b[90m{message}\u001b[0m";
        }
    }
}
