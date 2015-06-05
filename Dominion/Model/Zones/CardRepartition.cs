using System;
using System.Collections.Generic;

namespace org.gbd.Dominion.Model
{
    public class CardRepartition
    {


        public readonly int InLibrary;
        public readonly int InHand;
        public readonly int InDiscard;
        public readonly int InBattleField;

        public CardRepartition(int library, int hand, int discard, int battlefield)
        {
            InLibrary = library;
            InHand = hand;
            InDiscard = discard;
            InBattleField = battlefield;
        }

        public override string ToString()
        {
            return String.Format("Library {0} - Hand {1} - Discard {2} - BattleField {3}", 
                InLibrary, InHand, InDiscard, InBattleField);
        }

        /*
        public override bool Equals(object obj)
        {
            if (obj is CardRepartition)
            {
                return          ((CardRepartition) obj).InBattleField == this.InBattleField
                           &&   ((CardRepartition) obj).InDiscard == this.InDiscard
                           &&   ((CardRepartition) obj).InHand == this.InHand
                           &&   ((CardRepartition) obj).InLibrary == this.InLibrary;
            }

            else return false;
        }
         * */


        public override bool Equals(object obj)
        {
            if (obj is CardRepartition == false)
                return false;

            var repartition = (CardRepartition) obj;
            return InLibrary == repartition.InLibrary && InHand == repartition.InHand && InDiscard == repartition.InDiscard && InBattleField == repartition.InBattleField;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = InLibrary;
                hashCode = (hashCode * 397) ^ InHand;
                hashCode = (hashCode * 397) ^ InDiscard;
                hashCode = (hashCode * 397) ^ InBattleField;
                return hashCode;
            }
        }

        public class EqualityComparer : IEqualityComparer<CardRepartition>
        {
            public bool Equals(CardRepartition x, CardRepartition y)
            {
                return x.Equals(y);
            }

            public int GetHashCode(CardRepartition obj)
            {
                return obj.GetHashCode();
            }
        }

    }
}