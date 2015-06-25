namespace gbd.Dominion.Model.GameMechanics.Actions
{
    public abstract class GameAction
    {
        public abstract void Do();

        public override string ToString()
        {
            return this.GetType().Name;
        }
    }
}