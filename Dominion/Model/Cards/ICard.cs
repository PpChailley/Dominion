using System.Collections.Generic;
using gbd.Dominion.Contents;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.Zones;

namespace gbd.Dominion.Model.Cards

{
    public interface ICard
    {

        string PrintedText { get; }

        GameExtension Extension { get; }

        Include PresentInSet { get; }

        ActionContinue Continue { get; }



        IZone Zone { get; set; }

        ICardMechanics Mechanics { get; }

        IList<CardAttribute> Attributes { get; }

        void Ready(IZone zone);
        
    }
}
