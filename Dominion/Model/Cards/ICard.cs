using System.Collections.Generic;
using gbd.Dominion.Contents;

namespace gbd.Dominion.Model.Cards

{
    public interface ICard
    {
        GameExtension Extension { get; }
        
        
        CardMechanics Mechanics { get; }
        IList<CardAttribute> Attributes { get; }

        void ClearInPlayAttributes();
    }
}
