using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;

namespace gbd.Dominion.Contents.Cards
{
    public class CouncilRoom : Card, ICard
    {
        public CouncilRoom(ICardMechanics mechanics, GameExtension ext, Include inc) 
            : base(mechanics, ext, inc) { }
        public ActionContinue Continue { get { return ActionContinue.TerminalDraw; } }

        public override ICardMechanics Mechanics { get; protected set; }

        
    }
}