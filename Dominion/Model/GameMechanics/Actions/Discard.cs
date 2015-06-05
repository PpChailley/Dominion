namespace org.gbd.Dominion.Model.Actions
{
    public class Discard:GameActionTargetingPlayers, IGameActionTargetingPlayers
    {
        public int Amount;

        public Discard(int amount)
        {
            Amount = amount;
        }


        protected override void DoToPlayer(Player p)
        {
            p.DiscardFromHand(Amount);
        }
    }
}
