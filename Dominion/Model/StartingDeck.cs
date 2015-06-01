using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using System.Threading;
using org.gbd.Dominion.Model.Actions;

namespace org.gbd.Dominion.Model
{
    public class StartingDeck : Deck, IDeck
    {
        public StartingDeck()
        {
            // TODO/ Ultimately this should be sent through NInject (and this class obsoleted?)

            for (var i = 0; i < 10; i++)
            {
                this.Enqueue(new NonameCard());
            }
        }


        public IEnumerator<Card> GetEnumerator()
        {
            return base.GetEnumerator();
        }
    }
}