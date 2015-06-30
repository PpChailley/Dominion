using System;
using gbd.Dominion.Contents;
using gbd.Dominion.Model.Cards;

namespace gbd.Dominion.Test.Utilities
{
    /// <summary>
    /// A test card that lets itself be bound and injected like any other, and unlike EmptyCard
    /// </summary>
    public class BindableCard: Card, ICard
    {
        public BindableCard(ICardMechanics mechanics) : base(mechanics) { }

        public override GameExtension Extension
        {
            get { return GameExtension.TestCards; }
            protected set { throw new InvalidOperationException();}
        }

        public override ICardMechanics Mechanics { get; protected set; }

        public override GameSet PresentInSet
        {
            get { return GameSet.TestCards;}
        }
    }
}
