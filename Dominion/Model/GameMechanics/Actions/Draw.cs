using System.Linq;
using gbd.Dominion.Injection;
using Ninject;

namespace gbd.Dominion.Model.GameMechanics.Actions
{
    public class Draw: GameAction, IGameAction
    {
        private readonly PlayerChoice _who;
        private readonly int _amount;


        public Draw(int amount)
        {
            _amount = amount;
        }

        public Draw(int amount, PlayerChoice who): this(amount)
        {
            _who = who;
        }


        public override void Do()
        {
            IoC.Kernel.Get<IGame>().GetPlayers(_who).ToList().ForEach(p => p.Draw(_amount));
        }
    }
}
