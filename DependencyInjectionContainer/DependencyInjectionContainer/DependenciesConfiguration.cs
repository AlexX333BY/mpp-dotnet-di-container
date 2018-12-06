using System;
using System.Collections.Generic;
using DependencyInjectionContainer.Extensions;

namespace DependencyInjectionContainer
{
    public class DependenciesConfiguration : IDependenciesConfiguration
    {
        protected readonly Dictionary<Type, List<ImplementationContainer>> implementations;

        public void Register<TDependency, TImplementation>(bool isSingleton = false, string name = null)
            where TDependency : class
            where TImplementation : TDependency
        {
            Register(typeof(TDependency), typeof(TImplementation), isSingleton, name);
        }

        public void Register(Type dependency, Type implementation, bool isSingleton = false, string name = null)
        {
            if (dependency.IsGenericTypeDefinition ^ implementation.IsGenericTypeDefinition)
            {
                throw new ArgumentException("Open generics register should be with both open generic types");
            }

            if (dependency.IsGenericTypeDefinition)
            {
                if (isSingleton)
                {
                    throw new ArgumentException("Open generic cannot be singleton");
                }

                if (!dependency.IsAssignableFromAsOpenGeneric(implementation))
                {
                    throw new ArgumentException("Dependency is not assignable from implementation");
                }
            }
            else
            {
                if (!dependency.IsClass && !dependency.IsAbstract && !dependency.IsInterface || (!implementation.IsClass || implementation.IsAbstract))
                {
                    throw new ArgumentException("Wrong types");
                }

                if (!dependency.IsAssignableFrom(implementation))
                {
                    throw new ArgumentException("Dependency is not assignable from implementation");
                }
            }

            ImplementationContainer container = new ImplementationContainer(implementation, isSingleton, name);
            if (dependency.IsGenericType)
            {
                dependency = dependency.GetGenericTypeDefinition();
            }

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

        public IEnumerable<ImplementationContainer> GetImplementations(Type type)
        {
            if (type.IsGenericType)
            {
                type = type.GetGenericTypeDefinition();
            }

            if (implementations.TryGetValue(type, out List<ImplementationContainer> dependencyImplementations))
            {
                return dependencyImplementations;
            }
            else
            {
                return new List<ImplementationContainer>();
            }
        }

        public DependenciesConfiguration()
        {
            implementations = new Dictionary<Type, List<ImplementationContainer>>();
        }
    }
}
