using System;
using System.Collections.Generic;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;

namespace gbd.Dominion.Model.Zones
{
    public interface ILibrary: IZone, IMutableZone
    {
        void Ready(IDeck deck);
    }
}