using System;
using System.Linq;
using gbd.Dominion.Contents.Cards;
using gbd.Dominion.Injection;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.GameMechanics.AI;
using gbd.Dominion.Model.Zones;
using gbd.Dominion.Test.Utilities;
using gbd.Tools.NInject;
using Ninject;
using NUnit.Framework;

namespace gbd.Dominion.Test.Scenarios
{




    public abstract class SpecificAiTestBase: BaseTest
    {
        protected void Discard(int drawAmount, int discardAmount)
        {
            var player = IoC.Kernel.Get<IPlayer>();
            player.Ready();
            player.StartTurn();

            player.Draw(drawAmount);
            var discarded = player.I.Discard(discardAmount);

            Assert.That(discarded, Has.Count.EqualTo(discardAmount));
            Assert.That(discarded, Has.All.Matches<ICard>(c => c.Zone == player.Deck.DiscardPile));
            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(
                                            library: 5 - drawAmount,
                                            hand: 5 + drawAmount - discardAmount,
                                            discard: discardAmount,
                                            battlefield: 0)));
        }



        protected void Receive(int coppers,         Resources costA, int amountA,
                                                    Resources costB, int amountB,
                                                    Resources costC, int amountC,
                                                    Resources minCost, Resources maxCost)
        {
            // TODO: refactor IoC.Kernel into IoC.K
            IoC.Kernel.Unbind<ICard>();
            IoC.Kernel.BindToInto<ICard, Copper, ILibrary>(coppers);

            IoC.Kernel.Bind<Resources>().ToMethod(x => costA.Clone()).WhenInto<Resources, BindableCard.A>();
            IoC.Kernel.BindToInto<ICard, BindableCard.A, ISupplyZone>(amountA);

            IoC.Kernel.Bind<Resources>().ToMethod(x => costB.Clone()).WhenInto<Resources, BindableCard.B>();
            IoC.Kernel.BindToInto<ICard, BindableCard.B, ISupplyZone>(amountB);

            IoC.Kernel.Bind<Resources>().ToMethod(x => costC.Clone()).WhenInto<Resources, BindableCard.C>();
            IoC.Kernel.BindToInto<ICard, BindableCard.C, ISupplyZone>(amountC);


            var game = IoC.Kernel.Get<IGame>();
            game.Ready();
            var player = game.CurrentPlayer;


            player.I.Receive(minCost, maxCost);

            var received = player.Deck.Cards.Single(c => c is BindableCard);

            Assert.That(received.Mechanics.Cost.SmallerOrEqual(maxCost));
            Assert.That(received.Mechanics.Cost.GreaterOrEqual(minCost));


        }
    }
}
