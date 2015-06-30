using gbd.Dominion.Model.Cards;

namespace gbd.Dominion.Contents.Cards
{
    public class Gardens: SelectableCard
    {
        public Gardens(ICardMechanics mechanics, GameExtension ext, Include inc)
            : base(mechanics, ext, inc) { }

        public override ICardMechanics Mechanics { get; protected set; }

        
    }
}