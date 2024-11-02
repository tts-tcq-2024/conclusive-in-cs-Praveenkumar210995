//using Moq;
using System;
using Xunit;

namespace BatteryTemperature.Tests
{
    public class TypewiseAlertTests
    {
        private readonly TypewiseAlert _typewiseAlert;

        public TypewiseAlertTests()
        {
            _typewiseAlert = new TypewiseAlert();
        }

        [Theory]
        [InlineData(CoolingType.PassiveCooling, 0, BreachType.TooLow)]
        [InlineData(CoolingType.PassiveCooling, 34, BreachType.Normal)]
        [InlineData(CoolingType.PassiveCooling, 35, BreachType.TooHigh)]
        [InlineData(CoolingType.PassiveCooling, 36, BreachType.TooHigh)]
        [InlineData(CoolingType.MediumActiveCooling, 0, BreachType.TooLow)]
        [InlineData(CoolingType.MediumActiveCooling, 39, BreachType.Normal)]
        [InlineData(CoolingType.MediumActiveCooling, 40, BreachType.TooHigh)]
        [InlineData(CoolingType.MediumActiveCooling, 41, BreachType.TooHigh)]
        [InlineData(CoolingType.HighActiveCooling, 0, BreachType.TooLow)]
        [InlineData(CoolingType.HighActiveCooling, 44, BreachType.Normal)]
        [InlineData(CoolingType.HighActiveCooling, 45, BreachType.TooHigh)]
        [InlineData(CoolingType.HighActiveCooling, 46, BreachType.TooHigh)]
        public void ClassifyTemperatureBreach_ValidInputs_ReturnsExpectedBreachType(CoolingType coolingType, double temperature, BreachType expected)
        {
            var result = _typewiseAlert.ClassifyTemperatureBreach(coolingType, temperature);
            Assert.Equal(expected, result);
        }
    }
    /*
        public class CheckAndAlertTests
        {
            private readonly Mock<ControllerAlert> _mockControllerAlert;
            private readonly Mock<TypewiseAlert> _mockTypewiseAlert;
            private readonly Mock<MailAlert> _mockMailAlert;
            private readonly CheckandAlert _checkAndAlert;

            public CheckAndAlertTests()
            {
                _mockControllerAlert = new Mock<ControllerAlert>();
                _mockTypewiseAlert = new Mock<TypewiseAlert>();
                _mockMailAlert = new Mock<MailAlert>();
                _checkAndAlert = new CheckandAlert(
                    _mockControllerAlert.Object,
                    _mockTypewiseAlert.Object,
                    _mockMailAlert.Object);
            }

            [Theory]
            [InlineData(AlertTarget.ToController, CoolingType.PassiveCooling, 30, BreachType.Normal)]
            [InlineData(AlertTarget.ToEmail, CoolingType.HighActiveCooling, 50, BreachType.TooHigh)]
            public void CheckAndAlert_SendsAlertsBasedOnTarget(AlertTarget alertTarget, CoolingType coolingType, double temperature, BreachType breachType)
            {
                var batteryChar = new BatteryCharacter { CoolingType = coolingType };
                _mockTypewiseAlert.Setup(x => x.ClassifyTemperatureBreach(coolingType, temperature)).Returns(breachType);

                _checkAndAlert.CheckAndAlert(alertTarget, batteryChar, temperature);

                if (alertTarget == AlertTarget.ToController)
                {
                    _mockControllerAlert.Verify(x => x.SendToController(breachType), Times.Once);
                    _mockMailAlert.Verify(x => x.SendToEmail(It.IsAny<BreachType>()), Times.Never);
                }
                else if (alertTarget == AlertTarget.ToEmail)
                {
                    _mockMailAlert.Verify(x => x.SendToEmail(breachType), Times.Once);
                    _mockControllerAlert.Verify(x => x.SendToController(It.IsAny<BreachType>()), Times.Never);
                }
            }

        }
        */
}
