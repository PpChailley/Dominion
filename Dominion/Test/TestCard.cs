using System;
using org.gbd.Dominion.Contents;
using org.gbd.Dominion.Model;
using org.gbd.Dominion.Model.Cards;

namespace org.gbd.Dominion.Test
{

    /// <summary>
    /// A card that is included in the supply only in test games
    /// </summary>
    public class TestCard : AbstractCard, ICard
    {
        public static int LastIndex = 0;

        public int Index;

        public TestCard()
        {
            Index = LastIndex ++;
        }

        public override string ToString()
        {
            return String.Format("Test Card # {0}", Index);
        }

        public override GameExtension Extension
        {
            get { return GameExtension.TestCards; }
        }

        public override GameSet PresentInSet
        {
            get { return GameSet.TestCards; }
        }
    }
}