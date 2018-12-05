using System;
using System.Collections.Generic;

namespace DependencyInjectionContainer
{
    public interface IDependenciesConfiguration
    {
        IEnumerable<ImplementationContainer> GetImplementations<TDependency>();

        void Register<TDependency, TImplementation>(bool isSingleton = false, string name = null)
            where TDependency : class
            where TImplementation : TDependency;

        void Register(Type dependency, Type implementation, bool isSingleton = false, string name = null);
    }
}
