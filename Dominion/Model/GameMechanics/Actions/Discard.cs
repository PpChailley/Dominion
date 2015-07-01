using System.Linq;
using gbd.Dominion.Injection;
using Ninject;

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
            IoC.Kernel.Get<IGame>().GetPlayers(Who).ToList().ForEach(p => p.I.Discard(Amount));
        }
    }
}
