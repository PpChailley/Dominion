namespace gbd.Dominion.Model.Cards
{
    public abstract class ConditionalCard: Card
    {
        

        public override GameSet PresentInSet
        {
            get { return GameSet.Conditional; }
        }
    }
}
