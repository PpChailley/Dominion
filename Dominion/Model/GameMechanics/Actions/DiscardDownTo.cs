using gbd.Dominion.Injection;
using Ninject;

namespace gbd.Dominion.Model.GameMechanics.Actions
{
    internal class DiscardDownTo : GameAction
    {
        private readonly PlayerChoice _who;
        private readonly int _amount;

        public DiscardDownTo(PlayerChoice who, int amount)
        {
            _who = who;
            _amount = amount;
        }

        public override void Do()
        {
            foreach (var player in IoC.Kernel.Get<IGame>().GetPlayers(_who))
            {
                player.ChooseAndDiscard(player.Deck.Hand.TotalCardsAvailable - _amount);
            }
        }
    }
}