using System;
using System.Collections.Generic;
using org.gbd.Dominion.Model.Zones;

namespace org.gbd.Dominion.Model
{
    public interface ILibrary: IZone
    {
        [Obsolete] IEnumerable<ICard> GetFromTop(int amount);
        void Add(ICard card, Position position);
        void Init(IDeck deck);
        void ShuffleDiscardToLibrary();
    }
}