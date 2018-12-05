using System;

namespace DependencyInjectionContainer
{
    internal class ImplementationContainer
    {
        Type ImplementationType
        { get; }

        bool IsSingleton
        { get; }

        object SingletonInstance
        { get; set; }

        string Name
        { get; }

        bool IsOpenGeneric
        { get; }

        internal ImplementationContainer(Type implementationType, bool isSingleton, string name, bool isOpenGeneric)
        {
            ImplementationType = implementationType;
            IsSingleton = isSingleton;
            Name = name;
            IsOpenGeneric = isOpenGeneric;
        }
    }
}
