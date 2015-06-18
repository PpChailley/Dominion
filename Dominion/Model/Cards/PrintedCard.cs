using System;
using gbd.Dominion.Contents;

namespace gbd.Dominion.Model.Cards
{
    public abstract class PrintedCard
    {
        public String Text;

        // TODO: Change type to more precise 
        public Object Illustration;

        public abstract GameExtension Extension { get; }
        public abstract GameSet PresentInSet { get; }

        public CardMetadata Meta;



    }
}
