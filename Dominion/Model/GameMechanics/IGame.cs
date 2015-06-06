using System.Collections.Generic;
using Ninject;
using org.gbd.Dominion.Model.Zones;
using org.gbd.Dominion.Tools;

namespace org.gbd.Dominion.Model.GameMechanics
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