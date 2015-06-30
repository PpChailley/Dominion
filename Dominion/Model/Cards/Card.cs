using System;
using System.Collections.Generic;
using gbd.Dominion.Contents;
using gbd.Dominion.Model.Zones;
using Ninject;

namespace gbd.Dominion.Model.Cards
{
    public abstract class Card: PrintedCard, ICard
    {

        public IZone Zone { get; set; }
        
        // This has to stay abstract so that NInject will see the implementing type when injecting it
        public abstract ICardMechanics Mechanics { get; protected set; }

        [Inject]
        public IList<CardAttribute> Attributes { get; protected set; }


        [Inject]
        protected Card(ICardMechanics mechanics, GameExtension ext, Include inc) : base(ext, inc)
        {
            Attributes = new List<CardAttribute>();
            Mechanics = mechanics;
        }



        public void Ready(IZone zone)
        {
            Zone = zone;

            foreach (var cardType in this.Mechanics.Types)
            {
                cardType.Ready(zone);
            }
        }



        public string PrintedText
        {
            get { return Mechanics.PrintedText; }
        }

        public override string ToString()
        {
            return String.Format("{0} # {1} (in {3} with {{{2}}}",
                GetType().Name,
                GetHashCode(),
                Mechanics,
                Zone
                );
        }
    }
}
