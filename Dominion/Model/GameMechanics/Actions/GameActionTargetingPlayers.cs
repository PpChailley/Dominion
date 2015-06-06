using System.Collections.Generic;
using org.gbd.Dominion.Model.Actions;

namespace org.gbd.Dominion.Model.GameMechanics.Actions
{
    public abstract class GameActionTargetingPlayers : IGameActionTargetingPlayers
    {

        public ICollection<PlayerChoice> DesignatedPlayers;


        public void Do()
        {
            foreach (var player in Player.Get(DesignatedPlayers))
            {
                this.DoToPlayer(player);
            }
        }

        protected abstract void DoToPlayer(IPlayer p);

    }
}