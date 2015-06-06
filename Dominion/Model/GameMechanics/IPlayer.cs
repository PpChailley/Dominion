using System.Collections.Generic;
using org.gbd.Dominion.Model.Zones;

namespace org.gbd.Dominion.Model.GameMechanics
{
    public interface IPlayer
    {
        void Buy(ICard card);

        PlayerStatus Status { get; set; }

        IDeck Deck { get; set; }
        IHand Hand { get; }
        IDiscardPile DiscardPile { get; }
        ILibrary Library { get; }

        int CurrentScore { get; }


        void Receive(ICard card);
        void GetReadyToStartGame();
        void Draw(int amount = 1);
        void DiscardFromHand(int amount);
        void AddToDeck(ICard card, CardsPile destination = CardsPile.Discard, Position position = Position.Top);
        void Buy(IList<ICard> cards);
    }
}