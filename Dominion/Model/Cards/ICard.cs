using System.Collections.Generic;
using gbd.Dominion.Contents;
using gbd.Dominion.Model.Cards;

namespace gbd.Dominion.Model

{
    public interface ICard
    {
        GameExtension Extension { get; }
        
        
        CardMechanics Mechanics { get; }
        IList<CardAttribute> Attributes { get; }

        void ClearInPlayAttributes();
    }
}
