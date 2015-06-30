using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.GameMechanics.Actions;
using gbd.Dominion.Model.Zones;
using gbd.Dominion.Tools;
using Ninject;

namespace gbd.Dominion.Contents.Cards
{
    // TODO: test this
    internal class ReceiveCurse : GameAction
    {
        private readonly PlayerChoice _who;
        private readonly int _amount;


        public ReceiveCurse(PlayerChoice who, int amount)
        {
            _amount = amount;
            _who = who;
        }


        public override void Do()
        {
            foreach (var player in IoC.Kernel.Get<IGame>().GetPlayers(_who))
            {
                player.ReceiveFrom(IoC.Kernel.Get<ISupplyZone>().PileOf<Curse>(), _amount);
            }
            
            
        }
    }
}