using gbd.Dominion.Model;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;

namespace gbd.Dominion.Contents
{
    public class Curse: AlwaysInSupplyCard
    {
        public Curse()
        {
            Mechanics.Cost = new Resources(0);
            Mechanics.Types.Add(new Model.Cards.Curse(-1));
        }
    }
}

