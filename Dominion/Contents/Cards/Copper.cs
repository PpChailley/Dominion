using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;

namespace gbd.Dominion.Contents.Cards
{
    public class Copper : Card, ICard
    {
        public Copper(ICardMechanics mechanics, GameExtension ext, Include inc)
            : base(mechanics, ext, inc) { }


        public ActionContinue Continue { get { return ActionContinue.NotAnAction; } }

        public override ICardMechanics Mechanics { get; protected set; }
    }
}
