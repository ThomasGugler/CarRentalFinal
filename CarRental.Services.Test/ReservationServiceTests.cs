namespace CarRental.Services.Test
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ReservationServiceTests
    {
        private ReservationService sut;

        [TestInitialize]
        public void TestInitialize()
        {
            this.sut = new ReservationService();
        }

        /// <summary>
        /// Testet FindAvailableCarsAsync: ergebnis sind 2 cars.
        /// </summary>
        [TestMethod]
        [TestCategory("Integration Tests")]
        public async Task INT_Test_If_CarRentalBusinessLayer_FindAvailableCarsAsync_Returns_2Cars()
        {
            // Arrange
            var requestedReservationStartDateTime = new DateTime(2017, 9, 20);
            var requestedReservationEndDateTime = new DateTime(2017, 9, 28);
            var cityForRequestedReservation = "Vienna";
            // Act
            var result = await this.sut.FindAvailableCarsAsync(requestedReservationStartDateTime, requestedReservationEndDateTime, cityForRequestedReservation);
            // Assert
            Assert.IsNotNull(result);
            var resultList = result.ToList();
            Assert.AreEqual(2, resultList.Count);
        }
    }
}
