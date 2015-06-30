using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using gbd.Dominion.Contents;
using gbd.Dominion.Model.Zones;

namespace gbd.Dominion.Model.Cards

{
    public interface ICard
    {

        string PrintedText { get; }

        GameExtension Extension { get; }

        GameSet PresentInSet { get; }


        IZone Zone { get; set; }

        ICardMechanics Mechanics { get; }

        IList<CardAttribute> Attributes { get; }

        void Ready(IZone zone);
        
    }
}
