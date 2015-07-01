using System.Linq;
using gbd.Dominion.Contents.Cards;
using gbd.Dominion.Injection;
using gbd.Dominion.Model;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.Zones;
using gbd.Dominion.Test.Utilities;
using gbd.Tools.NInject;
using Ninject;
using NUnit.Framework;

namespace gbd.Dominion.Test.Scenarios
{

    [TestFixture]
    public class InjectionOfCardsTests: BaseTest
    {



        [Test]
        public void SmokeTest()
        {
            var player = IoC.Kernel.Get<IPlayer>();
        }

        [Test]
        public void CardBinding()
        {
            IoC.Kernel.Unbind<ICardType>();
            IoC.Kernel.Bind<ICardType>().ToConstructor(x => new VictoryType(1));

            IoC.Kernel.Bind<ICard>().To<BindableCard>();

            var card = IoC.Kernel.Get<ICard>();

            Assert.That(card.Mechanics.VictoryPoints, Is.EqualTo(1));
        }

        [Test]
        public void CardConditionalBinding()
        {
            IoC.Kernel.Unbind<ICardType>();
            IoC.Kernel.Bind<ICardType>()
                .ToConstructor(x => new VictoryType(1))
                //.WhenInjectedInto<BindableCard>()
                ;

            IoC.Kernel.Bind<ICard>().To<BindableCard>();

            var card = IoC.Kernel.Get<ICard>();

            Assert.That(card.Mechanics.VictoryPoints, Is.EqualTo(1));
        }

        [TestCase(1,0,0)]
        [TestCase(5,0,0)]
        [TestCase(100,0,0)]
        [TestCase(1,1,1)]
        public void BindCardWithSimplifiedHelper(int library, int hand, int discard)
        {
            IoC.Kernel.Unbind<ICard>();
            IoC.Kernel.BindCard<EmptyCard, ILibrary>(library);
            IoC.Kernel.BindCard<EmptyCard, IHand>(hand);
            IoC.Kernel.BindCard<EmptyCard, IDiscardPile>(discard);
            IoC.Kernel.BindCard<EmptyCard, ISupplyPile>(10000);

            var deck = IoC.Kernel.Get<IDeck>();

            Assert.That(deck.CardCountByZone, Is.EqualTo(new CardRepartition(library, hand, discard, 0)));

        }

        [TestCase(1, 0)]
        [TestCase(0, 1)]
        [TestCase(7, 3)]
        [TestCase(15, 44)]
        public void BindFullDeckWithSimplifiedHelper(int coppers, int estates)
        {
            IoC.Kernel.Unbind<ICard>();
            IoC.Kernel.BindCard<Copper, ILibrary>(coppers);
            IoC.Kernel.BindCard<Estate, ILibrary>(estates);

            var deck = IoC.Kernel.Get<IDeck>();

            Assert.That(deck.CardCountByZone, Is.EqualTo(new CardRepartition(coppers + estates, 0, 0, 0)));
            Assert.That(deck.Cards.Count(c => c.GetType() == typeof(Copper)), Is.EqualTo(coppers));
            Assert.That(deck.Cards.Count(c => c.GetType() == typeof(Estate)), Is.EqualTo(estates));

        }





        [TestCase(1, 1, 1, 1)]
        [TestCase(2, 2, 2, 2)]
        [TestCase(10, 50, 500, 5000)]
        public void InjectIntoDeckComponents_Tools(int inLib, int inHand, int inDisc, int inBf)
        {
            IoC.Kernel.Unbind<ICard>();

            IoC.Kernel.Bind<ICard>(inLib).To<ICard, Copper>()
                .WhenAnyAncestorOfType<Copper, ILibrary>();

            IoC.Kernel.Bind<ICard>(inHand).To<ICard, Silver>()
                .WhenAnyAncestorOfType<Silver, IHand>();

            IoC.Kernel.Bind<ICard>(inDisc).To<ICard, Gold>()
                .WhenAnyAncestorOfType<Gold, IDiscardPile>();

            IoC.Kernel.Bind<ICard>(inBf).To<ICard, Estate>()
                .WhenAnyAncestorOfType<Estate, IBattleField>();


            InjectIntoDeckComponents_InternalCheck(inLib, inHand, inDisc, inBf);

        }

        [TestCase(1, 1, 1, 1)]
        [TestCase(2, 2, 2, 2)]
        [TestCase(10, 50, 500, 5000)]
        public void InjectIntoDeckComponents_Linq(int inLib, int inHand, int inDisc, int inBf)
        {
            IoC.Kernel.Unbind<ICard>();

            IoC.Kernel.Bind<ICard>(inLib).To<ICard, Copper>()
                .ForEach(c => c.WhenAnyAncestorOfType<Copper, ILibrary>());


            IoC.Kernel.Bind<ICard>(inHand).To<ICard, Silver>()
                .ForEach(c => c.WhenAnyAncestorOfType<Silver, IHand>());


            IoC.Kernel.Bind<ICard>(inDisc).To<ICard, Gold>()
                 .ForEach(c => c.WhenAnyAncestorOfType<Gold, IDiscardPile>());


            IoC.Kernel.Bind<ICard>(inBf).To<ICard, Estate>()
                 .ForEach(c => c.WhenAnyAncestorOfType<Estate, IBattleField>());



            InjectIntoDeckComponents_InternalCheck(inLib, inHand, inDisc, inBf);

        }


        [TestCase(1, 1, 1, 1)]
        [TestCase(2, 2, 2, 2)]
        [TestCase(10, 50, 500, 5000)]
        public void InjectIntoDeckComponents_Foreach(int inLib, int inHand, int inDisc, int inBf)
        {
            IoC.Kernel.Unbind<ICard>();

            foreach (var syntax in IoC.Kernel.Bind<ICard>(inLib).To<ICard, Copper>())
                syntax.WhenAnyAncestorOfType<Copper, ILibrary>();


            foreach (var syntax in IoC.Kernel.Bind<ICard>(inHand).To<ICard, Silver>())
                syntax.WhenAnyAncestorOfType<Silver, IHand>();


            foreach (var syntax in IoC.Kernel.Bind<ICard>(inDisc).To<ICard, Gold>())
                syntax.WhenAnyAncestorOfType<Gold, IDiscardPile>();


            foreach (var syntax in IoC.Kernel.Bind<ICard>(inBf).To<ICard, Estate>())
                syntax.WhenAnyAncestorOfType<Estate, IBattleField>();



            InjectIntoDeckComponents_InternalCheck(inLib, inHand, inDisc, inBf);

        }


        [TestCase(1, 1, 1, 1)]
        [TestCase(2, 2, 2, 2)]
        [TestCase(10, 50, 500, 5000)]
        public void InjectIntoDeckComponents_Loop(int inLib, int inHand, int inDisc, int inBf)
        {
            IoC.Kernel.Unbind<ICard>();

            for (int i = 0; i < inLib; i++)
                IoC.Kernel.Bind<ICard>().To<Copper>().WhenAnyAncestorOfType<Copper, ILibrary>();

            for (int i = 0; i < inHand; i++)
                IoC.Kernel.Bind<ICard>().To<Silver>().WhenAnyAncestorOfType<Silver, IHand>();

            for (int i = 0; i < inDisc; i++)
                IoC.Kernel.Bind<ICard>().To<Gold>().WhenAnyAncestorOfType<Gold, IDiscardPile>();

            for (int i = 0; i < inBf; i++)
                IoC.Kernel.Bind<ICard>().To<Estate>().WhenAnyAncestorOfType<Estate, IBattleField>();


            InjectIntoDeckComponents_InternalCheck(inLib, inHand, inDisc, inBf);
        }

        private static void InjectIntoDeckComponents_InternalCheck(int inLib, int inHand, int inDisc, int inBf)
        {
            var lib = IoC.Kernel.Get<ILibrary>();
            var hand = IoC.Kernel.Get<IHand>();
            var discard = IoC.Kernel.Get<IDiscardPile>();
            var bf = IoC.Kernel.Get<IBattleField>();

            Assert.That(lib.Cards.Count, Is.EqualTo(inLib));
            Assert.That(hand.Cards.Count, Is.EqualTo(inHand));
            Assert.That(discard.Cards.Count, Is.EqualTo(inDisc));
            Assert.That(bf.Cards.Count, Is.EqualTo(inBf));


            var deck = new TestDeck(lib, discard, bf, hand);

            Assert.That(deck.CardCountByZone, Is.EqualTo(new CardRepartition(inLib, inHand, inDisc, inBf)));
        }







    }
}
