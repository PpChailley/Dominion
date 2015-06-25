﻿using System;
using System.Collections.Generic;
using System.Security.Policy;
using gbd.Dominion.Contents;
using gbd.Dominion.Model.Zones;
using gbd.Dominion.Tools;
using gbd.Tools.Cli;
using Ninject;

namespace gbd.Dominion.Model.Cards
{
    public abstract class Card: PrintedCard, ICard
    {

        public IZone Zone { get; private set; }
        
        
        
        public ICardMechanics Mechanics { get; set; }  

        
        public IList<CardAttribute> Attributes { get; protected set; }


        protected Card()
        {
            Attributes = new List<CardAttribute>();
            Mechanics = IoC.Kernel.Get<ICardMechanics>();

        }
        
        


        public string PrintedText
        {
            get { return Mechanics.PrintedText; }
        }



        public void ClearInPlayAttributes()
        {
            this.Attributes.Clear();
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
