using gbd.Dominion.Model.Cards;

namespace gbd.Dominion.Contents.Cards
{
    public class Market: SelectableCard
    {
        public Market(CardMechanics m, GameExtension e) : base(m, e) { }

        public override GameExtension Extension
        {
            get { return GameExtension.BaseGame; }
        }

        public override ICardMechanics Mechanics { get; protected set; }

        
    }
}