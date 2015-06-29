using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;

namespace gbd.Dominion.Contents.Cards
{
    public class Province : AlwaysInSupplyCard
    {
        public override ICardMechanics Mechanics { get; protected set; }

        public Province()
        {
            //Mechanics.Cost = new Resources(8);
            //Mechanics.Types.Add(new VictoryType(6));
        }
    }
}