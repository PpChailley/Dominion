using System;
using System.Collections.Generic;
using System.Linq;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.GameMechanics.Actions;

namespace gbd.Dominion.Model.Cards
{
    public class CardMechanics
    {
        public Resources Cost;

        public Resources TreasureValue
        {
            get
            {
                var treasureType = (TreasureType) GetCardType<TreasureType>();
                if (treasureType == null)   return new Resources(0);
                else                        return treasureType.BuyValue;
            }
        }

        public int VictoryPoints
        {
            get
            {
                var victoryType = (VictoryType)GetCardType<VictoryType>();
                if (victoryType == null) return 0;
                else return victoryType.VictoryPoints;
            }
        }


        private ICardType GetCardType<TCardType>() where TCardType:ICardType
        {
            var matchingTypes = Types.Where(t => t.GetType() == typeof (TCardType)).Cast<TCardType>();
            if (matchingTypes.Any() == false)
            {
                return null;
            }
            if (matchingTypes.Count() == 1)
            {
                return matchingTypes.Single();
            }
            else
            {
                throw new InvalidOperationException("Card has more than once the same card type " + typeof(TCardType).Name);
            }            
        }

        
        public List<IGameAction> OnBuyTrigger = new List<IGameAction>();
        public List<IGameAction> OnPlayTrigger = new List<IGameAction>();

        public IList<ICardType> Types = new List<ICardType>();


    }
}