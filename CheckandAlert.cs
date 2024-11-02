using System;
namespace BatteryTemperature
{
    class CheckandAlert
    {
        ControllerAlert controllerAlert = new ControllerAlert();
        TypewiseAlert typewiseAlerts = new TypewiseAlert();
        MailAlert mailAlert = new MailAlert();

        private readonly ControllerAlert _controllerAlert;
        private readonly TypewiseAlert _typewiseAlerts;
        private readonly MailAlert _mailAlert;

        public CheckandAlert(ControllerAlert controllerAlert, TypewiseAlert typewiseAlerts, MailAlert mailAlert)
        {
            _controllerAlert = controllerAlert;
            _typewiseAlerts = typewiseAlerts;
            _mailAlert = mailAlert;
        }

        public void CheckAndAlert(AlertTarget alertTarget, BatteryCharacter batteryChar, double temperatureInC)
        {
            BreachType breachType = typewiseAlerts.ClassifyTemperatureBreach(batteryChar.CoolingType, temperatureInC);
            SendAlert(alertTarget, breachType);
        }

        private void SendAlert(AlertTarget alertTarget, BreachType breachType)
        {
            if (alertTarget == AlertTarget.ToController)
            {
                controllerAlert.SendToController(breachType);
            }
            else if (alertTarget == AlertTarget.ToEmail)
            {
                mailAlert.SendToEmail(breachType);
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(alertTarget), "Invalid alert target");
            }
        }
    }

}
