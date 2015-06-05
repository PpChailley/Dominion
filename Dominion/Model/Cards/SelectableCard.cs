namespace org.gbd.Dominion.Model.Cards
{
    public abstract class SelectableCard: AbstractCard
    {
        public override GameSet PresentInSet
        {
            get { return GameSet.Selectable; }
        }
    }
}
