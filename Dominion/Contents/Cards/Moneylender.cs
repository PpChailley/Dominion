using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;

namespace gbd.Dominion.Contents.Cards
{
    public class Moneylender: Card, ICard
    {
        public Moneylender(ICardMechanics mechanics, GameExtension ext, Include inc) 
            : base(mechanics, ext, inc) { }

        public ActionContinue Continue { get { return ActionContinue.Terminal; } }

        public override ICardMechanics Mechanics { get; protected set; }
    }
}