using org.gbd.Dominion.Model.GameMechanics;
using org.gbd.Dominion.Model.GameMechanics.Actions;

namespace org.gbd.Dominion.Model.Actions
{
    public class Draw: GameActionTargetingPlayers, IGameActionTargetingPlayers
    {
        
        public int Amount;


        public Draw(int amount = 1)
        {
            Amount = amount;
        }



        protected override void DoToPlayer(Player p)
        {
            p.Draw(this.Amount);
        }
    }
}
