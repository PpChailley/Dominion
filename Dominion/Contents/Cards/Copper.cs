using org.gbd.Dominion.Model;
using org.gbd.Dominion.Model.Cards;
using org.gbd.Dominion.Model.GameMechanics;

namespace org.gbd.Dominion.Contents.Cards
{
    class Copper : AlwaysInSupplyCard, ICard
    {
        public Copper()
        {
            Mechanics.Cost = new Resources(0);
            Mechanics.Types.Add(new Treasure(1));
        }
    }
}
