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
        Resources AvailableResources { get; set; }


        void Receive(ICard card, ZoneChoice to = ZoneChoice.Discard, Position where = Position.Top);
        void Ready();
        void Draw(int amount);
        void ChooseAndDiscard(int amount);
        void Play(ICard card);

        void ReceiveFrom(ISupplyPile from, int amount, ZoneChoice to = ZoneChoice.Discard, Position where = Position.Top);
        void ChooseAndReceive(Resources minCost, Resources maxCost);
        ICard[] ChooseAndTrash(ZoneChoice @from, int minAmount, int? maxAmount = null);
        void StartTurn();
        void EndTurn();
    }
}