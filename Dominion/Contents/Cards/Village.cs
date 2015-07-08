using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;

namespace gbd.Dominion.Contents.Cards
{
    public class Village : Card, ICard
    {
        public Village(ICardMechanics mechanics, GameExtension ext, Include inc) 
            : base(mechanics, ext, inc) { }

        public ActionContinue Continue { get { return ActionContinue.ActionProvider; } }


        public override ICardMechanics Mechanics { get; protected set; }

        
    }
}