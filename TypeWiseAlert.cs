using System;
namespace BatteryTemperature
{
        public class TypewiseAlert
        {

            public static BreachType InferBreach(double value, double lowerLimit, double upperLimit)
            {
                return value <= lowerLimit ? BreachType.TooLow :
                       value >= upperLimit ? BreachType.TooHigh :
                       BreachType.Normal;
            }

            private const double PassiveCoolingLimit = 35;
            private const double HighActiveCoolingLimit = 45;
            private const double MediumActiveCoolingLimit = 40;

            public BreachType ClassifyTemperatureBreach(CoolingType coolingType, double temperatureInC)
            {
                double upperLimit;

                if (coolingType == CoolingType.PassiveCooling)
                    upperLimit = PassiveCoolingLimit;
                else if (coolingType == CoolingType.HighActiveCooling)
                    upperLimit = HighActiveCoolingLimit;
                else if (coolingType == CoolingType.MediumActiveCooling)
                    upperLimit = MediumActiveCoolingLimit;
                else
                    throw new ArgumentOutOfRangeException(nameof(coolingType), "Invalid cooling type");

                return InferBreach(temperatureInC, 0, upperLimit);
            }
        }

}
