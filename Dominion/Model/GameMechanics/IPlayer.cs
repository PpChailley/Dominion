using System.Collections.Generic;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.Zones;

namespace gbd.Dominion.Model.GameMechanics
{
    public interface IPlayer
    {
        void Buy(ICard card);

        PlayerStatus Status { get; set; }

        IDeck Deck { get; set; }


        int CurrentScore { get; }
        int AvailableActions { get; set; }
        int AvailableBuys { get; set; }
        int AvailableCoins { get; set; }


        void Receive(ICard card);
        void Ready();
        void Draw(int amount = 1);
        void DiscardFromHand(int amount);
        void AddToDeck(ICard card, CardsPile destination = CardsPile.Discard, Position position = Position.Top);
        void Buy(IList<ICard> cards);
        void Play(ICard card);

        void ReceiveFrom(ISupplyPile from, int amount);
        void ChooseAndReceive(Resources maxCost);
        ICard[] ChooseAndTrash(ZoneChoice @from, int numberOfCards);
    }
}