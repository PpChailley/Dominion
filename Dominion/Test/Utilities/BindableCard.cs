using gbd.Dominion.Contents;
using gbd.Dominion.Model.Cards;

namespace gbd.Dominion.Test.Utilities
{
    public class BindableCard: Card, ICard
    {
        public override GameExtension Extension
        {
            get { return GameExtension.TestCards; }
        }

        public override ICardMechanics Mechanics { get; set; }

        public override GameSet PresentInSet
        {
            get { return GameSet.TestCards;}
        }
    }
}
