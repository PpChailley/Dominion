using System;
using gbd.Dominion.Contents;

namespace gbd.Dominion.Model.Cards
{
    /// <summary>
    /// A card that is always in the supply, whatever the game. Like copper, curse, province...
    /// </summary>
    public abstract class AlwaysInSupplyCard: Card
    {
        public override GameSet PresentInSet
        {
            get { return GameSet.AlwaysIncluded; }
        }

        public override GameExtension Extension
        {
            get { return GameExtension.AlwaysPresent; }
            protected set { throw new InvalidOperationException();}
        }
    }
}
