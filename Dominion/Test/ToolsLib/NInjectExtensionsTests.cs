using System.Collections.Generic;
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

namespace gbd.Dominion.Test.ToolsLib
{
    [TestFixture]
    public class NInjectExtensionsTests : BaseTest
    {

        [SetUp]
        public new void SetUp()
        {
            base.SetUp();

            IoC.Kernel.Unbind<ICard>();
            IoC.Kernel.Unbind<IList<ICard>>();
            IoC.Kernel.Unbind<ICollection<ICard>>();
            IoC.Kernel.Unbind<ISupplyPile>();

            Assert.That(IoC.Kernel.GetBindings(typeof(ICard)).Count(), Is.EqualTo(0));
        }


        [Test]
        public void BindMultipleTimes()
        {
            IoC.Kernel.Bind<ICard>(10).To<ICard, EmptyCard>();
            Assert.That(IoC.Kernel.GetBindings(typeof(ICard)).Count(), Is.EqualTo(10));

            IoC.Kernel.Bind<ICard>().To<EmptyCard>();
            Assert.That(IoC.Kernel.GetBindings(typeof(ICard)).Count(), Is.EqualTo(11));
        }



        [TestCase(1)]
        [TestCase(3)]
        [TestCase(55)]
        [TestCase(0)]
        public void BindMultipleTimes(int numberOfBindings)
        {
            IoC.Kernel.Bind<IList<ICard>>().ToConstructor(syntax => new List<ICard>());

            IoC.Kernel.Bind<ICard>(numberOfBindings).To<ICard, EmptyCard>();
            Assert.That(IoC.Kernel.GetBindings(typeof(ICard)).Count(), Is.EqualTo(numberOfBindings));
        }




        [TestCase(0)]
        [TestCase(1)]
        [TestCase(3)]
        [TestCase(55)]
        public void BindMultipleTimesTo(int numberOfBindings)
        {
            IoC.Kernel.BindTo<ICard, EmptyCard>(numberOfBindings);
            Assert.That(IoC.Kernel.GetBindings(typeof(ICard)).Count(), Is.EqualTo(numberOfBindings));
        }


        [TestCase(1)]
        [TestCase(10)]
        [TestCase(100)]
        [TestCase(0)]
        public void ToMultiple(int nbBindings)
        {
            IoC.Kernel.Bind<ICard>(nbBindings).To<ICard, Copper>();
            IoC.Kernel.Bind<ISupplyPile>().To<SupplyPile>();

            Assert.That(IoC.Kernel.GetBindings(typeof(ICard)), Has.Count.EqualTo(nbBindings));

            var pile = IoC.Kernel.Get<ISupplyPile>();


            Assert.That(pile.Cards, Has.Count.EqualTo(nbBindings));

        }



        [Test]
        public void WhenAnyAncestorOfType_SelfShouldBeAnAncestor_Level1()
        {
            IoC.Kernel.Unbind<ICard>();
            IoC.Kernel.Bind<ICard>().To<Silver>().WhenInto(typeof(IDeck));

            var deck = IoC.Kernel.Get<IDeck>();

            Assert.That(deck.Cards, Is.All.InstanceOf(typeof(Silver)));

            Assert.That(deck.Cards.Count, Is.EqualTo(4)); // 1 in each subZone
        }

        [Test]
        public void WhenAnyAncestorOfType_SelfShouldBeAnAncestor_Level0A()
        {
            IoC.Kernel.Unbind<ICard>();
            IoC.Kernel.Bind<ICard>().To<Silver>().WhenInto(typeof(ILibrary));

            var deck = IoC.Kernel.Get<IDeck>();

            Assert.That(deck.Cards, Is.All.InstanceOf(typeof(Silver)));
            Assert.That(deck.Cards.Count, Is.EqualTo(1));
        }

        [Test]
        public void WhenAnyAncestorOfType_SelfShouldBeAnAncestor_Level0B()
        {
            IoC.Kernel.Unbind<ISupplyPile>();
            IoC.Kernel.Bind<ISupplyPile>().To<SupplyPile>();

            IoC.Kernel.Unbind<ICard>();
            IoC.Kernel.Bind<ICard>().To<Copper>().WhenInto(typeof(ISupplyPile));

            var pile = IoC.Kernel.Get<ISupplyPile>();

            Assert.That(pile.Cards, Is.All.InstanceOf(typeof(Copper)));
            Assert.That(pile.Cards.Count, Is.EqualTo(1));
        }

        [Test]
        public void WhenAnyAncestorOfType_Single()
        {
            const int numberOfSubZones = 4;

            IoC.Kernel.Bind<ICard>().To<Silver>();
            IoC.Kernel.Bind<ICard>().To<Copper>().WhenInto(typeof(TestDeck));
            var witnessdeck = IoC.Kernel.Get<StartingDeck>();
            Assert.That(witnessdeck.Cards, Is.All.InstanceOf(typeof(Silver)));
            Assert.That(witnessdeck.Cards.Count, Is.EqualTo(1 * numberOfSubZones));

            IoC.Kernel.Unbind<ICard>();
            IoC.Kernel.Bind<ICard>().To<Copper>().WhenInto(typeof(TestDeck));
            Assert.That(IoC.Kernel.GetBindings(typeof(ICard)).Count(), Is.EqualTo(1));

            var deck = IoC.Kernel.Get<TestDeck>();
            Assert.That(deck.Cards, Is.All.InstanceOf(typeof(Copper)));
            Assert.That(witnessdeck.Cards.Count, Is.EqualTo(1 * numberOfSubZones));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(3)]
        [TestCase(55)]
        public void WhenAnyAncestorOfType_Collection(int collectionSize)
        {

            // TODO: test more. See InjectIntoDeckComponents() test
            const int numberOfSubZones = 4;

            IoC.Kernel.BindTo<ICard, Silver>(collectionSize);
            IoC.Kernel.BindTo<ICard, Copper>(collectionSize).WhenInto(typeof(TestDeck));

            var witnessdeck = IoC.Kernel.Get<StartingDeck>();
            Assert.That(witnessdeck.Cards, Is.All.InstanceOf(typeof(Silver)));
            Assert.That(witnessdeck.Cards.Count, Is.EqualTo(numberOfSubZones * collectionSize));

            IoC.Kernel.Unbind<ICard>();
            IoC.Kernel.BindTo<ICard, Copper>(collectionSize).WhenInto(typeof(TestDeck));
            Assert.That(IoC.Kernel.GetBindings(typeof(ICard)).Count(), Is.EqualTo(collectionSize));

            var deck = IoC.Kernel.Get<TestDeck>();
            Assert.That(deck.Cards, Is.All.InstanceOf(typeof(Copper)));
            Assert.That(witnessdeck.Cards.Count, Is.EqualTo(numberOfSubZones * collectionSize));

        }




        [Test]
        public void WhenAnyAncestorOfType_MemberFromAncestorType_Generic()
        {
            IoC.Kernel.Bind<GenericData<int>>().ToConstructor(syntax => new GenericData<int>(1))
                .WhenInto<GenericData<int>, ChildA>();

            IoC.Kernel.Bind<GenericData<int>>().ToConstructor(syntax => new GenericData<int>(2))
                .WhenInto<GenericData<int>, ChildB>();


            ChildA aAsSelf = IoC.Kernel.Get<ChildA>();
            ChildB bAsSelf = IoC.Kernel.Get<ChildB>();

            Parent aAsParent = IoC.Kernel.Get<ChildA>();
            Parent bAsParent = IoC.Kernel.Get<ChildB>();


            Assert.That(aAsSelf.AGenericData.I, Is.EqualTo(1));
            Assert.That(aAsParent.AGenericData.I, Is.EqualTo(1));

            Assert.That(bAsSelf.AGenericData.I, Is.EqualTo(2));
            Assert.That(bAsParent.AGenericData.I, Is.EqualTo(2));


        }


        [Test]
        public void WhenAnyAncestorOfType_MemberFromAncestorType_NonGeneric()
        {
            IoC.Kernel.Bind<NonGenericData>().ToConstructor(syntax => new NonGenericData(1))
                .WhenInto<NonGenericData, ChildA>();

            IoC.Kernel.Bind<NonGenericData>().ToConstructor(syntax => new NonGenericData(2))
                .WhenInto<NonGenericData, ChildB>();


            ChildA aAsSelf = IoC.Kernel.Get<ChildA>();
            ChildB bAsSelf = IoC.Kernel.Get<ChildB>();

            Parent aAsParent = IoC.Kernel.Get<ChildA>();
            Parent bAsParent = IoC.Kernel.Get<ChildB>();


            Assert.That(aAsSelf.ANonGenericData.I, Is.EqualTo(1));
            Assert.That(aAsParent.ANonGenericData.I, Is.EqualTo(1));

            Assert.That(bAsSelf.ANonGenericData.I, Is.EqualTo(2));
            Assert.That(bAsParent.ANonGenericData.I, Is.EqualTo(2));


        }






        private abstract class Parent
        {
            protected Parent(GenericData<int> aGenericData, NonGenericData aNonGenericData)
            {
                AGenericData = aGenericData;
                ANonGenericData = aNonGenericData;
            }

            public GenericData<int> AGenericData { get; set; }
            public NonGenericData ANonGenericData { get; set; }
        }

        private class ChildA : Parent {
            public ChildA(GenericData<int> aGenericData, NonGenericData aNonGenericData) : base(aGenericData, aNonGenericData)
            {
            }
        }

        private class ChildB : Parent {
            public ChildB(GenericData<int> aGenericData, NonGenericData aNonGenericData) : base(aGenericData, aNonGenericData)
            {
            }
        }

        private class NonGenericData
        {
            public int I;

            [Inject]
            public NonGenericData(int i)
            {
                this.I = i;
            }
        }

        private class GenericData<T>
        {
            public T I;

            [Inject]
            public GenericData(T i)
            {
                this.I = i;
            }
        }





    }

    



}
