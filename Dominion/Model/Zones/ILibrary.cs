using System;
using System.Collections.Generic;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;

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