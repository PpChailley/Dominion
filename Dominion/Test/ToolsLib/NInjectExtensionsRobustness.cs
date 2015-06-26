using System;
using gbd.Dominion.Contents.Cards;
using gbd.Dominion.Model;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Test.Utilities;
using gbd.Dominion.Tools;
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
            IoC.Kernel.BindMultipleTimes<ICard>(numberOfBindings).To<ICard, TestCard>();
        }


        [ExpectedException(typeof (InvalidOperationException))]
        [TestCase(-1)]
        [TestCase(-100)]
        public void BindMultipleTimesTo(int numberOfBindings)
        {
            IoC.Kernel.BindMultipleTimesTo<ICard, TestCard>(numberOfBindings);
        }


        [ExpectedException(typeof (InvalidOperationException))]
        [TestCase(-1)]
        [TestCase(-100)]
        public void WhenAnyAncestorOfType_Collection(int collectionSize)
        {
            IoC.Kernel.BindMultipleTimesTo<ICard, Copper>(collectionSize)
                .WhenAnyAncestorOfType(typeof (TestDeck));
        }
    }
}
