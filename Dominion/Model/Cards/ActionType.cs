using gbd.Dominion.Model.Zones;

namespace gbd.Dominion.Model.Cards
{
    public abstract class ActionType : ICardType
    {
        protected IZone Zone { get; set; }

        public abstract void Ready(IZone zone);
    }
}