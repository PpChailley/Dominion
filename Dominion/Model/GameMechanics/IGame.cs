using System.Collections.Generic;
using Ninject;
using org.gbd.Dominion.Tools;

namespace org.gbd.Dominion.Model.GameMechanics
{
    public interface IGame
    {

        ICollection<IPlayer> Players { get; set; }
        static Player CurrentPlayer { get; }
        static IZone SupplyZone { get; }


        void MakeReadyToStart();

        void Init();
    }
}