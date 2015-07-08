using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;

namespace gbd.Dominion.Contents.Cards
{
    public class Market : Card, ICard
    {
        public Market(ICardMechanics mechanics, GameExtension ext, Include inc) 
            : base(mechanics, ext, inc) { }


        public ActionContinue Continue { get { return ActionContinue.Cantrip; } }

        public override ICardMechanics Mechanics { get; protected set; }

        
    }
}