using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;

namespace gbd.Dominion.Contents.Cards
{
    public class Militia: Card, ICard
    {
        public Militia(ICardMechanics mechanics, GameExtension ext, Include inc) 
            : base(mechanics, ext, inc) { }

        public ActionContinue Continue { get { return ActionContinue.Terminal; } }

        public override ICardMechanics Mechanics { get; protected set; }
    }
}