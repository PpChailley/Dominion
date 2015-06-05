using org.gbd.Dominion.Model;
using org.gbd.Dominion.Model.Cards;

namespace org.gbd.Dominion.Contents.Cards
{
    class Copper : BaseSetCard, ICard
    {
        public Copper()
        {
            Mechanics.Cost = new Resources(0);
            Mechanics.Types.Add(new Treasure(1));
        }
    }
}
