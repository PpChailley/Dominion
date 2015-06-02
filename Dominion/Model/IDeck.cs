using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using org.gbd.Dominion.Model.Actions;

namespace org.gbd.Dominion.Model
{
    public interface IDeck
    {
        IList<ICard> Cards { get; }
        IHand Hand { get; }
        IDiscardPile DiscardPile { get; }
        ILibrary Library { get; }

        int CurrentScore { get; }

        void Add(ICard card, CardsPile destination);
        void Add(ICard card, CardsPile destination, PositionInCardsCollection positionInCardsCollection);

        void EndOfTurnCleanup();
        ILibrary ShuffleDiscardToLibrary();
        void GetReadyToStartGame();
    }
}