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
            int discarded = player.ChooseAndDiscard(0, int.MaxValue);
            player.Draw(discarded + _drawMoreThanDiscard);
        }
    }
}