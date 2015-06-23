using System;
using System.Collections.Generic;
using gbd.Dominion.Contents;
using gbd.Tools.Cli;
using Ninject;

namespace gbd.Dominion.Model.Cards
{
    public abstract class Card: PrintedCard, ICard
    {

        [Inject]
        public abstract ICardMechanics Mechanics { get; set; }

        [Inject]
        public IList<CardAttribute> Attributes { get; protected set; }


        protected Card()
        {
            Attributes = new List<CardAttribute>();
        }
        
        


        public string PrintedText
        {
            get { return Mechanics.PrintedText; }
        }



        public void ClearInPlayAttributes()
        {
            this.Attributes.Clear();
        }


        public override string ToString()
        {
            return String.Format("{0} # {1} with {{{2}}}",
                GetType().Name,
                GetHashCode(),
                Mechanics);
        }
    }
}
