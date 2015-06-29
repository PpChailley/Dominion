using gbd.Dominion.Model.Cards;

namespace gbd.Dominion.Contents.Cards
{
    public class CouncilRoom : SelectableCard
    {
        public CouncilRoom(GameExtension extension) : base(extension) { }
        
        public override GameExtension Extension
        {
            get { return GameExtension.BaseGame; }
        }

        public override ICardMechanics Mechanics { get; protected set; }

        
    }
}