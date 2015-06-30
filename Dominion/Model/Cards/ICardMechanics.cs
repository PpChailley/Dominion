using System.Collections.Generic;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.GameMechanics.Actions;

namespace gbd.Dominion.Model.Cards
{
    public interface ICardMechanics
    {
        Resources Cost { get; }

        IList<IGameAction> OnBuyTrigger { get; }
        IList<IGameAction> OnPlayTriggers { get; }
        IList<ICardType> Types { get; }

        Resources TreasureValue { get; }
        int VictoryPoints { get; }
        
        string PrintedText { get; }



        TCardType GetCardType<TCardType>() where TCardType : ICardType;
    }
}