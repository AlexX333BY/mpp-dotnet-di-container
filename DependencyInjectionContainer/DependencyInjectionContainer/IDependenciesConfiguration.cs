namespace DependencyInjectionContainer
{
    public interface IDependenciesConfiguration
    {
        void Register<TDependency, TImplementation>(bool isSingleton = false, string name = null)
            where TDependency : class
            where TImplementation : TDependency;
    }
}
