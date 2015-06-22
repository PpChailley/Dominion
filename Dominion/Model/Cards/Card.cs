using System;
using System.Collections.Generic;
using gbd.Dominion.Contents;
using Ninject;

namespace gbd.Dominion.Model.Cards
{
    public abstract class Card: PrintedCard, ICard
    {

        [Inject]
        public ICardMechanics Mechanics { get; set; }

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






    }
}
