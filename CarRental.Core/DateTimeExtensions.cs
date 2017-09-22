namespace CarRental.Core
{
    using System;
    using System.Globalization;

    /// <summary>
    /// DateTime Service
    /// </summary>
    public static class DateTimeService
    {
        private static DateTime minSqlDateTime = new DateTime(1753, 1, 1);
        private static DateTime maxDateSqlTime = new DateTime(9999, 12, 31, 23, 59, 59, 997);
        private static Func<DateTimeOffset> provider = () => DateTimeOffset.Now;
        private static DateTimeOffset providerValue = DateTimeOffset.Now;
        private static DateTimeOffset serviceCallTimestamp = DateTimeOffset.Now;

        /// <summary>
        /// Holt aktuelles Datum und Uhrzeit
        /// </summary>
        public static DateTime Now => DateTimeService.CurrentValue.DateTime;

        /// <summary>
        /// Holt aktuelles Datum und Uhrzeit
        /// </summary>
        public static DateTime Today => DateTimeService.CurrentValue.Date;

        /// <summary>
        /// Holt oder setzt aktuellen Provider
        /// </summary>
        public static Func<DateTimeOffset> Provider
        {
            get
            {
                return DateTimeService.provider;
            }

            set
            {
                DateTimeService.provider = value;
                DateTimeService.Update();
            }
        }

        private static DateTimeOffset CurrentValue => DateTimeService.providerValue.Add(DateTimeOffset.Now - DateTimeService.serviceCallTimestamp);

        /// <summary>
        /// Aktualisiert den Wert in ProviderValue
        /// </summary>
        public static void Update()
        {
            DateTimeService.providerValue = DateTimeService.Provider();
            DateTimeService.serviceCallTimestamp = DateTimeOffset.Now;
        }

        /// <summary>
        /// Liefert die Anzahl an Jahren zwischen zwei Daten, unter Berücksichtigung von Schaltjahren.
        /// </summary>
        /// <param name="startDate">Das StartDatum.</param>
        /// <param name="endDate">Das EndDatum.</param>
        /// <returns>Die Anzahl an Jahren.</returns>
        public static bool IsDifferenceMinimumOneYear(DateTime startDate, DateTime endDate)
        {
            return startDate.AddYears(1) <= endDate;
        }

        /// <summary>
        /// Liefert das größere von 2 Datumswerten.
        /// </summary>
        /// <param name="firstDate">Das erste Datum.</param>
        /// <param name="secondDate">Das zweite Datum.</param>
        /// <returns>Das größere Datum</returns>
        public static DateTime MaxDateTime(DateTime firstDate, DateTime secondDate)
        {
            return firstDate > secondDate ? firstDate : secondDate;
        }

        /// <summary>
        /// Liefert das kleinere von 2 Datumswerten.
        /// </summary>
        /// <param name="firstDate">Das erste Datum.</param>
        /// <param name="secondDate">Das zweite Datum.</param>
        /// <returns>Das größere Datum</returns>
        public static DateTime MinDateTime(DateTime firstDate, DateTime secondDate)
        {
            return firstDate < secondDate ? firstDate : secondDate;
        }

        /// <summary>
        /// Liefert den Monatsletzen (=Ultimo) des Vormonats zu dem Input-Datum retour.
        /// </summary>
        /// <param name="date">date param</param>
        /// <returns>Ultimo des Vormonats zu date param</returns>
        public static DateTime GetUltimoFromPreviousMonth(DateTime date)
        {
            return GetUltimoFromDateTime(date.AddMonths(-1));
        }

        /// <summary>
        /// Liefert den Monatsletzen (=Ultimo) des angegebenen Datum retour.
        /// </summary>
        /// <param name="date">Das Datum.</param>
        /// <returns>Ultimo des angegebenen Datums.</returns>
        public static DateTime GetUltimoFromDateTime(DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
        }

        /// <summary>
        /// Liefert den letzten Arbeitstag (Mo-Fr) vor dem angegebenen Datum. Feiertage werden dabei nicht berücksichtigt.
        /// </summary>
        /// <param name="date">Datum Parameter</param>
        /// <returns>Das Datum des letzten Arbeitstages kleiner dem Eingabedatum</returns>
        public static DateTime GetLastWorkdayBeforeDate(DateTime date)
        {
            date = date.Date;
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return date.AddDays(-3);
                case DayOfWeek.Sunday:
                    return date.AddDays(-2);
                default:
                    return date.AddDays(-1);
            }
        }

        /// <summary>
        /// Liefert das Datum zum ersten des Monats.
        /// </summary>
        /// <param name="date">Das Basisdatum.</param>
        /// <returns>Das Datum zum ersten des Monats.</returns>
        public static DateTime GetFirstDayOfMonthDate(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        /// <summary>
        /// Ermittelt, ob zwei angegebene Monate ein Quartal auseinander liegen.
        /// </summary>
        /// <param name="actualMonth">Der aktuelle Monat.</param>
        /// <param name="referenceMonth">Der Referenzmonat.</param>
        /// <returns>True, wenn ein Quartal dazwischen liegt, sonst false.</returns>
        public static bool IsQuartal(int actualMonth, int referenceMonth)
        {
            return IsPeriodBetween(actualMonth, referenceMonth, 3);
        }

        /// <summary>
        /// Ermittelt, ob zwei angegebene Monate n Monate auseinander liegen
        /// </summary>
        /// <param name="actualMonth">Der aktuelle Monat</param>
        /// <param name="referenceMonth">Der Referenzmonat.</param>
        /// <param name="periodLengthInMonths">Anzahl der Monate in der Periode</param>
        /// <returns>True, wenn n Monate auseinander liegen</returns>
        public static bool IsPeriodBetween(int actualMonth, int referenceMonth, int periodLengthInMonths)
        {
            return Math.Abs(actualMonth - referenceMonth) % periodLengthInMonths == 0;
        }

        /// <summary>
        /// Formatiert einen Datumswert als LongTimeString und ersetzt Zeichen, die eine Verwendung als Dateiname verhindern.
        /// </summary>
        /// <param name="value">Der zu formatierende Datumswert.</param>
        /// <returns>Den LongTimeString zur Verwendung als Dateiname.</returns>
        public static string ToLongTimeStringAsValidFileName(this DateTime value)
        {
            return value.ToString("HH_mm_ss", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Formatiert einen Datumswert als DateTimeString und ersetzt Zeichen, die eine Verwendung als Dateiname verhindern.
        /// </summary>
        /// <param name="value">Der zu formatierende Datumswert.</param>
        /// <returns>Den LongTimeString zur Verwendung als Dateiname.</returns>
        public static string ToDateTimeStringAsValidFileName(this DateTime value)
        {
            return value.ToString("dd_MM_yyyy_HH_mm_ss", System.Globalization.CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Überprüft, ob der Datumswert im Bereich des MSSQL-DateTime Bereiches liegt.
        /// </summary>
        /// <param name="value">Der zu überprüfende Datumswert.</param>
        /// <returns>True, wenn der Datumswert im Bereich des MSSQL-DateTime Bereiches liegt oder null ist, sonst false.</returns>
        public static bool IsInDbRange(this DateTime value)
        {
            return value >= minSqlDateTime && value <= maxDateSqlTime;
        }
    }
}
