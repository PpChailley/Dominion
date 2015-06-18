using System;
using gbd.Dominion.Model;
using gbd.Dominion.Model.Cards;

namespace gbd.Dominion.Test.Utilities
{

    /// <summary>
    /// A card that is included only in test games
    /// </summary>
    public class TestCard : Card, ICard
    {
        public static int LastIndex = 0;

        public int Index;

        public TestCard()
        {
            Index = LastIndex ++;
        }

        public override string ToString()
        {
            return String.Format("{0}  with testIndex {1}", this.GetType(), Index);
        }

    }
}