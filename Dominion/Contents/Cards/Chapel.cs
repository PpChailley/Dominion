using gbd.Dominion.Model.Cards;

namespace gbd.Dominion.Contents.Cards
{
    public class Chapel: SelectableCard
    {

        public Chapel(GameExtension extension) : base(extension) { }

        public override ICardMechanics Mechanics { get; protected set; }

        public override GameExtension Extension
        {
            get { return GameExtension.BaseGame; }
        }

        
    }
}