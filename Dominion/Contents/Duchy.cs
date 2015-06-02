using org.gbd.Dominion.Model;
using org.gbd.Dominion.Model.Cards;

namespace org.gbd.Dominion.Contents
{
    public class Duchy : Card, ICard
    {
        public Duchy()
        {
            Mechanics.Cost = new Resources(5);
            Mechanics.Types.Add(new Victory(3));
        }
    }
}