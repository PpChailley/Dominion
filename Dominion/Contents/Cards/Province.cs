using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;

namespace gbd.Dominion.Contents.Cards
{
    public class Province : Card, ICard
    {
        public Province(ICardMechanics mechanics, GameExtension ext, Include inc)
            : base(mechanics, ext, inc) { }


        public ActionContinue Continue { get { return ActionContinue.NotAnAction; } }

        public override ICardMechanics Mechanics { get; protected set; }

    }
}