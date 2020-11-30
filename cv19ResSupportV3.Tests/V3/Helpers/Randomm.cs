using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoFixture;
using AutoFixture.Dsl;
using AutoFixture.Kernel;
using Bogus;

namespace LBHFSSPublicAPI.Tests.TestHelpers
{
    public static class Randomm
    {
        private static Faker _faker = new Faker("en_GB");          // Good for single values
        private static Fixture _fixture = new Fixture();           // Good for complex objects
        static Randomm()                                           // Gets called automatically by common language runtime (CLR)
        {
            IgnoreVirtualMethods();
        }
        public static bool Bool()
        {
            return _faker.Random.Bool();
        }
        public static int Id(int minimum = 0, int maximum = 10000)
        {
            return _faker.Random.Int(minimum, maximum);
        }
        public static string Word()
        {
            return _faker.Random.Hash().ToString(); // .Word() had too many problems: sometimes less than 3 chars, sometimes more than 1 word.
        }
        public static string Text()
        {
            return string.Join(" ", _faker.Random.Words(5));
        }
        public static T RandomItem<T>(this ICollection<T> collection)
        {
            return _faker.Random.CollectionItem(collection);
        }
        public static T Create<T>()
        {
            return _fixture.Create<T>();
        }
        public static IEnumerable<T> CreateMany<T>(int quantity = 3)
        {
            return _fixture.CreateMany<T>(quantity);
        }
        #region Options
        public static void IgnoreVirtualMethods()
        {
            _fixture.Customize(new IgnoreVirtualMembersCustomisation());
        }

        public static ICustomizationComposer<T> Build<T>()
        {
            return _fixture.Build<T>();
        }

        #endregion

        /// <summary>
        /// Coordinate bounds (Lon, Lat) of a Rectangle surrounding
        /// Hackey. Some parts of rectangle are outside hackney.
        /// </summary>
        /// <returns></returns>
        public static double Longitude()
        {
            return _faker.Random.Double(-0.11, 0);
        }

        public static double Latitude()
        {
            return _faker.Random.Double(51.513, 51.58);
        }
    }
    #region Autofixture Customization
    public class IgnoreVirtualMembers : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            var pi = request as PropertyInfo;
            if (pi == null)
                return new NoSpecimen();
            if (pi.GetGetMethod().IsVirtual)
                return null;
            return new NoSpecimen();
        }
    }
    public class IgnoreVirtualMembersCustomisation : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(new IgnoreVirtualMembers());
        }
    }
    #endregion
}
