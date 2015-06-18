using System;

namespace gbd.Dominion.Model.GameMechanics
{
    public class DeckEmptyException : NotEnoughCardsException
    {
        public DeckEmptyException(string s) : base(s) { }

        public DeckEmptyException() : base(String.Empty) { }
    }
}