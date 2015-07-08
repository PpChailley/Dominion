using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;

namespace gbd.Dominion.Contents.Cards
{
    public class Gardens : Card, ICard
    {
        public Gardens(ICardMechanics mechanics, GameExtension ext, Include inc)
            : base(mechanics, ext, inc) { }

        public ActionContinue Continue { get { return ActionContinue.NotAnAction; } }

        public override ICardMechanics Mechanics { get; protected set; }

        
    }
}