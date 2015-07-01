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
        private readonly Resources _upgradeValue;

        public TrashAndUpgrade(ZoneChoice from, int numberOfCards, int upgradeValue)
        {
            _from = @from;
            _numberOfCards = numberOfCards;
            _upgradeValue = new Resources(upgradeValue);
        }

        public override void Do()
        {
            var player = IoC.Kernel.Get<IGame>().CurrentPlayer;
            var trashed = player.I.Trash<ICard>(_from, _numberOfCards);

            foreach (var card in trashed)
            {
                player.I.Receive(    Resources.Zero, 
                                            card.Mechanics.Cost.Plus(_upgradeValue));
            }



            

        }
    }
}