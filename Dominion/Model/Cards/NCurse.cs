using System;

namespace org.gbd.Dominion.Model.Cards
{
    public class Curse : ICardType
    {
        public int CurseValue;


        public Curse(int value)
        {
            if (value > 0)
                throw new InvalidOperationException("Curse value must be negative or null");
            CurseValue = value;
        }
    }
}