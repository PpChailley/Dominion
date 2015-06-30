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

        void EndOfTurnCleanup();
        ILibrary ShuffleDiscardToLibrary();

        IZone Get(ZoneChoice zone);
    }
}