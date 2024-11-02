using System;
using System.Collections.Generic;

namespace BatteryTemperature
{
    public class TypewiseAlert
    {
        private static readonly Dictionary<CoolingType, double> CoolingLimits = new()
        {
            { CoolingType.PassiveCooling, 35 },
            { CoolingType.MediumActiveCooling, 40 },
            { CoolingType.HighActiveCooling, 45 }
        };

        public BreachType ClassifyTemperatureBreach(CoolingType coolingType, double temperatureInC)
        {
            if (!CoolingLimits.TryGetValue(coolingType, out double upperLimit))
            {
                throw new ArgumentOutOfRangeException(nameof(coolingType), "Invalid cooling type");
            }

            return InferBreach(temperatureInC, 0, upperLimit);
        }

        public static BreachType InferBreach(double value, double lowerLimit, double upperLimit)
        {
            return value <= lowerLimit ? BreachType.TooLow :
                   value >= upperLimit ? BreachType.TooHigh :
                   BreachType.Normal;
        }
    }
}
