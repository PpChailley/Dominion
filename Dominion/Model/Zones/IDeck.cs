using System.Collections.Generic;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;

namespace gbd.Dominion.Model.Zones
{
    public interface IDeck: IZone
    {
        IHand Hand { get; }
        IDiscardPile DiscardPile { get; }
        ILibrary Library { get;  }
        IBattleField BattleField { get; }

        int CurrentScore { get; }
        CardRepartition CardCountByZone { get; }

        void Add(ICard card, CardsPile destination);
        void Add(ICard card, CardsPile destination, Position position);

        void EndOfTurnCleanup();
        ILibrary ShuffleDiscardToLibrary();
        void Ready();
    }
}