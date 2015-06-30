using gbd.Dominion.Injection;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.GameMechanics.Actions;
using gbd.Dominion.Model.Zones;
using Ninject;

namespace gbd.Dominion.Contents.Cards
{
    internal class ChooseAndTrash : GameAction
    {
        private readonly int _minAmount;
        private readonly int _maxAmount;


        public ChooseAndTrash(int minAmount, int maxAmount)
        {
            _minAmount = minAmount;
            _maxAmount = maxAmount;
        }

        public override void Do()
        {
            IoC.Kernel.Get<IGame>().CurrentPlayer.ChooseAndTrash(ZoneChoice.Hand, _minAmount, _maxAmount);

        }
    }
}