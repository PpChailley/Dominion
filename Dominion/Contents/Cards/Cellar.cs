using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;

namespace gbd.Dominion.Contents.Cards
{
    public class Cellar: Card, ICard
    {
        public Cellar(ICardMechanics mechanics, GameExtension ext, Include inc) 
            : base(mechanics, ext, inc) { }

        public ActionContinue Continue { get { return ActionContinue.Terminal; } }

        public override ICardMechanics Mechanics { get; protected set; }
    }
}