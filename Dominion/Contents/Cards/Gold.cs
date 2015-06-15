using org.gbd.Dominion.Model;
using org.gbd.Dominion.Model.Cards;
using org.gbd.Dominion.Model.GameMechanics;

namespace org.gbd.Dominion.Contents
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
