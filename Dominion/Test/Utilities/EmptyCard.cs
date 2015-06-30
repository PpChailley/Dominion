using System;
using gbd.Dominion.Contents;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.GameMechanics.Actions;

namespace gbd.Dominion.Test.Utilities
{

    /// <summary>
    /// A card that is included only in test games
    /// </summary>
    class EmptyCard : Card, ICard
    {
        


        public static int LastIndex = 0;

        public int Index;

        public static void ResetCounters()
        {
            LastIndex = 0;
        }


        public EmptyCard()
        {
            Index = LastIndex ++;
            Mechanics = new CardMechanics(  new Resources(0), 
                                            new ICardType[0], 
                                            new IGameAction[0], 
                                            new IGameAction[0]);
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