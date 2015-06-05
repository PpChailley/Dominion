namespace org.gbd.Dominion.Model.Cards
{
    public abstract class OptionalCard: AbstractCard
    {
        public override GameSet PresentInSet
        {
            get { return GameSet.Optional; }
        }
    }
}
