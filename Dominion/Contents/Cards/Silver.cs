using org.gbd.Dominion.Model;
using org.gbd.Dominion.Model.Cards;
using org.gbd.Dominion.Model.GameMechanics;

namespace org.gbd.Dominion.Contents
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
