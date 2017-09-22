namespace CodedUITestProject1
{
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class CodedUITest1
    {
        public TestContext TestContext { get; set; }

        /// <summary>
        /// TestInitialize Methode, wird vor jeder CodedUI TestMethod aufgerufen
        /// </summary>
        [TestInitialize]
        public void MyTestInitialize()
        {
            Helpers.LaunchApp();
        }

        /// <summary>
        /// TestCleanup Methode, wird nach jeder CodedUI TestMethod aufgerufen
        /// </summary>
        [TestCleanup]
        public void MyTestCleanup()
        {
            Helpers.KillProcesses("MainWindow");
        }

        [TestMethod]
        [TestCategory("Coded UI Test")]
        public void CodedUITest_If_FindAvailableCarsForRentalCommand_Loads_2_Items()
        {
            var mainWindow = Helpers.SearchControlByName<WpfWindow>("MainWindow", 1000);
            var liste = Helpers.SearchControlByAutomationId<WpfList>(mainWindow, "ListBoxCars");
            // Liste ist intial leer
            Assert.AreEqual(0, liste.Items.Count);
            // Button klicken und 2 Items sollten da sein...
            var button = Helpers.SearchControlByAutomationId<WpfButton>(mainWindow, "FindAvailableCarsForRentalCommand");
            button.Click();
            Assert.AreEqual(2, liste.Items.Count);
        }
    }
}
