using System.Linq;

namespace gbd.Dominion.Model.GameMechanics.Actions
{
    public class Discard: GameAction, IGameAction
    {
        public int Amount;
        public PlayerChoice Who;

        public Discard(PlayerChoice who, int amount)
        {
            Amount = amount;
            Who = who;
        }


        public override void Do()
        {
            Game.G.GetPlayers(Who).ToList().ForEach(p => p.DiscardFromHand(Amount));
        }
    }
}
