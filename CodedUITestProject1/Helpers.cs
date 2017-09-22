namespace CodedUITestProject1
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Threading;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;

    public static class Helpers
    {
        /// <summary>
        /// sucht ein Control über seine AutomationId. Schreibt einen Trace-Eintrag über die Dauer der Suche.
        /// </summary>
        /// <typeparam name="T">Typ des gesuchten Controls.</typeparam>
        /// <param name="parent">Parent des gesuchten Controls.</param>
        /// <param name="automationId">Automation-ID des gesuchten Controls</param>
        /// <param name="timeout">Timeout der Suche, bei 0 wird der Default-Wert aus den Settings genommen</param>
        /// <returns>Das gefundene Control</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "ist ein Workaround für einen Framework-Bug")]
        public static T SearchControlByAutomationId<T>(UITestControl parent, string automationId, int timeout = 0) where T : UITestControl
        {
            try
            {
                var control = (T)Activator.CreateInstance(typeof(T), new[] { parent });
                control.SearchProperties.Add("AutomationId", automationId);

                try
                {
                    // Workaround für Framework-interne Exception
                    control.WaitForControlExist(timeout);
                }
                catch (Exception)
                {
                    // war FormatException
                }

                control.Find();
                return control;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// sucht ein Control über seinen Namen. Schreibt einen Trace-Eintrag über die Dauer der Suche.
        /// </summary>
        /// <typeparam name="T">Typ des gesuchten Controls.</typeparam>
        /// <param name="name">Name des gesuchten Controls.</param>
        /// <param name="maxWaitTime">Wie lange das Control maximal gesucht wird.</param>
        /// <returns>Das gefundene Control</returns>
        public static T SearchControlByName<T>(string name, int maxWaitTime = 60 * 1000) where T : UITestControl
        {
            try
            {
                var control = (T)Activator.CreateInstance(typeof(T));
                control.SearchProperties.Add("Name", name);
                try
                {   // Workaround für Framework-interne Exception
                    control.WaitForControlExist(maxWaitTime);
                }
                catch (System.FormatException)
                {
                }

                control.Find();
                return control;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// wartet auf ein Control und feuert einen Mouseclick-Event aus.
        /// </summary>
        /// <param name="control">Das Control auf das geklickt werden soll.</param>
        /// <param name="maxWaitTime">maximale Wartezeit bis das Control enabled ist.</param>
        /// <param name="relativeCoordinate">Relative zum control zu klickende Koordinaten.</param>
        public static void Click(this UITestControl control, int maxWaitTime = 1000, Point? relativeCoordinate = null)
        {
            // control.TrySetFocus();
            control.WaitForControlEnabled(maxWaitTime);
            // control.EnsureClickable();
            if (control.Enabled)
            {
                if (relativeCoordinate == null)
                    Mouse.Click(control);
                else
                    Mouse.Click(control, (Point)relativeCoordinate);
            }
            else
            {
                string warning = "Das UiControl ist nicht enabled!!\n AutomationId: " + ((WpfControl)control).AutomationId.ToString();
                throw new InvalidOperationException(warning);
            }
            Playback.Wait(100);
        }

        public static void LaunchApp()
        {
            string applicationPath = @"C:\Projects\CarRental\Final\CarRentalSolution\CarRental.WpfClient\bin\Debug";
            string applicationName = @"CarRental.WpfClient.exe";
            var processInfo = new ProcessStartInfo(applicationPath + "\\" + applicationName);
            Process.Start(processInfo);
            Thread.Sleep(1000);
        }

        public static void KillProcesses(params string[] processNames)
        {
            foreach (var name in processNames)
            {
                foreach (var item in Process.GetProcessesByName(name))
                {
                    item.Kill();
                    item.WaitForExit();
                }
            }
        }
    }
}
