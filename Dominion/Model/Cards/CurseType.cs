using System;

namespace gbd.Dominion.Model.Cards
{
    public class CurseType : ICardType
    {
        public int CurseValue;


        public CurseType(int value)
        {
            if (value > 0)
                throw new InvalidOperationException("Curse value must be negative or null");
            CurseValue = value;
        }
    }
}