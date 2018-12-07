using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DependencyInjectionContainer.UnitTests
{
    [TestClass]
    public class DependencyInjectionContainerTests
    {
        IDependenciesConfiguration config;
        IDependencyProvider provider;

        [TestInitialize]
        public void TestInitialize()
        {
            config = new DependenciesConfiguration();
        }

        [TestMethod]
        public void NonGenericTypeRegisterTest()
        {
            
        }

        [TestMethod]
        public void GenericTypeRegisterTest()
        {

        }

        [TestMethod]
        public void OpenGenericTypeRegisterTest()
        {

        }

        [TestMethod]
        public void NonGenericTypeResolveTest()
        {

        }

        [TestMethod]
        public void GenericTypeResolveTest()
        {

        }

        [TestMethod]
        public void OpenGenericTypeResolveTest()
        {

        }

        [TestMethod]
        public void SingletonResolveTest()
        {

        }

        [TestMethod]
        public void ExplicitNameResolveTest()
        {

        }

        [TestMethod]
        public void ConstructorNameResolveTest()
        {

        }
    }
}
