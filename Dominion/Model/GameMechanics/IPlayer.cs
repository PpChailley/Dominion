using System.Collections.Generic;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.Zones;

namespace gbd.Dominion.Model.GameMechanics
{
    public interface IPlayer
    {

        PlayerStatus Status { get; set; }

        IDeck Deck { get; set; }

        IIntelligence I { get; }



        void Ready();
        void StartTurn();
        void EndTurn();


        void Draw(int amount);
        void PlayAction(ICard card);
        void Receive(ICard card, ZoneChoice to = ZoneChoice.Discard, Position where = Position.Top);
        void ReceiveFrom(ISupplyPile from, int amount, ZoneChoice to = ZoneChoice.Discard, Position where = Position.Top);
        void Buy(List<ICard> card);


        void PlayTreasure(ICard card);
    }
}