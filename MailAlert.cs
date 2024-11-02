using System;
namespace BatteryTemperature
{
    class MailAlert
    {
        public void SendToEmail(BreachType breachType)
        {
            string recipient = "a.b@c.com";
            string message = string.Empty;

            if (breachType == BreachType.TooLow)
                message = "Hi, the temperature is too low.";
            else if (breachType == BreachType.TooHigh)
                message = "Hi, the temperature is too high.";

            if (!string.IsNullOrEmpty(message))
            {
                Console.WriteLine($"To: {recipient}\n{message}");
            }
        }
    }
}
