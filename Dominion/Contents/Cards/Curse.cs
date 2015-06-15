using org.gbd.Dominion.Model;
using org.gbd.Dominion.Model.Cards;
using org.gbd.Dominion.Model.GameMechanics;

namespace org.gbd.Dominion.Contents
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

