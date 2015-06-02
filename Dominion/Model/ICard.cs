using System;
using System.Collections.Generic;
using org.gbd.Dominion.Model.Cards;

namespace org.gbd.Dominion.Model

{
    public interface ICard
    {
        CardMechanics Mechanics { get; }
        IList<CardAttribute> Attributes { get; }
    }
}
