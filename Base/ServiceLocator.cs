namespace DG_Pack.Base {
    public class ServiceLocator {
        private static ServiceLocator _hub;
        public static ServiceLocator Hub => _hub ??= new ServiceLocator();
        
        public void RegisterSingle<TService>(TService implementation) where TService : IService =>
            Implementation<TService>.instance = implementation;
        public TService Single<TService>() where TService : IService =>
            Implementation<TService>.instance;

        private static class Implementation<TService> where TService : IService {
            public static TService instance;
        }
    }
}