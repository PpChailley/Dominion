using org.gbd.Dominion.Model;
using org.gbd.Dominion.Model.Cards;
using org.gbd.Dominion.Model.GameMechanics;

namespace org.gbd.Dominion.Contents
{
    public class Estate : AlwaysInSupplyCard, ICard
    {
        public Estate()
        {
            Mechanics.Cost = new Resources(2);
            Mechanics.Types.Add(new Victory(1));
        }
    }
}