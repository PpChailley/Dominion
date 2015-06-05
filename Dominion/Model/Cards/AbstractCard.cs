using System;
using System.Collections.Generic;
using org.gbd.Dominion.Contents;

namespace org.gbd.Dominion.Model.Cards
{
    public abstract class AbstractCard: ICard
    {
        public String Text;
        // TODO: Change type to more precise 
        public Object Illustration;

        public abstract GameExtension Extension { get; }
        public abstract GameSet PresentInSet { get; }

        public CardMetadata Meta;


        private readonly CardMechanics _mechanics = new CardMechanics();
        private readonly IList<CardAttribute> _attributes = new List<CardAttribute>();

        public IList<CardAttribute> Attributes
        {
            get { return _attributes; }
        }

        public CardMechanics Mechanics
        {
            get { return _mechanics; }
        }


        /* TODO: deal with these metadata later
        protected AbstractCard(CardOrigin origin, int indexInOrigin)
        {
            this.Meta.Origin = origin;
            this.Meta.IndexInOrigin = indexInOrigin;
        }
         * */



        public void ClearInPlayAttributes()
        {
            this.Attributes.Clear();
        }



    }
}
