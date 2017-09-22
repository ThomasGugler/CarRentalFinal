namespace CarRental.WpfClient.ViewModel
{
    using CarRental.Contracts;
    using GalaSoft.MvvmLight.Ioc;

    public class ContainerService : IContainerService
    {
        public T Resolve<T>()
        {
            return SimpleIoc.Default.GetInstance<T>();
        }
    }
}
