using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using org.gbd.Dominion.Model.Actions;

namespace org.gbd.Dominion.Model
{
    public interface IDeck: ICollection<Card>
    {
        void Shuffle();

        ICollection<ICard> Dequeue(int n);

    }
}