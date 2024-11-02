using System;
namespace BatteryTemperature
{
    class ControllerAlert
    {
        public void SendToController(BreachType breachType)
        {
            const ushort header = 0xfeed;
            Console.WriteLine($"{header} : {breachType}");
        }
    }
}
