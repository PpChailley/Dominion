using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.GameMechanics.Actions;
using gbd.Dominion.Model.Zones;
using Ninject;

namespace gbd.Dominion.Injection
{
    internal class Trash<T> : GameAction, IGameAction
        where T: ICard
    {
        private readonly ZoneChoice _from;
        private readonly int _minAmount;
        private readonly int _maxAmount;

        public Trash(ZoneChoice from, int minAmount, int maxAmount)
        {
            _from = from;
            _minAmount = minAmount;
            _maxAmount = maxAmount;
        }


        public override void Do()
        {
            var player = IoC.Kernel.Get<IGame>().CurrentPlayer;
            player.ChooseAndTrash<T>(_from, _minAmount, _maxAmount);
        }
    }
}