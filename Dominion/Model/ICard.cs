using System;
using System.Collections.Generic;

namespace org.gbd.Dominion.Model

{
    public interface ICard
    {
        CardMechanics Mechanics { get; }
        IList<CardAttribute> Attributes { get; }
    }
}
