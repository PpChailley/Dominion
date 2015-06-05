using org.gbd.Dominion.Contents;

namespace org.gbd.Dominion.Model.Cards
{
    public abstract class BaseSetCard: AbstractCard
    {
        public override GameSet PresentInSet
        {
            get { return GameSet.BaseSet; }
        }

        public override GameExtension Extension
        {
            get { return GameExtension.AlwaysPresent; }
        }
    }
}
