using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using org.gbd.Dominion.Model;
using org.gbd.Dominion.Tools;

namespace org.gbd.Dominion.Contents
{
    
    [TestFixture]
    public class CardsIntegrityTests
    {

        [Test]
        public void SmokeTest()
        {

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
