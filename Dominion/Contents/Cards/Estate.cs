using gbd.Dominion.Model;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;

namespace gbd.Dominion.Contents
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