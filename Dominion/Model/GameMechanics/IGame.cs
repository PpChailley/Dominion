using System.Collections.Generic;
using gbd.Dominion.Model.Zones;
using gbd.Dominion.Model.GameMechanics;

namespace gbd.Dominion.Model.GameMechanics
{
    public interface IGame
    {

        ICollection<IPlayer> Players { get; set; }
        IPlayer CurrentPlayer { get; }
        ISupplyZone SupplyZone { get; }


        void MakeReadyToStart();

        void Init();
    }
}