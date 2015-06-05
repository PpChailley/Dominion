using System.Collections.Generic;
using org.gbd.Dominion.Contents;
using org.gbd.Dominion.Model.Cards;

namespace org.gbd.Dominion.Model

{
    public interface ICard
    {
        GameExtension Extension { get; }
        
        
        CardMechanics Mechanics { get; }
        IList<CardAttribute> Attributes { get; }

        void ClearInPlayAttributes();
    }
}
