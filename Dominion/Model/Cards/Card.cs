using System;
using System.Collections.Generic;
using gbd.Dominion.Model.Zones;
using gbd.Dominion.Tools;
using Ninject;

namespace gbd.Dominion.Model.Cards
{
    public abstract class Card: PrintedCard, ICard
    {

        public IZone Zone { get; set; }
        
        
        [Inject]
        public ICardMechanics Mechanics { get;  set; }  

        
        public IList<CardAttribute> Attributes { get; set; }


        protected Card()
        {
            Attributes = new List<CardAttribute>();
        }
        
        

        public void Ready(IZone zone)
        {
            Zone = zone;

            foreach (var cardType in this.Mechanics.Types)
            {
                cardType.Ready(zone);
            }
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
