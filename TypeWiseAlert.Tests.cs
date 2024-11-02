using System;
using Xunit;
namespace BatteryTemperature.Tests
{
    public class TypewiseAlertTests
    {
        [Theory]
        [InlineData(CoolingType.PassiveCooling, 34, BreachType.TooLow)]
        [InlineData(CoolingType.PassiveCooling, 35, BreachType.Normal)]
        [InlineData(CoolingType.PassiveCooling, 36, BreachType.TooHigh)]
        [InlineData(CoolingType.MediumActiveCooling, 39, BreachType.TooLow)]
        [InlineData(CoolingType.MediumActiveCooling, 40, BreachType.Normal)]
        [InlineData(CoolingType.MediumActiveCooling, 41, BreachType.TooHigh)]
        [InlineData(CoolingType.HighActiveCooling, 44, BreachType.TooLow)]
        [InlineData(CoolingType.HighActiveCooling, 45, BreachType.Normal)]
        [InlineData(CoolingType.HighActiveCooling, 46, BreachType.TooHigh)]
        public void ClassifyTemperatureBreach_ValidInputs_ReturnsExpectedBreachType(CoolingType coolingType, double temperature, BreachType expected)
        {
            var typewiseAlert = new TypewiseAlert();
            var result = typewiseAlert.ClassifyTemperatureBreach(coolingType, temperature);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ClassifyTemperatureBreach_InvalidCoolingType_ThrowsArgumentOutOfRangeException()
        {
            var typewiseAlert = new TypewiseAlert();
            var invalidCoolingType = (CoolingType)99;        // Assuming 99 is not a valid CoolingType
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
                typewiseAlert.ClassifyTemperatureBreach(invalidCoolingType, 30));

            Assert.Equal("coolingType", exception.ParamName);
            Assert.Equal("Invalid cooling type", exception.Message);
        }
    }

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

            // Use the mocks in your CheckandAlert class
            _checkAndAlert = new CheckandAlert(
                _mockControllerAlert.Object,
                _mockTypewiseAlert.Object,
                _mockMailAlert.Object);
        }

        [Fact]
        public void CheckAndAlert_SendsToController_WhenAlertTargetIsToController()
        {
            // Arrange
            var batteryChar = new BatteryCharacter { CoolingType = CoolingType.PassiveCooling };
            var breachType = BreachType.Normal;

            _mockTypewiseAlert.Setup(x => x.ClassifyTemperatureBreach(batteryChar.CoolingType, It.IsAny<double>()))
                .Returns(breachType);

            // Act
            _checkAndAlert.CheckAndAlert(AlertTarget.ToController, batteryChar, 30);

            // Assert
            _mockControllerAlert.Verify(x => x.SendToController(breachType), Times.Once);
            _mockMailAlert.Verify(x => x.SendToEmail(It.IsAny<BreachType>()), Times.Never);
        }

        [Fact]
        public void CheckAndAlert_SendsToEmail_WhenAlertTargetIsToEmail()
        {
            var batteryChar = new BatteryCharacter { CoolingType = CoolingType.HighActiveCooling };
            var breachType = BreachType.TooHigh;
            _mockTypewiseAlert.Setup(x => x.ClassifyTemperatureBreach(batteryChar.CoolingType, It.IsAny<double>()))
                .Returns(breachType);
            _checkAndAlert.CheckAndAlert(AlertTarget.ToEmail, batteryChar, 50);
            _mockMailAlert.Verify(x => x.SendToEmail(breachType), Times.Once);
            _mockControllerAlert.Verify(x => x.SendToController(It.IsAny<BreachType>()), Times.Never);
        }

        [Fact]
        public void CheckAndAlert_ThrowsException_WhenAlertTargetIsInvalid()
        {
            var batteryChar = new BatteryCharacter { CoolingType = CoolingType.MediumActiveCooling };
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
                _checkAndAlert.CheckAndAlert((AlertTarget)99, batteryChar, 30)); // Assuming 99 is invalid
            Assert.Equal("alertTarget", exception.ParamName);
            Assert.Equal("Invalid alert target", exception.Message);
        }
    }
}

