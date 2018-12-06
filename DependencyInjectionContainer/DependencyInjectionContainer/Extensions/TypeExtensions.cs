using System;
using System.Collections.Generic;

namespace DependencyInjectionContainer.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsAssignableFromAsOpenGeneric(this Type type, Type c)
        {
            if (!type.IsGenericTypeDefinition || !c.IsGenericTypeDefinition)
            {
                throw new ArgumentException("Specified types should be generic");
            }

            Type comparedType, baseType;

            Queue<Type> baseTypes = new Queue<Type>();
            baseTypes.Enqueue(c);

            bool result;

            do
            {
                comparedType = baseTypes.Dequeue();
                baseType = comparedType.BaseType;
                if (baseType != null)
                {
                    baseTypes.Enqueue(baseType);
                }
                foreach (Type baseInterface in comparedType.GetInterfaces())
                {
                    baseTypes.Enqueue(baseInterface);
                }
                result = comparedType == type;
            } while (!result && (baseTypes.Count > 0));

            return result;
        }
    }
}
