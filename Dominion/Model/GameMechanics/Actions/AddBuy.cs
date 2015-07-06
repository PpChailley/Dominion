using gbd.Dominion.Injection;
using Ninject;

namespace gbd.Dominion.Model.GameMechanics.Actions
{
    public class AddBuy : GameAction, IGameAction
    {
        public int Amount;

        public AddBuy(int amount)
        {
            Amount = amount;
        }

        public override void Do()
        {
            IoC.Kernel.Get<IGame>().CurrentPlayer.Status.Buys += Amount;
        }
    }
}