using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionContainer
{
    public class DependencyProvider : IDependencyProvider
    {
        protected readonly IDependenciesConfiguration configuration;

        public IEnumerable<TDependency> Resolve<TDependency>(string name = null) where TDependency : class
        {
            throw new NotImplementedException();
        }

        public DependencyProvider(IDependenciesConfiguration configuration)
        {
            this.configuration = configuration;
        }
    }
}
