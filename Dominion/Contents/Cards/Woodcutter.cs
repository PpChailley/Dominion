using gbd.Dominion.Model.Cards;

namespace gbd.Dominion.Contents.Cards
{
    public class Woodcutter: SelectableCard
    {
        public Woodcutter(CardMechanics m, GameExtension e) : base(m, e) { }
        public override ICardMechanics Mechanics { get; protected set; }

        
    }
}