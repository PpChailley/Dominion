using System;
using gbd.Dominion.Contents;
using gbd.Dominion.Model.Cards;

namespace gbd.Dominion.Test.Utilities
{

    /// <summary>
    /// A card that is included only in test games
    /// </summary>
    public class TestCard : Card, ICard
    {
        public override ICardMechanics Mechanics { get; set; }


        public static int LastIndex = 0;

        public int Index;

        public static void ResetCounters()
        {
            LastIndex = 0;
        }


        public TestCard()
        {
            Index = LastIndex ++;
        }

        public override string ToString()
        {
            return String.Format("{0}  # {1}", this.GetType().Name, Index);
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