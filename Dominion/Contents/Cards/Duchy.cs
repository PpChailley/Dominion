using org.gbd.Dominion.Model;
using org.gbd.Dominion.Model.Cards;
using org.gbd.Dominion.Model.GameMechanics;

namespace org.gbd.Dominion.Contents
{
    public class Duchy : BaseSetCard
    {
        public Duchy()
        {
            Mechanics.Cost = new Resources(5);
            Mechanics.Types.Add(new Victory(3));
        }
    }
}