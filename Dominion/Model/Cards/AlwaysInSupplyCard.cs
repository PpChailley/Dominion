using org.gbd.Dominion.Contents;

namespace org.gbd.Dominion.Model.Cards
{
    /// <summary>
    /// A card that is always in the supply, whatever the game. Like copper, curse, province...
    /// </summary>
    public abstract class AlwaysInSupplyCard: AbstractCard
    {
        public override GameSet PresentInSet
        {
            get { return GameSet.AlwaysIncluded; }
        }

        public override GameExtension Extension
        {
            get { return GameExtension.AlwaysPresent; }
        }
    }
}
