using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.GameMechanics.Actions;
using gbd.Dominion.Model.Zones;
using gbd.Dominion.Test.Utilities;
using gbd.Dominion.Tools;
using gbd.Tools.NInject;
using Ninject;
using Ninject.Activation;
using Ninject.Modules;
using Ninject.Syntax;

namespace gbd.Dominion.Contents.Cards
{
    public class IoCCardsModule : NinjectModule
    {
        public override void Load()
        {
            BindAlwaysAvailableCards();
            BindCardsFromBaseGame();
        }

        private void BindAlwaysAvailableCards()
        {
            SetBaseData<Copper>(0, 1, 0);
            SetBaseData<Silver>(3, 2, 0);
            SetBaseData<Gold>(6, 3, 0);
            SetBaseData<Estate>(2, 0, 1);
            SetBaseData<Duchy>(5, 0, 3);
            SetBaseData<Province>(8, 0, 6);


            Kernel.Bind<ICardType>().ToConstructor(x => new CurseType(-1)).WhenAnyAncestorOfType<CurseType, Curse>();
            Kernel.Bind<Resources>().ToConstructor(x => new Resources(0)).WhenAnyAncestorOfType<Resources, Curse>();
        }

        private void BindCardsFromBaseGame()
        {
            // TODO: comment back in cards and implement them
            //SetBaseData<Cellar>(2, 0, 0);
            //SetBaseData<Chapel>(2, 0, 0);
            //SetBaseData<Moat>(2, 0, 0);

            //SetBaseData<Chancellor>(3, 0, 0);
            SetBaseData<Village>(3, 0, 0).AddActions(new Draw(1), new AddAction(2));
            SetBaseData<Woodcutter>(3, 0, 0).AddActions(new AddBuy(1), new AddCoins(2));
            //SetBaseData<Workshop>(3, 0, 0);

            //SetBaseData<Bureaucrat>(4, 0, 0);
            //SetBaseData<Feast>(4, 0, 0);
            SetBaseData<Gardens>(4, 0, 0).AddVariableVictory(deck => deck.Cards.Count/10);
            //SetBaseData<Militia>(4, 0, 0).AddActions(new AddCoins(2), new DiscardDownTo(PlayerChoice.Opponents, 3));
            //SetBaseData<Moneylender>(4, 0, 0);
            //SetBaseData<Remodel>(4, 0, 0).AddActions(new TrashAndUpgrade(ZoneChoice.Hand, 1, 2));
            SetBaseData<Smithy>(4, 0, 0).AddActions(new Draw(3));
            //SetBaseData<Spy>(4, 0, 0);
            //SetBaseData<Thief>(4, 0, 0);
            //SetBaseData<ThroneRoom>(4, 0, 0);

            SetBaseData<CouncilRoom>(5, 0, 0).AddActions(new Draw(4), new AddBuy(1), new Draw(1, PlayerChoice.Opponents));
            SetBaseData<Festival>(5, 0, 0).AddActions(new AddAction(2), new AddBuy(1), new AddCoins(2));
            SetBaseData<Laboratory>(5, 0, 0).AddActions(new Draw(2), new AddAction(1));
            //SetBaseData<Cards.Library>(5, 0, 0);
            SetBaseData<Market>(5, 0, 0).AddActions(new Draw(1), new AddAction(1), new AddBuy(1), new AddCoins(1));
            //SetBaseData<Mine>(5, 0, 0);
            SetBaseData<Witch>(5, 0, 0).AddActions(new Draw(2), new ReceiveCurse(PlayerChoice.Opponents, 1));


            //SetBaseData<Adventurer>(6, 0, 0);
        }



  



        private MoreBindingSyntax<T> SetBaseData<T>(int coinsCost, int coinValue, int victory) where T : ICard
        {
            Kernel.Bind<Resources>().ToConstructor(x => new Resources(coinsCost)).WhenAnyAncestorOfType<Resources, T>();


            if (coinValue > 0)
                Kernel.Bind<ICardType>()
                    .ToConstructor(x => new TreasureType(coinValue))
                    .WhenAnyAncestorOfType<TreasureType, T>();

            if (victory > 0)
                //Kernel.Bind<ICardType>().ToConstructor(x => new VictoryType(victory)).WhenAnyAncestorOfType<VictoryType, T>();
                Kernel.Bind<ICardType>()
                    .ToConstructor(x => new VictoryType(victory))
                    .WhenAnyAncestorOfType<VictoryType, T>();
            //.WhenAnyAncestorMatches(r => 
            //    r.Request.Target.Member.DeclaringType.IsAssignableFrom(typeof(T))
            //    );

            return new MoreBindingSyntax<T>(this);
        }


        private class MoreBindingSyntax<T> where T: ICard
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
                _module.Bind<ICardType>().ToConstructor(syntax => new VictoryType(computeVictory));
            }
        }

    }

    public class TrashAndUpgrade : GameAction
    {
        private readonly ZoneChoice _from;
        private readonly int _numberOfCards;
        private readonly int _upgradeValue;

        public TrashAndUpgrade(ZoneChoice from, int numberOfCards, int upgradeValue)
        {
            _from = @from;
            _numberOfCards = numberOfCards;
            _upgradeValue = upgradeValue;
            throw new NotImplementedException();
        }

        public override void Do()
        {
            var player = IoC.Kernel.Get<IGame>().CurrentPlayer;
            ICard[] trashed = player.ChooseAndTrash(_from, _numberOfCards);

            foreach (var card in trashed)
            {
                player.ChooseAndReceive(
                    new Resources(card.Mechanics.Cost.Money + _upgradeValue,
                                  card.Mechanics.Cost.Potions  ));
            }



            

        }
    }
}
