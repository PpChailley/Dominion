using System;
using System.Collections.Generic;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.Zones;

namespace gbd.Dominion.Model.GameMechanics.AI
{
    public abstract class AbstractIntelligence
    {
        public IPlayer Player { get; protected set; }


        public void Ready(IPlayer player)
        {
            Player = player;
        }

        public IEnumerable<ICard> Trash<T>(ZoneChoice zoneFrom, int minAmount, int? maxAmount = null)
        {
            return Trash(typeof(T), zoneFrom, minAmount, maxAmount);
        }

        public abstract IEnumerable<ICard> Trash(Type zoneFrom, ZoneChoice minAmount, int maxAmount, int? i);
    }
}