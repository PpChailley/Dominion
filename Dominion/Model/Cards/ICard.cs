using System;
using System.Collections.Generic;
using gbd.Dominion.Contents;
using gbd.Dominion.Model.Zones;

namespace gbd.Dominion.Model.Cards

{
    public interface ICard
    {
        GameExtension Extension { get; }

        IZone Zone { get; set; }

        ICardMechanics Mechanics { get; }

        IList<CardAttribute> Attributes { get; set; }

        void Ready(IZone zone);
        
    }
}
