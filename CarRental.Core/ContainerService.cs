namespace CarRental.Core
{
    using System.Diagnostics;
    using CarRental.Contracts;
    using Castle.Windsor;

    public class ContainerService : IContainerService
    {
        private readonly IWindsorContainer container;

        public ContainerService(IWindsorContainer container)
        {
            this.container = container;
        }

        /// <summary>
        /// Wrapper fuer die Resolve Methode
        /// </summary>
        /// <typeparam name="T">Typ des Objektes</typeparam>
        /// <returns>Instanz von T</returns>
        [DebuggerHidden]
        public T Resolve<T>()
        {
            return this.container.Resolve<T>();
        }
    }
}
