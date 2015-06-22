using System;
using System.Collections.Generic;
using System.Linq;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.GameMechanics.Actions;
using Ninject;

namespace gbd.Dominion.Model.Cards
{
    public class CardMechanics : ICardMechanics
    {
        [Inject]
        public CardMechanics(IList<ICardType> types, IList<IGameAction> onBuy, IList<IGameAction> onPlay)
        {
            OnBuyTrigger = onBuy;
            OnPlayTriggers = onPlay;
            Types = types;
        }


        [Inject]
        public Resources Cost { get; private set; }

        [Inject]
        public IList<IGameAction> OnBuyTrigger { get; private set; }

        [Inject]
        public IList<IGameAction> OnPlayTriggers { get; private set; }

        [Inject]
        public IList<ICardType> Types { get; private set; }


        public Resources TreasureValue
        {
            get
            {
                var treasureType = (TreasureType) GetCardType<TreasureType>();
                return treasureType == null ? new Resources(0) : treasureType.BuyValue;
            }
        }

        public int VictoryPoints
        {
            get
            {
                var victoryType = (VictoryType)GetCardType<VictoryType>();
                return victoryType == null ? 0 : victoryType.VictoryPoints;
            }
        }

        public string PrintedText
        {
            get
            {
                throw new NotImplementedException();
            }
        }


        public ICardType GetCardType<TCardType>() where TCardType:ICardType
        {
            var matchingTypes = Types.Where(t => t.GetType() == typeof (TCardType)).Cast<TCardType>().SingleOrDefault();

            return matchingTypes;
        }

        
    }
}