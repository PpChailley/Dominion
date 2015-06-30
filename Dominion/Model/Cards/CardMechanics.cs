using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.GameMechanics.Actions;
using gbd.Tools.Clr;
using Ninject;

namespace gbd.Dominion.Model.Cards
{
    public class CardMechanics : ICardMechanics
    {
        [Inject]
        public CardMechanics(   Resources cost,
                                IList<ICardType> types, 
                                IList<IGameAction> onBuy, 
                                IList<IGameAction> onPlay
                                )
        {
            Cost = cost;
            OnBuyTrigger = onBuy;
            OnPlayTriggers = onPlay;
            Types = types;
        }


        public Resources Cost { get; private set; }

        public IList<IGameAction> OnBuyTrigger { get; private set; }
        
        public IList<IGameAction> OnPlayTriggers { get; private set; }
        
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
                int score = 0;

                var victoryType = GetCardType<VictoryType>();
                if (victoryType != null)
                    score += victoryType.VictoryPoints;

                var curseType = GetCardType<CurseType>();
                if (curseType != null)
                    score += curseType.CurseValue;

                return score;
            }
        }

        public string PrintedText
        {
            get
            {
                throw new NotImplementedException();
            }
        }


        public TCardType GetCardType<TCardType>() where TCardType : ICardType
        {
            var matchingTypes = Types
                .Where(t => t.GetType() == typeof (TCardType))
                .Cast<TCardType>()
                .SingleOrDefault();

            return matchingTypes;
        }


        public override string ToString()
        {
            var toreturn = new StringBuilder();

            toreturn.Append("{{ {0} V - {1} - ".Format(VictoryPoints, TreasureValue));

            if (OnPlayTriggers.Any())
            {
                toreturn.Append("{{ PLAY: ");
                foreach (var trigger in OnPlayTriggers)
                {
                    toreturn.Append(trigger);
                }
                toreturn.Append(" }}");
            }

            if (OnBuyTrigger.Any())
            {
                toreturn.Append("{{ BUY: ");
                foreach (var trigger in OnPlayTriggers)
                {
                    toreturn.Append(trigger);
                }
                toreturn.Append(" }}");
            }

            toreturn.Append(" }}");

            return toreturn.ToString();
        }
    }
}