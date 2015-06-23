using gbd.Dominion.Tools;
using Ninject;

namespace gbd.Dominion.Model.GameMechanics.Actions
{
    public class AddBuy : GameAction
    {
        public int Amount;

        public AddBuy(int amount)
        {
            Amount = amount;
        }

        public override void Do()
        {
            IoC.Kernel.Get<IGame>().CurrentPlayer.AvailableBuys += Amount;
        }
    }
}