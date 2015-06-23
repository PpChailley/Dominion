using System;
using System.Collections.Generic;
using gbd.Dominion.Contents;
using gbd.Dominion.Model.Zones;

namespace gbd.Dominion.Model.Cards

{
    public interface ICard
    {
        GameExtension Extension { get; }

        IZone Zone { get; }

        ICardMechanics Mechanics { get; }

        IList<CardAttribute> Attributes { get; }

        String PrintedText { get; }

        void ClearInPlayAttributes();
        void Ready(IZone zone);
        
    }
}
