using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;

namespace gbd.Dominion.Contents.Cards
{
    public class Copper : AlwaysInSupplyCard, ICard
    {
        public override ICardMechanics Mechanics { get; protected set; }
    }
}
