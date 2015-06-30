using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;

namespace gbd.Dominion.Contents.Cards
{
    public class Gold : AlwaysInSupplyCard
    {
        public Gold(ICardMechanics mechanics, GameExtension ext, Include inc)
            : base(mechanics, ext, inc) { }

        public override ICardMechanics Mechanics { get; protected set; }

    }
}
