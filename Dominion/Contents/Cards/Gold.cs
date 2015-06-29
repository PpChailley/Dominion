using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;

namespace gbd.Dominion.Contents.Cards
{
    public class Gold : AlwaysInSupplyCard
    {
        public override ICardMechanics Mechanics { get; protected set; }
        public Gold()
        {
            //Mechanics.Cost = new Resources(6);
            //Mechanics.Types.Add(new TreasureType(3));
        }
    }
}
