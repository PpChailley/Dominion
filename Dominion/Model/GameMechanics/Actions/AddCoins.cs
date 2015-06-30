using gbd.Dominion.Injection;
using Ninject;

namespace gbd.Dominion.Model.GameMechanics.Actions
{
    public class AddCoins : GameAction
    {
        public int Amount { get; set; }

        public AddCoins(int amount)
        {
            Amount = amount;
        }


        public override void Do()
        {
            IoC.Kernel.Get<IGame>().CurrentPlayer.AvailableResources.Money += Amount;
        }
    }
}