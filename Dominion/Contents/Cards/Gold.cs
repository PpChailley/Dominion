using gbd.Dominion.Model;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;

namespace gbd.Dominion.Contents
{
    public class Gold : AlwaysInSupplyCard
    {
        public Gold()
        {
            Mechanics.Cost = new Resources(6);
            Mechanics.Types.Add(new Treasure(3));
        }
    }
}
