namespace CarRental.BusinessLayer.Test
{
    using System;
    using System.Collections.Generic;
    using CarRental.Contracts;
    using CarRental.Core;
    using CarRental.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class CarRentalBusinessLayerTests
    {
        private CarRentalBusinessLayer sut;
        private Mock<IContainerService> containerMock;
        private Mock<ICarCalculationBusinessLayer> carCalculationBusinessLayerMock;
        private Mock<ILoggingService> loggingServiceMock;

        [TestInitialize]
        public void TestInitialize()
        {
            this.containerMock = new Mock<IContainerService>();
            this.loggingServiceMock = new Mock<ILoggingService>();
            this.carCalculationBusinessLayerMock = new Mock<ICarCalculationBusinessLayer>();
            this.sut = new CarRentalBusinessLayer(this.containerMock.Object, this.loggingServiceMock.Object, this.carCalculationBusinessLayerMock.Object);
        }

        /// <summary>
        /// Testet CreateCarReservation für den Fehlerfall.
        /// </summary>
        [TestMethod]
        // [ExpectedException(typeof(InvalidOperationException))]
        [TestCategory("Unit Tests")]
        public void Test_If_CarRentalBusinessLayer_CreateCarReservation_Throws_Exception()
        {
            // Arrange Mocks
            var reservationServiceMock = new Mock<IReservationService>();
            this.containerMock.Setup(x => x.Resolve<IReservationService>()).Returns(reservationServiceMock.Object);
            var customerDummy = CustomerModel.CreateObject();
            var requestedReservationStartDateTimeDummy = DateTime.Now;
            var requestedReservationEndDateTimeDummy = DateTime.Now;
            DateTimeService.Provider = () => new DateTime(1977, 3, 27);
            var cityDummy = "bla";
            reservationServiceMock.Setup(x => x.TakeCarReservervation(customerDummy, requestedReservationStartDateTimeDummy, requestedReservationEndDateTimeDummy, cityDummy)).Throws(new InvalidOperationException());
            // Act
            var result = this.sut.CreateCarReservation(customerDummy, requestedReservationStartDateTimeDummy, requestedReservationEndDateTimeDummy, cityDummy);
            // Assert
            Assert.IsFalse(result);
            this.loggingServiceMock.Verify(x => x.LogError(It.IsAny<string>()), Times.Once());
        }

        /// <summary>
        /// Testet CreateCarReservation: wird richtig aufgerufen.
        /// </summary>
        [TestMethod]
        [TestCategory("Unit Tests")]
        public void Test_If_CarRentalBusinessLayer_CreateCarReservation_CallsIReservationService_TakeCarReservervation()
        {
            // Arrange Mocks
            var reservationServiceMock = new Mock<IReservationService>();
            this.containerMock.Setup(x => x.Resolve<IReservationService>()).Returns(reservationServiceMock.Object);
            var customerDummy = CustomerModel.CreateObject();
            var requestedReservationStartDateTimeDummy = DateTime.Now;
            var requestedReservationEndDateTimeDummy = DateTime.Now;
            DateTimeService.Provider = () => new DateTime(1977, 3, 27);
            var cityDummy = "bla";
            reservationServiceMock.Setup(x => x.TakeCarReservervation(customerDummy, requestedReservationStartDateTimeDummy, requestedReservationEndDateTimeDummy, cityDummy)).Returns(true);
            // Act
            var result = this.sut.CreateCarReservation(customerDummy, requestedReservationStartDateTimeDummy, requestedReservationEndDateTimeDummy, cityDummy);
            // Assert
            Assert.IsTrue(result);
            reservationServiceMock.Verify(x => x.TakeCarReservervation(customerDummy, requestedReservationStartDateTimeDummy, requestedReservationEndDateTimeDummy, cityDummy), Times.Once());
            this.loggingServiceMock.Verify(x => x.LogError(It.IsAny<string>()), Times.Never());
        }

        /// <summary>
        /// Testet CreateNewCustomer.
        /// </summary>
        [TestMethod]
        [TestCategory("Unit Tests")]
        public void Test_If_CarRentalBusinessLayer_CreateNewCustomer_Creates_New_Customer()
        {
            // Arrange Mocks
            var customerServiceMock = new Mock<ICustomerService>();
            this.containerMock.Setup(x => x.Resolve<ICustomerService>()).Returns(customerServiceMock.Object);
            // Arrange Data
            var firstName = "Thomas";
            var lastName = "Gugler;";
            var dateOfBirth = new DateTime(1977, 3, 27);
            var street = "Antonsgasse 6";
            var city = "Baden bei Wien";
            var postcode = "2500";
            var customerType = CustomerType.Consumer;
            // Act
            this.sut.CreateNewCustomer(firstName, lastName, dateOfBirth, street, city, postcode, customerType);
            // Asser
            customerServiceMock.Verify(x => x.AddCustomer(It.IsAny<CustomerModel>()), Times.Once());
        }

        /// <summary>
        /// Testst ob CalculatePrice richtig aufgerufen wird.
        /// </summary>
        [TestMethod]
        [TestCategory("Unit Tests")]
        public void Test_If_CarRentalBusinessLayer_FindAvailableCarsForRental_Returns_Result_From_CalculatePrice()
        {
            // Arrange Mocks
            var reservationServiceMock = new Mock<IReservationService>();
            this.containerMock.Setup(x => x.Resolve<IReservationService>()).Returns(reservationServiceMock.Object);
            // Arrange Data
            var customer = CustomerModel.CreateObject(CustomerType.Business, "Thomas", "Gugler", new DateTime(1977, 3, 27));
            var inputStartDate = new DateTime(2017, 9, 18);
            var inputEndDate = new DateTime(2017, 9, 23);
            var citydummy = "blabla";
            // Arrange Mock mit Data: 2 Cars
            var expectedCarListe = new List<CarModel>() { CarModel.CreateObject(), CarModel.CreateObject() };
            reservationServiceMock.Setup(x => x.FindAvailableCars(inputStartDate, inputEndDate, citydummy)).Returns(expectedCarListe);
            this.carCalculationBusinessLayerMock.Setup(x => x.CalculatePrice(customer, expectedCarListe[0], inputStartDate, inputEndDate)).Returns(10M);
            this.carCalculationBusinessLayerMock.Setup(x => x.CalculatePrice(customer, expectedCarListe[1], inputStartDate, inputEndDate)).Returns(20M);
            // Act
            var result = this.sut.FindAvailableCarsForRental(customer, inputStartDate, inputEndDate, citydummy);
            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(10, result[expectedCarListe[0]]);
            Assert.AreEqual(20, result[expectedCarListe[1]]);
        }
    }
}
