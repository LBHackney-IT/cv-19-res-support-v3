using System.Collections.Generic;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Factories.Commands;
using cv19ResSupportV3.V3.Infrastructure;
using FluentAssertions;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.Factories.Commands
{
    [TestFixture]
    public class LookupFactory
    {
        [Test]
        public void CanMapADatabaseEntityToADomainObject()
        {
            var entityObject = new LookupEntity() {Id = 1, Lookup = "lookup", LookupGroup = "Group"};
            var domainObject = new LookupDomain() {Id = 1, Lookup = "lookup", LookupGroup = "Group"};

            var result = entityObject.ToDomain();
            result.Should().BeEquivalentTo(domainObject);
        }
        [Test]
        public void CanMapADatabaseEntityListToADomainList()
        {
            var entityList = new List<LookupEntity> (){new LookupEntity() {Id = 1, Lookup = "lookup", LookupGroup = "Group"}, new LookupEntity() {Id = 2, Lookup = "lookup2", LookupGroup = "Group2"}};
            var domainList = new List<LookupDomain> (){new LookupDomain() {Id = 1, Lookup = "lookup", LookupGroup = "Group"}, new LookupDomain() {Id = 2, Lookup = "lookup2", LookupGroup = "Group2"}};

            var result = entityList.ToDomain();
            result.Should().BeEquivalentTo(domainList);
        }
    }
    }

