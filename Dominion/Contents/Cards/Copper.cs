using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;

namespace gbd.Dominion.Contents.Cards
{
    public class Copper : AlwaysInSupplyCard, ICard
    {
        public Copper(ICardMechanics mechanics, GameExtension ext, Include inc)
            : base(mechanics, ext, inc) { }


        public override ICardMechanics Mechanics { get; protected set; }
    }
}
