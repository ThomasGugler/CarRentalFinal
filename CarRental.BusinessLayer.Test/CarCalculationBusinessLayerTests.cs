namespace CarRental.BusinessLayer.Test
{
    using System;
    using System.Globalization;
    using CarRental.Contracts;
    using CarRental.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class CarCalculationBusinessLayerTests
    {
        private CarCalculationBusinessLayer sut;
        private Mock<IContainerService> containerMock;
        private Mock<ILoggingService> loggingServiceMock;

        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            this.containerMock = new Mock<IContainerService>();
            this.loggingServiceMock = new Mock<ILoggingService>();
            this.sut = new CarCalculationBusinessLayer(this.containerMock.Object, this.loggingServiceMock.Object);
        }

        /// <summary>
        /// Testet CalculatePrice für Car
        /// </summary>
        [TestMethod]
        [TestCategory("Unit Tests")]
        public void Test_If_CarCalculationBusinessLayer_Calculates_Cars_CarA_ConsumerPremium()
        {
            // Arrange
            var customer = CustomerModel.CreateObject(CustomerType.ConsumerPremium, "Thomas", "Gugler", DateTime.Now);
            var car = CarModel.CreateObject(string.Empty, string.Empty, 0, "A");
            var requestedReservationStartDateTime = new DateTime(2017, 9, 19);
            var requestedReservationEndDateTime = new DateTime(2017, 9, 28);
            // Act
            var result = this.sut.CalculatePrice(customer, car, requestedReservationStartDateTime, requestedReservationEndDateTime);
            // Assert
            Assert.AreEqual(441.00M, result);
        }

        [TestMethod]
        [TestCategory("DataDriven")]
        [DataSource("System.Data.Odbc", "Driver={Microsoft Excel Driver (*.xls)};DriverId=790;Dbq=Testdata.xls;", "Tabelle1$", DataAccessMethod.Sequential)]
        [DeploymentItem(".\\Testdata\\Testdata.xls")]
        public void Datadriven_Test_If_CarCalculationBusinessLayer_Calculates_All_Cars_For_All_Customers()
        {
            // Arrange
            var customerType = (CustomerType)Enum.Parse(typeof(CustomerType), this.TestContext.DataRow["CustomerType"].ToString());
            var carCategory = this.TestContext.DataRow["CarCategory"].ToString();
            var reservationStartDateTime = Convert.ToDateTime(this.TestContext.DataRow["ReservationStartDateTime"], CultureInfo.CurrentCulture);
            var reservationEndDateTime = Convert.ToDateTime(this.TestContext.DataRow["ReservationEndDateTime"], CultureInfo.CurrentCulture);
            var expectedResult = Convert.ToDecimal(this.TestContext.DataRow["Result"], CultureInfo.CurrentCulture);
            var customer = CustomerModel.CreateObject(customerType, "Thomas", "Gugler", DateTime.Now);
            var car = CarModel.CreateObject(string.Empty, string.Empty, 0, carCategory);
            // Act
            var result = this.sut.CalculatePrice(customer, car, reservationStartDateTime, reservationEndDateTime);
            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}
