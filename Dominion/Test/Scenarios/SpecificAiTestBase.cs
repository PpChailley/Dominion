using System.Linq;
using gbd.Dominion.Contents.Cards;
using gbd.Dominion.Injection;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.Zones;
using gbd.Dominion.Test.Utilities;
using gbd.Tools.Clr;
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


        protected void Trash(ZoneChoice zone, int cardsInEveryZone, int minAmount, int? maxAmountOrNull)
        {
            int maxAmount = maxAmountOrNull ?? minAmount;

            IoC.Kernel.Unbind<ICard>();
            IoC.Kernel.BindTo<ICard, BindableCard>(cardsInEveryZone);

            var game = IoC.Kernel.Get<IGame>();
            var player = game.CurrentPlayer;
            player.Ready();
            player.StartTurn();
            player.Deck.Library.Cards.Random(cardsInEveryZone).MoveTo(player.Deck.Hand);
            player.Deck.Library.Cards.Random(cardsInEveryZone).MoveTo(player.Deck.DiscardPile);
            player.Deck.Library.Cards.Random(cardsInEveryZone).MoveTo(player.Deck.BattleField);

            Assert.That(game.CurrentPlayer.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(
                                            library: cardsInEveryZone - 5,
                                            hand: cardsInEveryZone + 5,
                                            discard: cardsInEveryZone,
                                            battlefield: cardsInEveryZone)));

            var trashed = game.CurrentPlayer.I.Trash<ICard>(zone, minAmount, maxAmount).ToList();

            Assert.That(trashed, Has.Count.GreaterThanOrEqualTo(minAmount));
            Assert.That(trashed, Has.Count.LessThanOrEqualTo(maxAmount));
            Assert.That(trashed, Has.All.Matches<ICard>(c => c.Zone == game.Trash));
            Assert.That(game.CurrentPlayer.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(
                        library: cardsInEveryZone - 5 - (zone == ZoneChoice.Library ? trashed.Count() : 0),
                        hand: cardsInEveryZone + 5 -    (zone == ZoneChoice.Hand ? trashed.Count() : 0),
                        discard: cardsInEveryZone  -    (zone == ZoneChoice.Discard ? trashed.Count() : 0),
                        battlefield: cardsInEveryZone - (zone == ZoneChoice.Play ? trashed.Count() : 0))));
        }


        protected void TrashWithTypeConstraint()
        {
            IoC.Kernel.Unbind<ICard>();
            IoC.Kernel.BindToInto<ICard, BindableCard.A, ILibrary>(3);
            IoC.Kernel.BindToInto<ICard, BindableCard.B, ILibrary>(2);
            IoC.Kernel.BindToInto<ICardType, VictoryType, BindableCard.A>(1);
            IoC.Kernel.BindToInto<ICardType, VictoryType, BindableCard.B>(1);

            var game = IoC.Kernel.Get<IGame>();
            var player = game.CurrentPlayer;
            player.Ready();
            player.StartTurn();

            Assert.That(game.CurrentPlayer.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(
                                library: 0,
                                hand: 5,
                                discard: 0,
                                battlefield: 0)));

            var trashed = game.CurrentPlayer.I.Trash<BindableCard.A>(ZoneChoice.Hand, 3, 3).ToList();

            Assert.That(trashed, Has.Count.EqualTo(3));
            Assert.That(trashed, Has.All.Matches<ICard>(c => c is BindableCard.A));
            Assert.That(player.Deck.Hand.Cards, Has.Count.EqualTo(2));
            Assert.That(player.Deck.Hand.Cards, Has.All.Matches<ICard>(c => c is BindableCard.B));
        }
    }
}
