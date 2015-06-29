using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;

namespace gbd.Dominion.Contents.Cards
{
    public class Duchy : AlwaysInSupplyCard
    {
        public override ICardMechanics Mechanics { get; set; }

        public Duchy()
        {
            //Mechanics.Cost = new Resources(5);
            //Mechanics.Types.Add(new VictoryType(3));
        }
    }
}