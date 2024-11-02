using System;
using System.Collections.Generic;

namespace BatteryTemperature
{
    class MailAlert
    {
        private static readonly Dictionary<BreachType, string> BreachMessages = new()
        {
            { BreachType.TooLow, "Hi, the temperature is too low." },
            { BreachType.TooHigh, "Hi, the temperature is too high." }
        };

        public void SendToEmail(BreachType breachType)
        {
            string recipient = "a.b@c.com";
            string message = BreachMessages.GetValueOrDefault(breachType, string.Empty);

            if (!string.IsNullOrEmpty(message))
            {
                Console.WriteLine($"To: {recipient}\n{message}");
            }
        }
    }
}
