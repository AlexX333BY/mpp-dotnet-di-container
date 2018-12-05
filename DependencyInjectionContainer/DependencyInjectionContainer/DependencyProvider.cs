using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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

            throw new NotImplementedException();
        }

        protected ConstructorInfo GetConstructor(Type type)
        {
            return type.GetConstructors().OrderBy((constructor) => constructor.GetParameters().Length).FirstOrDefault();
        }

        public DependencyProvider(IDependenciesConfiguration configuration)
        {
            this.configuration = configuration;
        }
    }
}
