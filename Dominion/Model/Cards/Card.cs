﻿using System;
using System.Collections.Generic;
using gbd.Dominion.Model.Zones;
using gbd.Dominion.Tools;
using Ninject;

namespace gbd.Dominion.Model.Cards
{
    public abstract class Card: PrintedCard, ICard
    {

        public IZone Zone { get; set; }
        
        
        public ICardMechanics Mechanics { get; protected set; }  

        
        public IList<CardAttribute> Attributes { get; set; }


        protected Card()
        {
            Attributes = new List<CardAttribute>();
            // TODO: use constructor injection if possible
            Mechanics = IoC.Kernel.Get<ICardMechanics>();
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
