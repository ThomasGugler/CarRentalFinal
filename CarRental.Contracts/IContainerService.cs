namespace CarRental.Contracts
{
    public interface IContainerService
    {
        /// <summary>
        /// Wrapper fuer die Resolve Methode
        /// </summary>
        /// <typeparam name="T">Typ des Objektes</typeparam>
        /// <returns>Instanz von T</returns>
        T Resolve<T>();
    }
}
