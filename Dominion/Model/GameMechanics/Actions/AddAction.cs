using gbd.Dominion.Tools;
using Ninject;

namespace gbd.Dominion.Model.GameMechanics.Actions
{
    public class AddAction: GameAction, IGameAction
    {
        public int Amount { get; private set; }

        public AddAction(int amount)
        {
            Amount = amount;
        }

        public override void Do()
        {
            IoC.Kernel.Get<IGame>().CurrentPlayer.AvailableActions += Amount;
        }
    }
}