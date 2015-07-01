using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.GameMechanics.Actions;
using Ninject;

namespace gbd.Dominion.Injection
{
    internal class GainCard : GameAction, IGameAction
    {
        private readonly Resources _minCost;
        private readonly Resources _maxCost;


        public GainCard(Resources minCost, Resources maxCost)
        {
            _minCost = minCost;
            _maxCost = maxCost;
        }

        public GainCard(int minCost, int maxCost)
            : this(new Resources(minCost), new Resources(maxCost))  { }


        public override void Do()
        {
            IoC.Kernel.Get<IGame>()
                .CurrentPlayer
                .I.Receive(_minCost, _maxCost);
        }
    }
}