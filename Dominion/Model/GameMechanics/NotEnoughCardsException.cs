using System;

namespace gbd.Dominion.Model.GameMechanics
{
    public class NotEnoughCardsException : Exception
    {
        public NotEnoughCardsException(string s) : base(s){ }

        public NotEnoughCardsException() { }
    }
}