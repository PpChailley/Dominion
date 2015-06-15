using gbd.Dominion.Model;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;

namespace gbd.Dominion.Contents
{
    public class Province : AlwaysInSupplyCard
    {
        public Province()
        {
            Mechanics.Cost = new Resources(8);
            Mechanics.Types.Add(new Victory(6));
        }
    }
}