using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.GameMechanics.Actions;

namespace gbd.Dominion.Model.Actions
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
