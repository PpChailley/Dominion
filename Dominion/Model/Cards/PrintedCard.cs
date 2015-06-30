using System;
using gbd.Dominion.Contents;
using Ninject;

namespace gbd.Dominion.Model.Cards
{
    public abstract class PrintedCard
    {

        public abstract GameExtension Extension { get; protected set; }
        public abstract GameSet PresentInSet { get; }

        public CardMetadata Meta;

        


    }
}
