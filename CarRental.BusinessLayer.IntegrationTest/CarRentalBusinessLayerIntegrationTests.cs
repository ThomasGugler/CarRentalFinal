namespace CarRental.BusinessLayer.IntegrationTest
{
    using System;
    using CarRental.Contracts;
    using CarRental.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class CarRentalBusinessLayerIntegrationTests
    {
        private CarRentalBusinessLayer sut;
        private Mock<IContainerService> containerMock;
        private Mock<ILoggingService> loggingServiceMock;

        [TestInitialize]
        public void TestInitialize()
        {
            this.containerMock = new Mock<IContainerService>();
            this.loggingServiceMock = new Mock<ILoggingService>();
            var carCalculationBusinessLayer = new CarCalculationBusinessLayerFake();
            this.sut = new CarRentalBusinessLayer(this.containerMock.Object, this.loggingServiceMock.Object, carCalculationBusinessLayer);
        }

        [TestMethod]
        [TestCategory("Integration Tests")]
        public void INT_Test_If_CarRentalBusinessLayer_CalculatePrice_Returns_1234_With_Fake()
        {
            // Arrange
            var customerDummy = CustomerModel.CreateObject();
            var carDummy = CarModel.CreateObject();
            var requestedReservationStartDateTime = new DateTime(2017, 9, 20);
            var requestedReservationEndDateTime = new DateTime(2017, 9, 19);
            // Act
             var result = this.sut.CalculatePrice(customerDummy, carDummy, requestedReservationStartDateTime, requestedReservationEndDateTime);
            // Assert
            Assert.AreEqual(1234M, result);
        }
    }
}
