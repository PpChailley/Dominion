using System;

namespace gbd.Dominion.Model.GameMechanics.Actions
{
    public class Discard:GameActionTargetingPlayers, IGameActionTargetingPlayers
    {
        public int Amount;

        public Discard(int amount)
        {
            Amount = amount;
        }


        //TODO: Try to map this action model to that of GameMechanics

        /*
        protected override void DoToPlayer(Player p)
        {
            p.DiscardFromHand(Amount);
        }

        protected override void DoToPlayer(Player p)
        {
            throw new System.NotImplementedException();
        }
         * 
         * */
        protected override void DoToPlayer(IPlayer p)
        {
            throw new NotImplementedException();
        }
    }
}
