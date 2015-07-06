using System;
using gbd.Dominion.Injection;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.GameMechanics.AI;
using gbd.Dominion.Model.Zones;
using gbd.Tools.NInject;
using NUnit.Framework;

namespace gbd.Dominion.Test.Scenarios
{
    [TestFixture]
    public class RandomAiTest : SpecificAiTestBase
    {

        [SetUp]
        public new void SetUp()
        {
            base.SetUp();
            IoC.Kernel.Unbind<IIntelligence>();
            IoC.Kernel.BindTo<IIntelligence, RandomAi>(1);
        }


        [TestCase(0, 0)]
        [TestCase(0, 1)]
        [TestCase(0, 5)]
        [TestCase(1, 5)]
        [TestCase(1, 6)]
        [TestCase(5, 10)]
        public new void Discard(int drawAmount, int discardAmount)
        {
            base.Discard(drawAmount, discardAmount);
        }


        [ExpectedException(typeof(NotEnoughCardsException))]
        [TestCase(0, 6)]
        [TestCase(0, 99)]
        [TestCase(1, 7)]
        [TestCase(5, 11)]
        public void DiscardRobustness(int drawAmount, int discardAmount)
        {
            base.Discard(drawAmount, discardAmount);
        }



        [TestCase(10,  1,10, 2,10, 3,10,     1,1)]
        [TestCase(10,  1,10, 2,10, 3,10,     1,3)]
        [TestCase(100, 1,10, 5,10, 9,10,     1,4)]
        [TestCase(100, 1,10, 5,10, 9,10,     5,9)]
        public void Receive(int coppers,    int costA, int amountA,
                                            int costB, int amountB,
                                            int costC, int amountC,
                                            int minCoinCost, int maxCoinCost)
        {
            base.Receive(coppers, new Resources(costA), amountA,
                                        new Resources(costB), amountB,
                                        new Resources(costC), amountC,
                                        new Resources(minCoinCost), new Resources(maxCoinCost));
        }

        [ExpectedException(typeof (NotEnoughCardsException))]
        [TestCase(10,  1,10, 2,10, 3,10,   4,4)]
        [TestCase(10,  1,10, 2,10, 3,10,   0,0)]
        [TestCase(10,  1,10, 2,10, 3,10,   2,1)]
        [TestCase(10,  1,0,  2,10, 3,10,   1,1)]
        [TestCase(10,  1,0,  2,0,  3,0,    1,1)]
        public void ReceiveRobustness(int coppers,      int costA, int amountA,
                                                        int costB, int amountB,
                                                        int costC, int amountC,
                                                        int minCoinCost, int maxCoinCost)
        {
            base.Receive(coppers, new Resources(costA), amountA,
                                        new Resources(costB), amountB,
                                        new Resources(costC), amountC,
                                        new Resources(minCoinCost), new Resources(maxCoinCost));
        }


        
        [TestCase(10,  1,0,10,   2,0,10,   1,0,   1,0)]
        [TestCase(10,  1,0,10,   2,0,10,   1,0,   5,0)]
        [TestCase(10,  1,0,10,   2,0,10,   1,0,   5,5)]
        [TestCase(10,  1,0,10,   1,1,10,   1,0,   5,5)]
        [TestCase(10,  1,0,10,   1,1,10,   1,1,   5,5)]
        [TestCase(10,  0,1,10,   1,1,10,   1,1,   5,5)]
        [TestCase(10,  0,1,10,   1,1,10,   0,1,   5,5)]
        public void ReceiveWithPotions(int coppers,     int coinsA, int potionsA, int amountA,
                                                        int coinsB, int potionsB, int amountB,
                                                        int minCoinCost, int minPotionsCost,
                                                        int maxCoinCost, int maxPotionsCost)
        {
            base.Receive(coppers,       new Resources(coinsA, potionsA), amountA,
                                        new Resources(coinsB, potionsB), amountB,
                                        new Resources(0), 0,
                                        new Resources(minCoinCost, minPotionsCost), 
                                        new Resources(maxCoinCost, maxPotionsCost));
        }

        [ExpectedException(typeof(NotEnoughCardsException))]
        [TestCase(10,  1,0,10,   2,0,10,   2,0,   1,0)]
        [TestCase(10,  1,0,10,   2,0,10,   1,1,   1,0)]
        [TestCase(10,  0,1,10,   0,2,10,   1,0,   5,5)]
        [TestCase(10,  0,1,10,   0,2,10,   1,1,   5,5)]
        public void ReceiveWithPotionsRobustness(int coppers, int coinsA, int potionsA, int amountA,
                                                        int coinsB, int potionsB, int amountB,
                                                        int minCoinCost, int minPotionsCost,
                                                        int maxCoinCost, int maxPotionsCost)
        {
            base.Receive(coppers, new Resources(coinsA, potionsA), amountA,
                                        new Resources(coinsB, potionsB), amountB,
                                        new Resources(0), 0,
                                        new Resources(minCoinCost, minPotionsCost),
                                        new Resources(maxCoinCost, maxPotionsCost));
        }


        [TestCase(ZoneChoice.Hand, 10, 1, 1)]
        [TestCase(ZoneChoice.Hand, 10, 1, null)]
        [TestCase(ZoneChoice.Library, 10, 1, 1)]
        [TestCase(ZoneChoice.Library, 10, 1, null)]
        [TestCase(ZoneChoice.Discard, 10, 1, 1)]
        [TestCase(ZoneChoice.Discard, 10, 1, null)]
        [TestCase(ZoneChoice.Play, 10, 1, 1)]
        [TestCase(ZoneChoice.Play, 10, 1, null)]
        [TestCase(ZoneChoice.Play, 10, 2, null)]
        [TestCase(ZoneChoice.Play, 10, 10, null)]
        [TestCase(ZoneChoice.Play, 10, 0, null)]
        [TestCase(ZoneChoice.Play, 10, 0, 10)]
        [TestCase(ZoneChoice.Hand, 10, 15, null)]
        public new void Trash(ZoneChoice zone, int cardsInEveryZone, int minAmount, int? maxAmountOrNull)
        {
            base.Trash(zone, cardsInEveryZone, minAmount, maxAmountOrNull);
        }

        [ExpectedException(typeof(NotEnoughCardsException))]
        [TestCase(ZoneChoice.Play, 10, 11, 11)]
        [TestCase(ZoneChoice.Play, 10, 11, null)]
        [TestCase(ZoneChoice.Hand, 10, 16, null)]
        public void TrashRobustness(ZoneChoice zone, int cardsInEveryZone, int minAmount, int? maxAmountOrNull)
        {
            base.Trash(zone, cardsInEveryZone, minAmount, maxAmountOrNull);
        }

        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestCase(ZoneChoice.Play, 10, 5, 4)]
        public void TrashRobustnessOutOfRangeException(ZoneChoice zone, int cardsInEveryZone, int minAmount, int? maxAmountOrNull)
        {
            base.Trash(zone, cardsInEveryZone, minAmount, maxAmountOrNull);
        }

        [Test]
        public new void TrashWithTypeConstraint()
        {
            base.TrashWithTypeConstraint();
        }


    }

}
