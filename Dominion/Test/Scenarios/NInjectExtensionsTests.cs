﻿using System.Collections.Generic;
using System.Linq;
using gbd.Dominion.Contents.Cards;
using gbd.Dominion.Model;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.Zones;
using gbd.Dominion.Test.Utilities;
using gbd.Dominion.Tools;
using gbd.Tools.NInject;
using Ninject;
using NUnit.Framework;

namespace gbd.Dominion.Test.Scenarios
{
    [TestFixture]
    public class NInjectExtensionsTests: BaseTest
    {

        [SetUp]
        public new void SetUp()
        {
            base.SetUp();

            IoC.Kernel.Unbind<ICard>();
            IoC.Kernel.Unbind<IList<ICard>>();
            IoC.Kernel.Unbind<ICollection<ICard>>();
            Assert.That(IoC.Kernel.GetBindings(typeof(ICard)).Count(), Is.EqualTo(0));
        }


        [Test]
        public void BindMultipleTimes()
        {
            IoC.Kernel.BindMultipleTimes<ICard>(10).To<ICard, TestCard>();
            Assert.That(IoC.Kernel.GetBindings(typeof(ICard)).Count(), Is.EqualTo(10));

            IoC.Kernel.Bind<ICard>().To<TestCard>();
            Assert.That(IoC.Kernel.GetBindings(typeof(ICard)).Count(), Is.EqualTo(11));
        }



        [TestCase(1)]
        [TestCase(3)]
        [TestCase(55)]
        [TestCase(0)]
        public void BindMultipleTimes(int numberOfBindings)
        {
            IoC.Kernel.Bind<IList<ICard>>().ToConstructor(syntax => new List<ICard>());

            IoC.Kernel.BindMultipleTimes<ICard>(numberOfBindings).To<ICard, TestCard>();
            Assert.That(IoC.Kernel.GetBindings(typeof(ICard)).Count(), Is.EqualTo(numberOfBindings));
        }

        


        [TestCase(0)]
        [TestCase(1)]
        [TestCase(3)]
        [TestCase(55)]
        public void BindMultipleTimesTo(int numberOfBindings)
        {
            IoC.Kernel.BindMultipleTimesTo<ICard, TestCard>(numberOfBindings);
            Assert.That(IoC.Kernel.GetBindings(typeof(ICard)).Count(), Is.EqualTo(numberOfBindings));
        }


        [Test]
        public void WhenAnyAncestorOfType_SelfShouldBeAnAncestor_Level1()
        {
            IoC.Kernel.Unbind<ICard>();
            IoC.Kernel.Bind<ICard>().To<Silver>().WhenAnyAncestorOfType(typeof (IDeck));

            var deck = IoC.Kernel.Get<IDeck>();

            Assert.That(deck.Cards, Is.All.InstanceOf(typeof (Silver)));
            Assert.That(deck.Cards.Count, Is.EqualTo(1));
        }

        [Test]
        public void WhenAnyAncestorOfType_SelfShouldBeAnAncestor_Level0A()
        {
            IoC.Kernel.Unbind<ICard>();
            IoC.Kernel.Bind<ICard>().To<Silver>().WhenAnyAncestorOfType(typeof(ILibrary));

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
            IoC.Kernel.Bind<ICard>().To<Copper>().WhenAnyAncestorOfType(typeof(ISupplyPile));

            var pile = IoC.Kernel.Get<ISupplyPile>();

            Assert.That(pile.Cards, Is.All.InstanceOf(typeof(Copper)));
            Assert.That(pile.Cards.Count, Is.EqualTo(1));
        }

        [Test]
        public void WhenAnyAncestorOfType_Single()
        {
            IoC.Kernel.Bind<ICard>().To<Silver>();
            IoC.Kernel.Bind<ICard>().To<Copper>().WhenAnyAncestorOfType(typeof(TestDeck));
            var witnessdeck = IoC.Kernel.Get<StartingDeck>();
            Assert.That(witnessdeck.Cards, Is.All.InstanceOf(typeof(Silver)));
            Assert.That(witnessdeck.Cards.Count, Is.EqualTo(1));

            IoC.Kernel.Unbind<ICard>(); 
            IoC.Kernel.Bind<ICard>().To<Copper>().WhenAnyAncestorOfType(typeof(TestDeck));
            Assert.That(IoC.Kernel.GetBindings(typeof(ICard)).Count(), Is.EqualTo(1));

            var deck = IoC.Kernel.Get<TestDeck>();
            Assert.That(deck.Cards, Is.All.InstanceOf(typeof(Copper)));
            Assert.That(witnessdeck.Cards.Count, Is.EqualTo(1));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(3)]
        [TestCase(55)]
        public void WhenAnyAncestorOfType_Collection(int collectionSize)
        {
            IoC.Kernel.BindMultipleTimesTo<ICard, Silver>(collectionSize);
            IoC.Kernel.BindMultipleTimesTo<ICard, Copper>(collectionSize).WhenAnyAncestorOfType(typeof(TestDeck)); 
                
            var witnessdeck = IoC.Kernel.Get<StartingDeck>();
            Assert.That(witnessdeck.Cards, Is.All.InstanceOf(typeof(Silver)));
            Assert.That(witnessdeck.Cards.Count, Is.EqualTo(collectionSize));

            IoC.Kernel.Unbind<ICard>();
            IoC.Kernel.BindMultipleTimesTo<ICard, Copper>(collectionSize).WhenAnyAncestorOfType(typeof(TestDeck)); 
            Assert.That(IoC.Kernel.GetBindings(typeof(ICard)).Count(), Is.EqualTo(collectionSize));

            var deck = IoC.Kernel.Get<TestDeck>();
            Assert.That(deck.Cards, Is.All.InstanceOf(typeof(Copper)));
            Assert.That(witnessdeck.Cards.Count, Is.EqualTo(collectionSize));

        }




    }

    



}