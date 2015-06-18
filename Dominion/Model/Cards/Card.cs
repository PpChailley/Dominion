using System.Collections.Generic;

namespace gbd.Dominion.Model.Cards
{
    public abstract class Card: PrintedCard, ICard
    {
        protected Card()
        {
            Attributes = new List<CardAttribute>();
            Mechanics = new CardMechanics();
        }

        public IList<CardAttribute> Attributes { get; protected set; }

        public CardMechanics Mechanics { get; protected set; }


        public void ClearInPlayAttributes()
        {
            this.Attributes.Clear();
        }


    }
}
