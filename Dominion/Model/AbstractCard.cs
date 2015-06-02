using System;
using System.Collections.Generic;
using org.gbd.Dominion.Model.Cards;

namespace org.gbd.Dominion.Model
{
    public abstract class AbstractCard: ICard
    {
        public String Text;

        private readonly CardMechanics _mechanics = new CardMechanics();

        public abstract GameSet PresentInSet { get; }


        // TODO: Change type to more precise 
        public Object Illustration;

        private readonly IList<CardAttribute> _attributes = new List<CardAttribute>();

        public IList<CardAttribute> Attributes
        {
            get { return _attributes; }
        }


        public CardMechanics Mechanics
        {
            get { return _mechanics; }
        }


    }
}
