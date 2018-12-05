using System;
using System.Collections.Generic;

namespace DependencyInjectionContainer
{
    public class DependenciesConfiguration : IDependenciesConfiguration
    {
        internal readonly Dictionary<Type, List<ImplementationContainer>> implementations;

        public void Register<TDependency, TImplementation>(bool isSingleton = false, string name = null)
            where TDependency : class
            where TImplementation : TDependency
        {
            Register(typeof(TDependency), typeof(TImplementation), isSingleton, name);
        }

        public void Register(Type dependency, Type implementation, bool isSingleton = false, string name = null)
        {
            if (dependency.IsValueType || implementation.IsValueType)
            {
                throw new ArgumentException("Types should be reference types");
            }

            if (!dependency.IsAssignableFrom(implementation))
            {
                throw new ArgumentException("Dependency is not assignable from implementation");
            }

            if (implementation.IsGenericTypeDefinition && isSingleton)
            {
                throw new ArgumentException("Open generic cannot be singleton");
            }

            ImplementationContainer container = new ImplementationContainer(implementation, isSingleton, name);

            if (!implementations.TryGetValue(dependency, out List<ImplementationContainer> dependencyImplementations))
            {
                dependencyImplementations = new List<ImplementationContainer>();
                implementations[dependency] = dependencyImplementations;
            }

            if (name != null)
            {
                dependencyImplementations.RemoveAll((existingContainer) => existingContainer.Name == name);
            }
            dependencyImplementations.Add(container);
        }

        public DependenciesConfiguration()
        {
            implementations = new Dictionary<Type, List<ImplementationContainer>>();
        }
    }
}
