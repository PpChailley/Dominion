using System.Collections.Generic;
using System.Linq;
using org.gbd.Dominion.Model.Actions;

namespace org.gbd.Dominion.Model
{
    public class Library :  ILibrary
    {

        private readonly Queue<ICard> _cards;

        public Library(IEnumerable<ICard> cards)
        {
            _cards = new Queue<ICard>();

            foreach (var card in cards)
            {
                _cards.Enqueue(card);
            }
        }

        public IEnumerable<ICard> Cards
        {
            get { return _cards; }
        }

        public IEnumerable<ICard> Dequeue(int amount = 1)
        {
            var toreturn = new List<ICard>();

            for (var i = 0; i < 10; i++)
            {
                toreturn.Add(_cards.Dequeue());
            }

            return toreturn;
        }
    }
}