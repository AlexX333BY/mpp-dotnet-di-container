﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using DependencyInjectionContainer.UnitTests.AccessoryClasses;
using System.Linq;
using System;
using System.Collections.Generic;

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
            config.Register<IMyInterface, MyImplementation1>();
            config.Register<IMyInterface, MyImplementation2>();
            var registeredImplementations = config.GetImplementations(typeof(IMyInterface)).ToList();
            Assert.AreEqual(2, registeredImplementations.Count);

            List<Type> expectedRegisteredTypes = new List<Type>
            {
                typeof(MyImplementation1),
                typeof(MyImplementation2)
            };
            CollectionAssert.AreEquivalent(expectedRegisteredTypes, 
                registeredImplementations.Select((implementation) => implementation.ImplementationType).ToList());
        }

        [TestMethod]
        public void GenericTypeRegisterTest()
        {
            config.Register<IMyGenericInterface<IMyInterface>, MyGenericImplementation1<IMyInterface>>();
            config.Register<IMyGenericInterface<IMyInterface>, MyGenericImplementation2<IMyInterface>>();
            var registeredImplementations = config.GetImplementations(typeof(IMyGenericInterface<IMyInterface>)).ToList();
            Assert.AreEqual(2, registeredImplementations.Count);

            List<Type> expectedRegisteredTypes = new List<Type>
            {
                typeof(MyGenericImplementation1<IMyInterface>),
                typeof(MyGenericImplementation2<IMyInterface>)
            };
            CollectionAssert.AreEquivalent(expectedRegisteredTypes,
                registeredImplementations.Select((implementation) => implementation.ImplementationType).ToList());
        }

        [TestMethod]
        public void OpenGenericTypeRegisterTest()
        {
            config.Register(typeof(IMyGenericInterface<>), typeof(MyGenericImplementation1<>));
            config.Register(typeof(IMyGenericInterface<>), typeof(MyGenericImplementation2<>));
            var registeredImplementations = config.GetImplementations(typeof(IMyGenericInterface<>)).ToList();
            Assert.AreEqual(2, registeredImplementations.Count);

            List<Type> expectedRegisteredTypes = new List<Type>
            {
                typeof(MyGenericImplementation1<>),
                typeof(MyGenericImplementation2<>)
            };
            CollectionAssert.AreEquivalent(expectedRegisteredTypes,
                registeredImplementations.Select((implementation) => implementation.ImplementationType).ToList());
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
