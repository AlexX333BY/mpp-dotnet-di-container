using System;

namespace DependencyInjectionContainer
{
    internal class ImplementationContainer
    {
        internal Type ImplementationType
        { get; }

        internal bool IsSingleton
        { get; }

        internal object SingletonInstance
        { get; set; }

        internal string Name
        { get; }

        internal ImplementationContainer(Type implementationType, bool isSingleton, string name)
        {
            ImplementationType = implementationType;
            IsSingleton = isSingleton;
            Name = name;
        }
    }
}
