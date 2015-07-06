using gbd.Dominion.Model.Zones;

namespace gbd.Dominion.Model.Cards
{
    public class ActionType : ICardType
    {
        protected IZone Zone { get; set; }

        public void Ready(IZone zone)
        {
            // Nothing to do yet
        }

        // TODO: move on play triggers from Card to ActionType

    }
}