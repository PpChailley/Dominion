namespace gbd.Dominion.Model.Cards
{
    public abstract class ConditionalCard: AbstractCard
    {
        

        public override GameSet PresentInSet
        {
            get { return GameSet.Conditional; }
        }
    }
}
