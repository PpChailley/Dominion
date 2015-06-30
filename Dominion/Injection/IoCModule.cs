using System;
using gbd.Dominion.Contents;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.GameMechanics.Actions;
using gbd.Dominion.Model.Zones;
using gbd.Tools.NInject;
using Ninject.Modules;

namespace gbd.Dominion.Injection
{
    abstract class IoCModule: NinjectModule
    {
        private GameExtension _currentExtension;
        private Include _defaultInclude;


        protected void SetDefaultInclude(Include include)
        {
            _defaultInclude = include;
        }

        protected void SetCurrentExtension(GameExtension gameExtension)
        {
            _currentExtension = gameExtension;
        }


        protected MoreBindingSyntax<T> SetBaseData<T>(int cost, int value, int victory) where T : ICard
        {
            return SetBaseData<T>(cost, value, victory, _currentExtension, _defaultInclude);
        }

        protected MoreBindingSyntax<T> SetBaseData<T>(int coinsCost,
                                                        int coinValue,
                                                        int victory,
                                                        GameExtension ext,
                                                        Include include) where T : ICard
        {
            Kernel.Bind<Resources>()
                .ToConstructor(x => new Resources(coinsCost))
                .WhenAnyAncestorOfType<Resources, T>();

            if (coinValue > 0)
                Kernel.Bind<ICardType>()
                    .ToConstructor(x => new TreasureType(coinValue))
                    .WhenAnyAncestorOfType<TreasureType, T>();

            if (victory > 0)
                Kernel.Bind<ICardType>()
                    .ToConstructor(x => new VictoryType(victory))
                    .WhenAnyAncestorOfType<VictoryType, T>();

            Kernel.Bind<GameExtension>().ToConstant(ext)
                .WhenAnyAncestorOfType<GameExtension, T>();

            Kernel.Bind<Include>().ToConstant(include)
                .WhenAnyAncestorOfType<Include, T>();

            return new MoreBindingSyntax<T>(this);
        }


        protected class MoreBindingSyntax<T> where T : ICard
        {

            private readonly NinjectModule _module;

            public MoreBindingSyntax(NinjectModule module)
            {
                _module = module;
            }

            public MoreBindingSyntax<T> AddActions(params GameAction[] actions)
            {
                foreach (var action in actions)
                {
                    _module.Bind<GameAction>().ToConstant(action).WhenInjectedInto<T>();
                }

                return this;
            }

            public void AddVariableVictory(Func<IZone, int> computeVictory)
            {
                _module.Bind<ICardType>()
                    .ToConstructor(syntax => new VictoryType(computeVictory))
                    .WhenAnyAncestorOfType<VictoryType, T>();
            }
        }



    }
}
