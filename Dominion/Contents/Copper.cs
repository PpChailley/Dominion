using org.gbd.Dominion.Model;
using org.gbd.Dominion.Model.Cards;

namespace org.gbd.Dominion.Contents
{
    class Copper: Card
    {
        public Copper()
        {
            Mechanics.Cost = new Resources(0);
            Mechanics.Types.Add(new Treasure(1));
        }
    }
}
