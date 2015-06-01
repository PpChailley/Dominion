using System.Collections.Generic;

namespace org.gbd.Dominion.Model.Actions
{
    public interface IGameActionTargetingPlayers: IGameAction
    {
    }

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

        protected abstract void DoToPlayer(Player p);

    }
}