using System.Collections.Generic;
using gbd.Dominion.Injection;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.Zones;
using Ninject;

namespace gbd.Dominion.Model.GameMechanics.AI
{
    public class ModularAi: IAi
    {

        private readonly IDiscardDelegate _discarder;
        private readonly IReceiveDelegate _receiver;
        private readonly ITrashDelegate _trasher;
        private readonly IPlayDelegate _playDelegate;
        private readonly IBuyDelegate _buyer;

        public IPlayer Player { get; private set; }


        [Inject]
        public ModularAi(       IDiscardDelegate discarder, 
                                IReceiveDelegate receiver, 
                                ITrashDelegate trasher, 
                                IPlayDelegate playDelegate, 
                                IBuyDelegate buyer)
        {
            _buyer = (IBuyDelegate) buyer.Init(this);
            _discarder = (IDiscardDelegate) discarder.Init(this);
            _receiver = (IReceiveDelegate) receiver.Init(this);
            _trasher = (ITrashDelegate) trasher.Init(this);
            _playDelegate = (IPlayDelegate) playDelegate.Init(this);
        }

        public void Ready(IPlayer player)
        {
            Player = player;
        }


        public IList<ICard> Discard(int minAmount, int? maxAmount = null)
        {
            var todiscard = _discarder.Choose(minAmount, maxAmount);
            todiscard.MoveTo(Player.Deck.DiscardPile);
            return todiscard;
        }

        public IList<ICard> Receive(Resources minCost, Resources maxCost = null)
        {
            var toReceive = _receiver.Choose(minCost, maxCost);
            // TODO: Intelligences should not do the cards moving. 
            // Refactor and test that
            toReceive.MoveTo(Player.Deck.DiscardPile);
            return toReceive;
        }

        public IList<ICard> Trash<T>(ZoneChoice fromZone, int minAmount, int? maxAmount = null)
        {
            var toTrash = _trasher.Choose(fromZone, minAmount, maxAmount);
            toTrash.MoveTo(IoC.Kernel.Get<IGame>().Trash);
            return toTrash;
        }

        public void PlayTurn()
        {
            _playDelegate.PlayTurn();
        }
    }
}
