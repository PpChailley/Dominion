using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using gbd.Dominion.Model;
using gbd.Dominion.Test.Utilities;
using NUnit.Framework;

namespace gbd.Dominion.Test.Scenarios
{
    
    [TestFixture]
    public class ReflectionSelfTests: BaseTest
    {

        [Test]
        public void SmokeTest()
        {

        }


/*
       [Ignore("Suppress any call to ReflectionClassFinder")]
        [Test]
        public void HaveEnoughCardsImplementedToPlay()
        {
            
            IEnumerable testCases = ReflectionClassFinder.GetAllImplementedCardsTestData;
            IEnumerable<TestCaseData> castedTestCases = testCases.Cast<TestCaseData>();
            
            Assert.That(castedTestCases.Count() > 10);
        }

        [Ignore("Suppress any call to ReflectionClassFinder")]
        [Test, TestCaseSource(typeof(ReflectionClassFinder),"GetAllImplementedCardsTestData")]
        public void CardsRequirements(ICard card)
        {
            Assert.That(card.Mechanics.Types.Any());
            Assert.That(card.Mechanics.Cost, Is.Not.Null);
            Assert.That(card.Mechanics.TreasureValue, Is.Not.Null);


            //TODO: Many tests to add => change model ?

            throw new InconclusiveException("Think it over");

        }

        [Ignore("Suppress any call to ReflectionClassFinder")]
        [Test, TestCaseSource(typeof(ReflectionClassFinder), "GetAllTestRelatedClassesTestCaseData")]
        public void AllTestsFixturesDeriveFromBaseTest(Type t)
        {
            Assert.That(typeof(BaseTest).IsAssignableFrom(t), Is.True);

        }

        [Ignore("Suppress any call to ReflectionClassFinder")]
        [Test, TestCaseSource(typeof (ReflectionClassFinder), "GetAllTestRelatedClassesTestCaseData")]
        public void AllTestRelatedClassesAreTestFixtures(Type t)
        {
            Assert.That(t.GetCustomAttributes(typeof (TestFixtureAttribute), true).Any(), Is.True);
        }

        [Ignore("Suppress any call to ReflectionClassFinder")]
        [Test]
        public double TestMethodsAmountIsCorrect()
        {
            var testMethods = ReflectionClassFinder.GetAllTestRelatedClasses()
                                .Select(clazz => clazz.GetMethods().Where( m =>     m.GetCustomAttributes(typeof(TestAttribute)).Any()
                                                                                ||  m.GetCustomAttributes(typeof(TestCaseAttribute)).Any())
                                .ToList()).ToList();


            long nbTestMethods = testMethods.Aggregate<List<MethodInfo>, long>(0, (n, methodsColl) => n + methodsColl.LongCount());


            long nbNonTestClasses = Assembly.GetExecutingAssembly().GetTypes()
                                .Where(t => t.GetCustomAttributes(typeof(TestFixtureAttribute)).Any() == false).LongCount();

            double ratio = 1D * nbTestMethods/nbNonTestClasses;

            Assert.That(ratio, Is.GreaterThan(0.3));

            return ratio;
        }


        [Ignore("Suppress any call to ReflectionClassFinder")]
        [Test]
        public void TestMethodsAmountIsGood()
        {
            Assert.That(TestMethodsAmountIsCorrect(), Is.GreaterThan(0.6));
        }
 * 
 * */

    }
}
