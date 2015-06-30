using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using gbd.Dominion.Contents.Cards;
using gbd.Dominion.Injection;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Test.Utilities;
using Ninject;
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



        [Test]
        public void HaveEnoughCardsImplementedToPlay()
        {
            
            IEnumerable testCases = ReflectionClassFinder.GetAllImplementedCardsTestData;
            IEnumerable<TestCaseData> castedTestCases = testCases.Cast<TestCaseData>();
            
            Assert.That(castedTestCases.Count() > 10);
        }

        [Test, TestCaseSource(typeof(ReflectionClassFinder),"GetAllImplementedCardsTestData")]
        public void CardsRequirements(Type type)
        {
            if (type.Namespace.StartsWith("gbd.Dominion.Test"))
                return;
            
            var card = (ICard) IoC.Kernel.Get(type);

            Assert.That(card.Mechanics.Types.Any());
            Assert.That(card.Mechanics.Types, Is.Unique);
            Assert.That(card.Mechanics.Cost, Is.Not.Null);
            Assert.That(card.Mechanics.TreasureValue, Is.Not.Null);
        }


        [Test, TestCaseSource(typeof(ReflectionClassFinder), "GetAllImplementedCardsTestData")]
        public void CardIsPublic(Type type)
        {
            if (type.Namespace.StartsWith("gbd.Dominion.Test"))
                return;

            Assert.That(type.IsPublic);
        }

        [Test, TestCaseSource(typeof(ReflectionClassFinder), "GetAllTestRelatedClassesTestCaseData")]
        public void AllTestsFixturesDeriveFromBaseTest(Type t)
        {
            Assert.That(typeof(BaseTest).IsAssignableFrom(t), Is.True);

        }

        [Test, TestCaseSource(typeof (ReflectionClassFinder), "GetAllTestRelatedClassesTestCaseData")]
        public void AllTestRelatedClassesAreTestFixtures(Type t)
        {
            Assert.That(t.GetCustomAttributes(typeof (TestFixtureAttribute), true).Any(), Is.True);
        }

        [TestCase(0.3)]
        [TestCase(0.5)]
        [TestCase(0.7)]
        [TestCase(0.9)]
        public void TestMethodsRatio(double expectedRatio)
        {
            var testMethods = ReflectionClassFinder.GetAllTestRelatedClasses()
                                .Select(clazz => clazz.GetMethods().Where( m =>     m.GetCustomAttributes(typeof(TestAttribute)).Any()
                                                                                ||  m.GetCustomAttributes(typeof(TestCaseAttribute)).Any())
                                .ToList()).ToList();


            long nbTestMethods = testMethods.Aggregate<List<MethodInfo>, long>(0, (n, methodsColl) => n + methodsColl.LongCount());


            long nbNonTestClasses = Assembly.GetExecutingAssembly().GetTypes()
                                .Where(t => t.GetCustomAttributes(typeof(TestFixtureAttribute)).Any() == false).LongCount();

            double ratio = 1D * nbTestMethods/nbNonTestClasses;

            Assert.That(ratio, Is.GreaterThan(expectedRatio));
        }



        [Test, TestCaseSource(typeof(ReflectionClassFinder), "GetAllInterfaces")]
        public void InterfacesAreNotPollutedByNInjectDecorations(Type t)
        {

            var decorations = t.GetCustomAttributes(typeof (Ninject.InjectAttribute));

            Assert.That(decorations, Is.Empty);

            foreach (var member in t.GetMembers())
            {
                Assert.That(member.GetCustomAttributes(typeof(Ninject.InjectAttribute)), Is.Empty);
            }

        }

        [Test, TestCaseSource(typeof (ReflectionClassFinder), "GetAllImplementedCards")]
        public void CardKnowsSet(Type type)
        {
            //if (type.Namespace.StartsWith("gbd.Dominion.Test"))
            //    return;


            var card = (ICard) IoC.Kernel.Get(type);

            if (typeof (AlwaysInSupplyCard).IsAssignableFrom(type))
            {
                Assert.That(card.PresentInSet, Is.EqualTo(Include.AlwaysIncluded));
            }
            else if (typeof (SelectableCard).IsAssignableFrom(type))
            {
                Assert.That(card.PresentInSet, Is.EqualTo(Include.Selectable));
            }
            else if (typeof (ConditionalCard).IsAssignableFrom(type))
            {
                Assert.That(card.PresentInSet, Is.EqualTo(Include.Conditional));
            }
            else if (typeof (OptionalCard).IsAssignableFrom(type))
            {
                Assert.That(card.PresentInSet, Is.EqualTo(Include.Optional));
            }
            else
            {
                Assert.That(card.PresentInSet, Is.EqualTo(Include.TestCards));
            }
        }

        [Test, TestCaseSource(typeof (ReflectionClassFinder), "GetAllImplementedCards")]
        public void CardKnowsExtension(Type type)
        {
            //if (type.Namespace.StartsWith("gbd.Dominion.Test"))
            //    return;

            var card = (ICard) IoC.Kernel.Get(type);

            Assert.That(card.Extension, Is.Not.Null);            
        }


        [Test]
        public void ReflectionClassFinderFindsAllCards()
        {
            var cards = ReflectionClassFinder.GetAllImplementedCards();

            Assert.That(cards, Contains.Item(typeof(Copper)));
            Assert.That(cards, Contains.Item(typeof(Estate)));
            Assert.That(cards, Contains.Item(typeof(Village)));
            Assert.That(cards, Contains.Item(typeof(Gardens)));
        }


    }
}
