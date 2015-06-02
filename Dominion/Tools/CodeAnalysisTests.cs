using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NLog;
using NUnit.Framework;
using org.gbd.Dominion.Model;
using org.gbd.Dominion.Model.Cards;

namespace org.gbd.Dominion.Tools
{
    
    [TestFixture]
    public class CodeAnalysisTests
    {

        [Test]
        public void SmokeTest()
        {

        }

        public class CardsFinder
        {
            public static IEnumerable TestCases
            {
                get
                {
                    Type[] classes = Assembly.GetExecutingAssembly().GetTypes();

                    IEnumerable<Type> cardClasses = classes.Where(t => typeof(ICard).IsAssignableFrom(t) 
                                                                    && t.IsInterface == false
                                                                    && t.IsAbstract == false);

                    var n = cardClasses.ToList().Count();

                    IEnumerable<object> cards =  cardClasses.Select(type => Activator.CreateInstance(type));

                    IEnumerable<ICard> castedCards = cards.Cast<ICard>();

                    IEnumerable<TestCaseData> testCaseData = castedCards.Select(c => new TestCaseData(c));

                    return testCaseData;
                }
            }
        }

        [Test]
        public void CardsFinderTest()
        {
            
            IEnumerable testCases = CardsFinder.TestCases;
            IEnumerable<TestCaseData> castedTestCases = testCases.Cast<TestCaseData>();
            
            Assert.That(castedTestCases.Count() > 10);
        }

        [Test, TestCaseSource(typeof(CardsFinder),"TestCases")]
        public void CardsRequirements(ICard card)
        {
            Assert.That(card.Mechanics.Types.Any());
            Assert.That(card.Mechanics.Cost, Is.Not.Null);
            Assert.That(card.Mechanics.TreasureValue, Is.Not.Null);


            //TODO: Many tests to add => change model ?

        }




    }
}
