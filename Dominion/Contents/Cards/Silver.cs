using gbd.Dominion.Model;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;

namespace gbd.Dominion.Contents
{
    public class Silver : AlwaysInSupplyCard
    {
        public Silver()
        {
            Mechanics.Cost = new Resources(3);
            Mechanics.Types.Add(new Treasure(2));
        }
    }
}
