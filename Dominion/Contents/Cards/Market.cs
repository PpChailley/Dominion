using gbd.Dominion.Model.Cards;

namespace gbd.Dominion.Contents.Cards
{
    public class Market: SelectableCard
    {
        public Market(ICardMechanics mechanics, GameExtension ext, Include inc) 
            : base(mechanics, ext, inc) { }

        public override ICardMechanics Mechanics { get; protected set; }

        
    }
}