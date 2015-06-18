using gbd.Dominion.Model;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;

namespace gbd.Dominion.Contents
{
    public class Duchy : AlwaysInSupplyCard
    {
        public Duchy()
        {
            Mechanics.Cost = new Resources(5);
            Mechanics.Types.Add(new Victory(3));
        }
    }
}