using System.Collections.Generic;
using gbd.Dominion.Contents;

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
