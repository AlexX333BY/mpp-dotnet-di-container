using System;

namespace DependencyInjectionContainer
{
    public class DependencyKeyAttribute : Attribute
    {
        protected readonly string name;

        public DependencyKeyAttribute(String name)
        {
            this.name = name;
        }
    }
}
