namespace gbd.Dominion.Model.GameMechanics.Actions
{
    public class Draw: GameActionTargetingPlayers, IGameActionTargetingPlayers
    {
        
        public int Amount;


        public Draw(int amount = 1)
        {
            Amount = amount;
        }



        protected override void DoToPlayer(IPlayer p)
        {
            p.Draw(this.Amount);
        }
    }
}
