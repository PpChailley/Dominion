namespace gbd.Dominion.Model.Cards
{

    /// <summary>
    /// A card that can be included in the supply or not, as selected by the game creator. 
    /// Like Platinum or any Ruins
    /// </summary>
    public abstract class OptionalCard: AbstractCard
    {
        public override GameSet PresentInSet
        {
            get { return GameSet.Optional; }
        }
    }
}
