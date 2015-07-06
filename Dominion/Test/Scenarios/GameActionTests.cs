using System;
using System.Collections.Generic;
using System.Linq;
using gbd.Dominion.Contents.Cards;
using gbd.Dominion.Injection;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.GameMechanics.Actions;
using gbd.Dominion.Model.Zones;
using gbd.Dominion.Test.Utilities;
using gbd.Tools.NInject;
using Ninject;
using NUnit.Framework;

namespace gbd.Dominion.Test.Scenarios
{
    [TestFixture]
    public class GameActionTests: BaseTest
    {

        [SetUp]
        public new void SetUp()
        {
            base.SetUp();

            IoC.Kernel.Unbind<ICard>();
            IoC.Kernel.Unbind<IGameAction>();
            IoC.Kernel.Unbind<ICardType>();
            IoC.Kernel.Unbind<IPlayer>();

            IoC.Kernel.Bind<IPlayer>().To<Player>().InSingletonScope();
        }

 
        
        [TestCase(10, 1)]
        [TestCase(10, 2)]
        [TestCase(10, 5)]
        [TestCase(10, 1)]
        public void Draw(int deckSize, int drawAmount)
        {
            IoC.Kernel.Bind<ICard>(deckSize).To<ICard, BindableCard>().WhenInto<BindableCard, ILibrary>();
            IoC.Kernel.BindToInto<ICardType, ActionType, BindableCard>(1);
            IoC.Kernel.Bind<IGameAction>().ToConstructor(x => new Draw(drawAmount));
            

            var player = IoC.Kernel.Get<IPlayer>();
            player.Ready();
            player.StartTurn();

            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(
                new CardRepartition(    library: deckSize - 5, 
                                        hand: 5, 
                                        discard: 0, 
                                        battlefield: 0)));

            player.PlayAction(player.Deck.Hand.Cards.First());

            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(
                new CardRepartition(    library: deckSize - 5 - drawAmount,
                                        hand: 4 + drawAmount, 
                                        discard: 0, 
                                        battlefield: 1)));
        }

        [TestCase(1)]
        [TestCase(0)]
        [TestCase(2)]
        [TestCase(10)]
        public void AddAction(int amount)
        {
            IoC.Kernel.Bind<ICard>(10).To<ICard, BindableCard>().WhenInto<BindableCard, ILibrary>();
            IoC.Kernel.Bind<ICardType>().To<ActionType>();
            IoC.Kernel.Bind<IGameAction>().ToConstructor(x => new AddAction(amount));

            var player = IoC.Kernel.Get<IPlayer>();
            player.Ready();
            player.StartTurn();

            Assert.That(player.Status.Actions, Is.EqualTo(1));

            player.PlayAction(player.Deck.Hand.Cards.First());

            Assert.That(player.Status.Actions, Is.EqualTo(amount));
        }


        [TestCase(1)]
        [TestCase(0)]
        [TestCase(2)]
        [TestCase(10)]
        public void AddBuy(int amount)
        {
            IoC.Kernel.Bind<ICard>(10).To<ICard, BindableCard>().WhenInto<BindableCard, ILibrary>();
            IoC.Kernel.BindToInto<ICardType, ActionType, BindableCard>(1);
            IoC.Kernel.Bind<IGameAction>().ToConstructor(x => new AddBuy(amount));

            var player = IoC.Kernel.Get<IPlayer>();
            player.Ready();
            player.StartTurn();

            Assert.That(player.Status.Buys, Is.EqualTo(1));

            player.PlayAction(player.Deck.Hand.Cards.First());

            Assert.That(player.Status.Buys, Is.EqualTo(1 + amount));
        }


        [TestCase(10, 0, 1)]
        [TestCase(10, 1, 1)]
        [TestCase(30, 10, 14)]
        [TestCase(30, 25, 29)]
        public void Discard(int deckSize, int draw, int discard)
        {
            IoC.Kernel.Bind<ICard>(deckSize).To<ICard, BindableCard>().WhenInto<BindableCard, ILibrary>();
            IoC.Kernel.BindToInto<ICardType, ActionType, BindableCard>(1);
            IoC.Kernel.Bind<IGameAction>().ToConstructor(x => 
                new Discard(PlayerChoice.Current, discard));

            var player = IoC.Kernel.Get<IPlayer>();
            player.Ready();
            player.StartTurn();
            player.Draw(draw);

            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(
                            new CardRepartition(library: deckSize-5-draw, 
                                                hand: 5+draw, 
                                                discard: 0, 
                                                battlefield: 0)));

            player.PlayAction(player.Deck.Hand.Cards.First());

            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(
                            new CardRepartition(library: deckSize - 5 - draw,
                                                hand: 4 + draw - discard,
                                                discard: discard,
                                                battlefield: 1)));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(100)]
        public void ReceiveCurse(int amount)
        {
            IoC.Kernel.Bind<ICard>(10).To<ICard, BindableCard>().WhenInto<BindableCard, ILibrary>();
            IoC.Kernel.BindToInto<ICardType, ActionType, BindableCard>(1);
            IoC.Kernel.Bind<IGameAction>().ToConstructor(x => new ReceiveCurse(PlayerChoice.Current, amount));
            IoC.Kernel.BindTo<ICard, Curse>(100).WhenInto<Curse, ISupplyZone>();


            var player = IoC.Kernel.Get<IPlayer>();
            player.Ready();
            player.StartTurn();

            player.PlayAction(player.Deck.Hand.Cards.First());

            Assert.That(player.Deck.Cards.Count, Is.EqualTo(10 + amount));
            Assert.That(player.Deck.Cards.Count(c => c.GetType() == typeof(Curse)), Is.EqualTo(amount));
        }


        [TestCase(1, PlayerChoice.Left)]
        [TestCase(1, PlayerChoice.Right)]
        [TestCase(1, PlayerChoice.Opponents)]
        [TestCase(1, PlayerChoice.Current)]
        public void ReceiveCurse(int amount, PlayerChoice who)
        {
            IoC.Kernel.Bind<ICard>(10).To<ICard, BindableCard>().WhenInto<BindableCard, ILibrary>();
            IoC.Kernel.BindToInto<ICardType, ActionType, BindableCard>(1);
            IoC.Kernel.Bind<IGameAction>().ToConstructor(x => new ReceiveCurse(who, amount));
            IoC.Kernel.BindTo<ICard, Curse>(100).WhenInto<Curse, ISupplyZone>();

            IoC.Kernel.Unbind<IPlayer>();
            IoC.Kernel.BindTo<IPlayer, Player>(4);


            var game = IoC.Kernel.Get<IGame>();
            game.Ready();
            var player = game.CurrentPlayer;
            var affectedPlayers = new List<IPlayer>();
            var safePlayers = new List<IPlayer>();

            switch (who)
            {
                case PlayerChoice.Left:
                    safePlayers.Add(player);
                    safePlayers.Add(game.Players[1]);
                    safePlayers.Add(game.Players[2]);
                    affectedPlayers.Add(game.Players[3]);
                break;

                case PlayerChoice.Right:
                    safePlayers.Add(player);
                    safePlayers.Add(game.Players[2]);
                    safePlayers.Add(game.Players[3]);
                    affectedPlayers.Add(game.Players[1]);
                break;

                case PlayerChoice.Opponents:
                    safePlayers.Add(player);
                    affectedPlayers.Add(game.Players[1]);
                    affectedPlayers.Add(game.Players[2]);
                    affectedPlayers.Add(game.Players[3]);
                break;

                case PlayerChoice.Current:
                    affectedPlayers.Add(player);
                    safePlayers.Add(game.Players[1]);
                    safePlayers.Add(game.Players[2]);
                    safePlayers.Add(game.Players[3]);
                break;

                default:
                    throw new NotImplementedException();
            }

            player.PlayAction(player.Deck.Hand.Cards.First());

            foreach (var safe in affectedPlayers)
            {
                Assert.That(safe.Deck.Cards.Count, Is.EqualTo(10 + amount));
                Assert.That(safe.Deck.Cards.Count(c => c.GetType() == typeof(Curse)), Is.EqualTo(amount));
            }

            foreach (var affected in safePlayers)
            {
                Assert.That(affected.Deck.Cards.Count, Is.EqualTo(10));
                Assert.That(affected.Deck.Cards, Has.None.InstanceOf<Curse>());
            }

        }

        [Test]
        public void TrashThis()
        {
            IoC.Kernel.Bind<ICard>(10).To<ICard, BindableCard>().WhenInto<BindableCard, ILibrary>();
            IoC.Kernel.BindToInto<ICardType, ActionType, BindableCard>(1);
            IoC.Kernel.Bind<IGameAction>().ToConstructor(x => new TrashThis());

            var game = IoC.Kernel.Get<IGame>();
            game.Ready();
            var player = game.CurrentPlayer;

            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(5,5,0,0)));

            var card = player.Deck.Hand.Cards.First();
            player.PlayAction(card);

            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(5, 4, 0, 0)));
            Assert.That(game.Trash.Cards.Single(), Is.EqualTo(card));
        }

    }
}
