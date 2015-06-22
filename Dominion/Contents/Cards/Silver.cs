using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;

namespace gbd.Dominion.Contents.Cards
{
    public class Silver : AlwaysInSupplyCard
    {
        public Silver()
        {
            Mechanics.Cost = new Resources(3);
            Mechanics.Types.Add(new TreasureType(2));
        }
    }
}
