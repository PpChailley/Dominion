using gbd.Dominion.Tools;
using Ninject;

namespace gbd.Dominion.Model.GameMechanics.Actions
{
    public class Draw: GameAction, IGameAction
    {
        
        public int Amount;


        public Draw(int amount = 1)
        {
            Amount = amount;
        }


        public override void Do()
        {
            IoC.Kernel.Get<IGame>().CurrentPlayer.Draw();
        }
    }
}
