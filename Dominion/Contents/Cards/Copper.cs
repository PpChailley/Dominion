using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;

namespace gbd.Dominion.Contents.Cards
{
    class Copper : AlwaysInSupplyCard, ICard
    {
        public override ICardMechanics Mechanics { get; set; }
    }
}
