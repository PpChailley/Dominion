using System.Linq;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.GameMechanics.Actions;
using Ninject;

namespace gbd.Dominion.Injection
{
    internal class DiscardAndDraw : GameAction, IGameAction
    {
        private readonly int _drawMoreThanDiscard;

        public DiscardAndDraw(int drawMoreThanDiscard)
        {
            _drawMoreThanDiscard = drawMoreThanDiscard;
        }

        public override void Do()
        {
            var player = IoC.Kernel.Get<IGame>().CurrentPlayer;
            int discarded = player.I.Discard(0, int.MaxValue).Count();
            player.Draw(discarded + _drawMoreThanDiscard);
        }
    }
}