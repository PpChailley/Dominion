using System;
using gbd.Dominion.Contents.Cards;
using gbd.Dominion.Injection;
using gbd.Dominion.Model;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Test.Utilities;
using gbd.Tools.NInject;
using NUnit.Framework;

namespace gbd.Dominion.Test.ToolsLib
{
    [TestFixture]
    public class NInjectExtensionsRobustness : BaseTest
    {
        [ExpectedException(typeof (InvalidOperationException))]
        [TestCase(-1)]
        [TestCase(-100)]
        public void BindMultipleTimes(int numberOfBindings)
        {
            IoC.Kernel.Bind<ICard>(numberOfBindings).To<ICard, EmptyCard>();
        }


        [ExpectedException(typeof (ArgumentOutOfRangeException))]
        [TestCase(-1)]
        [TestCase(-100)]
        public void BindMultipleTimesTo(int numberOfBindings)
        {
            IoC.Kernel.BindTo<ICard, EmptyCard>(numberOfBindings);
        }


        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestCase(-1)]
        [TestCase(-100)]
        public void WhenAnyAncestorOfType_Collection(int collectionSize)
        {
            IoC.Kernel.BindTo<ICard, Copper>(collectionSize)
                .WhenAnyAncestorOfType(typeof (TestDeck));
        }
    }
}
