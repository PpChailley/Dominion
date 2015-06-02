using System.Collections.Generic;

namespace org.gbd.Dominion.Model
{
    public class PlayZone : IPlayZone
    {
        private readonly IList<ICard> _cards = new List<ICard>();

        public IList<ICard> Cards
        {
            get { return _cards; }
        }
    }
}