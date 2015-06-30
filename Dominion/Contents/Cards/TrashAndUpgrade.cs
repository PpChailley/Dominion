using System;
using gbd.Dominion.Injection;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.GameMechanics.Actions;
using gbd.Dominion.Model.Zones;
using Ninject;

namespace gbd.Dominion.Contents.Cards
{
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