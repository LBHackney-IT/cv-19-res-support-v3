using System;
using Bogus;
using cv19ResSupportV3.V4.Helpers;
using FluentAssertions;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V4.TestHelpers
{
    [TestFixture]
    public class PredicatesTests
    {
        Faker _faker = new Faker();

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void IsNotNullAndNotEmptyPredicateReturnsFalseWhenAStringIsEmpty(string testCaseString)
        {
            // arrange
            var testString = testCaseString;

            // act
            var evaluationResult = Predicates.IsNotNullAndNotEmpty(testString);

            // assert
            evaluationResult.Should().BeFalse();
        }

        [Test]
        public void IsNotNullAndNotEmptyPredicateReturnsTrueWhenAStringIsNonEmpty()
        {
            // arrange
            var testString = _faker.Random.Hash();

            // act
            var evaluationResult = Predicates.IsNotNullAndNotEmpty(testString);

            // assert
            evaluationResult.Should().BeTrue();
        }
    }
}
