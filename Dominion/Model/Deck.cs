using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using org.gbd.Dominion.Model.Actions;

namespace org.gbd.Dominion.Model
{
    public class Deck: Queue<ICard>
    {
        public ICollection<ICard> Cards = new List<ICard>();

        public ICollection<ICard> Dequeue(int n)
        {
            return Cards.Select(card => this.Dequeue()).ToList();
        }
    }
}
