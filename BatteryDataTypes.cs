namespace BatteryTemperature
{
    public enum BreachType
    {
        Normal,
        TooLow,
        TooHigh
    };

    public enum CoolingType
    {
        PassiveCooling,
        HighActiveCooling,
        MediumActiveCooling
    };

    public enum AlertTarget
    {
        ToController,
        ToEmail
    };
}
