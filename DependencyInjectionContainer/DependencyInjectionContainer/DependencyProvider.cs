using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DependencyInjectionContainer
{
    public class DependencyProvider : IDependencyProvider
    {
        protected readonly IDependenciesConfiguration configuration;

        public IEnumerable<TDependency> Resolve<TDependency>(string name = null) where TDependency : class
        {
            if (typeof(TDependency).IsValueType)
            {
                throw new ArgumentException("Cannot create non-reference dependencies");
            }

            return (IEnumerable<TDependency>)Resolve(typeof(TDependency), name);
        }

        protected IEnumerable<object> Resolve(Type dependency, string name)
        {
            if (dependency.IsGenericType || dependency.IsGenericTypeDefinition)
            {
                return ResolveGeneric(dependency, name);
            }
            else
            {
                return ResolveNonGeneric(dependency, name);
            }
        }

        protected IEnumerable<object> ResolveGeneric(Type dependency, string name)
        {
            throw new NotImplementedException();
        }

        protected IEnumerable<object> ResolveNonGeneric(Type dependency, string name)
        {
            if (dependency.IsValueType)
            {
                return new List<object>
                {
                    Activator.CreateInstance(dependency)
                };
            }

            List<object> result = new List<object>();
            object dependencyInstance;
            foreach (ImplementationContainer container in configuration.GetImplementations(dependency))
            {
                if (container.IsSingleton)
                {
                    if (container.SingletonInstance == null)
                    {
                        container.SingletonInstance = CreateByConstructor(container.ImplementationType);
                    }
                    dependencyInstance = container.ImplementationType;
                }
                else
                {
                    dependencyInstance = CreateByConstructor(container.ImplementationType);
                }

                if (dependencyInstance != null)
                {
                    result.Add(dependencyInstance);
                }
            }
            return result;
        }

        protected object CreateByConstructor(Type type)
        {
            ConstructorInfo[] constructors = type.GetConstructors().OrderBy((constructor) => constructor.GetParameters().Length).ToArray();
            object instance = null;
            List<object> parameters = new List<object>();

            for (int constructor = 0; (constructor < constructors.Length) && (instance == null); ++constructor)
            {
                try
                {
                    foreach (ParameterInfo constructorParameter in constructors[constructor].GetParameters())
                    {
                        parameters.Add(Resolve(constructorParameter.ParameterType, null)
                            .Where((implementation) => implementation != null).FirstOrDefault());
                    }
                    instance = constructors[constructor].Invoke(parameters.ToArray());
                }
                catch
                {
                    parameters.Clear();
                }
            }

            return instance;
        }

        public DependencyProvider(IDependenciesConfiguration configuration)
        {
            this.configuration = configuration;
        }
    }
}
