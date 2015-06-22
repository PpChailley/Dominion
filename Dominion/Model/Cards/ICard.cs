using System;
using System.Collections.Generic;
using gbd.Dominion.Contents;

namespace gbd.Dominion.Model.Cards

{
    public interface ICard
    {
        GameExtension Extension { get; }


        ICardMechanics Mechanics { get; }

        IList<CardAttribute> Attributes { get; }

        String PrintedText { get; }

        void ClearInPlayAttributes();
    }
}
