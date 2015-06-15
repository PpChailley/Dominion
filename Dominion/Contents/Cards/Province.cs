using org.gbd.Dominion.Model;
using org.gbd.Dominion.Model.Cards;
using org.gbd.Dominion.Model.GameMechanics;

namespace org.gbd.Dominion.Contents
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