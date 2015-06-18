using System;
using System.Collections.Generic;

namespace gbd.Dominion.Model.Zones
{
    public interface ILibrary: IZone
    {
        [Obsolete] IEnumerable<ICard> GetFromTop(int amount);
        void Add(ICard card, Position position);
        void Init(IDeck deck);
        void ShuffleDiscardToLibrary();
    }
}