using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Test.Utilities;
using NUnit.Framework;


namespace gbd.Dominion.Test.Scenarios
{
    [TestFixture]
    class GameActionRobustnessTests: GameActionTests
    {


        [ExpectedException(typeof(NotEnoughCardsException))]
        [TestCase(10, 6)]
        [TestCase(1, 6)]
        [TestCase(100, 96)]
        [TestCase(0, 1)]
        public new void Draw(int deckSize, int drawAmount)
        {
            base.Draw(deckSize, drawAmount);
        }



    }
}
