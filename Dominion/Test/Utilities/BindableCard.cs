using gbd.Dominion.Contents;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;

namespace gbd.Dominion.Test.Utilities
{
    /// <summary>
    /// A test card that lets itself be bound and injected like any other, and unlike EmptyCard
    /// </summary>
    public class BindableCard: Card, ICard
    {
        public BindableCard(ICardMechanics mechanics, GameExtension ext, Include inc)
            : base(mechanics, ext, inc) { }

        public ActionContinue Continue { get { return ActionContinue.Terminal; } }

        public override ICardMechanics Mechanics { get; protected set; }

        public class A : BindableCard, ICard
        {
            public A(ICardMechanics mechanics, GameExtension ext, Include inc) 
                : base(mechanics, ext, inc) { }
        }

        public class B : BindableCard, ICard
        {
            public B(ICardMechanics mechanics, GameExtension ext, Include inc)
                : base(mechanics, ext, inc) { }
        }

        public class C : BindableCard, ICard
        {
            public C(ICardMechanics mechanics, GameExtension ext, Include inc)
                : base(mechanics, ext, inc) { }
        }

    }
}
