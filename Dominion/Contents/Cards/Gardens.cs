using gbd.Dominion.Model.Cards;

namespace gbd.Dominion.Contents.Cards
{
    public class Gardens: SelectableCard
    {
        public Gardens(CardMechanics m, GameExtension e) : base(m, e) { }
        
        public override GameExtension Extension
        {
            get { return GameExtension.BaseGame; }
        }

        public override ICardMechanics Mechanics { get; protected set; }

        
    }
}