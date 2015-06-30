using System;
using System.Collections.Generic;
using gbd.Dominion.Contents;
using gbd.Dominion.Contents.Cards;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.GameMechanics.Actions;
using gbd.Dominion.Model.Zones;
using gbd.Tools.NInject;
using Ninject.Syntax;
using NUnit.Framework;

namespace gbd.Dominion.Injection
{
    class IoCCardsModule : IoCModule
    {


        public override void Load()
        {
            BindAlwaysAvailableCards();
            BindCardsFromBaseGame();
        }



        private void BindAlwaysAvailableCards()
        {
            SetCurrentExtension(GameExtension.AlwaysPresent);
            SetDefaultInclude(Include.AlwaysIncluded);

            SetBaseData<Copper>(0, 1, 0);
            SetBaseData<Silver>(3, 2, 0);
            SetBaseData<Gold>(6, 3, 0);
            SetBaseData<Estate>(2, 0, 1);
            SetBaseData<Duchy>(5, 0, 3);
            SetBaseData<Province>(8, 0, 6);


            Kernel.Bind<ICardType>().ToConstructor(x => new CurseType(-1)).WhenAnyAncestorOfType<CurseType, Curse>();
            Kernel.Bind<Resources>().ToConstructor(x => new Resources(0)).WhenAnyAncestorOfType<Resources, Curse>();
            Kernel.Bind<GameExtension>().ToConstant(GameExtension.AlwaysPresent).WhenAnyAncestorOfType<GameExtension, Curse>();
            Kernel.Bind<Include>().ToConstant(Include.AlwaysIncluded).WhenAnyAncestorOfType<Include, Curse>();

        }

        private void BindCardsFromBaseGame()
        {

            SetCurrentExtension(GameExtension.BaseGame);
            SetDefaultInclude(Include.Selectable);


            // TODO: comment back in cards and implement them
            //SetBaseData<Cellar>(2, 0, 0);
            SetBaseData<Chapel>(2, 0, 0).AddActions(new ChooseAndTrash(4,4));
            //SetBaseData<Moat>(2, 0, 0);

            //SetBaseData<Chancellor>(3, 0, 0);
            SetBaseData<Village>(3, 0, 0).AddActions(new Draw(1), new AddAction(2));
            SetBaseData<Woodcutter>(3, 0, 0).AddActions(new AddBuy(1), new AddCoins(2));
            ////SetBaseData<Workshop>(3, 0, 0);

            ////SetBaseData<Bureaucrat>(4, 0, 0);
            SetBaseData<Feast>(4, 0, 0).AddActions(new TrashThis(), new GainCard(5, 5));
            SetBaseData<Gardens>(4, 0, 0).AddVariableVictory(deck => deck.Cards.Count / 10);
            SetBaseData<Militia>(4, 0, 0).AddActions(new AddCoins(2), new DiscardDownTo(PlayerChoice.Opponents, 3));
            SetBaseData<Moneylender>(4, 0, 0).AddActions(new Trash<Copper>(ZoneChoice.Hand, 1, 1), new AddCoins(3));
            SetBaseData<Remodel>(4, 0, 0).AddActions(new TrashAndUpgrade(ZoneChoice.Hand, 1, 2));
            SetBaseData<Smithy>(4, 0, 0).AddActions(new Draw(3));
            ////SetBaseData<Spy>(4, 0, 0);
            ////SetBaseData<Thief>(4, 0, 0);
            ////SetBaseData<ThroneRoom>(4, 0, 0);

            SetBaseData<CouncilRoom>(5, 0, 0).AddActions(new Draw(4), new AddBuy(1), new Draw(1, PlayerChoice.Opponents));
            SetBaseData<Festival>(5, 0, 0).AddActions(new AddAction(2), new AddBuy(1), new AddCoins(2));
            SetBaseData<Laboratory>(5, 0, 0).AddActions(new Draw(2), new AddAction(1));
            ////SetBaseData<Cards.Library>(5, 0, 0);
            SetBaseData<Market>(5, 0, 0).AddActions(new Draw(1), new AddAction(1), new AddBuy(1), new AddCoins(1));
            ////SetBaseData<Mine>(5, 0, 0);
            SetBaseData<Witch>(5, 0, 0).AddActions(new Draw(2), new ReceiveCurse(PlayerChoice.Opponents, 1));


            //SetBaseData<Adventurer>(6, 0, 0);
        }

 

    }
}
