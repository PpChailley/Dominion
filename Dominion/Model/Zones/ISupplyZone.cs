using System.Collections.Generic;

namespace gbd.Dominion.Model.Zones
{
    public interface ISupplyZone: IZone
    {
        IList<ISupplyPile> Piles { get; }


        void MakeReadyToStartGame();
    }
}