using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;

namespace gbd.Dominion.Contents.Cards
{
    class Copper : AlwaysInSupplyCard, ICard
    {
        public Copper()
        {
            Mechanics.Cost = new Resources(0);
            Mechanics.Types.Add(new TreasureType(1));
        }
    }
}
