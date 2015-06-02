using System.Collections.Generic;
using org.gbd.Dominion.Model.Actions;

namespace org.gbd.Dominion.Model
{
    public class CardMechanics
    {
        public Resources Cost;
        public Resources BuyValue;

        public int VictoryPoints;
        public List<IGameAction> OnBuyTrigger = new List<IGameAction>();
        public List<IGameAction> OnPlayTrigger = new List<IGameAction>();

        public IList<CardType> Types = new List<CardType>();


    }

    public enum CardType
    {
        Victory,
        Treasure,
        Action,
        Attack,
        Reaction
    }
}