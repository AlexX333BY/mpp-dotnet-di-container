namespace DependencyInjectionContainer.UnitTests.AccessoryClasses
{
    public class MyGenericNamedConstructorParameter
    {
        public readonly IMyInterface intfImpl1;
        public readonly IMyInterface intfImpl2;

        public MyGenericNamedConstructorParameter([DependencyKey("1")] IMyInterface intfImpl1, 
            [DependencyKey("2")] IMyInterface intfImpl2)
        {
            this.intfImpl1 = intfImpl1;
            this.intfImpl2 = intfImpl2;
        }
    }
}
