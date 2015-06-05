using System;
using System.Collections;
using System.Collections.Generic;

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