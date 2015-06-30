using gbd.Dominion.Contents;
using gbd.Dominion.Model.Cards;

namespace gbd.Dominion.Test.Utilities
{
    /// <summary>
    /// A test card that lets itself be bound and injected like any other, and unlike EmptyCard
    /// </summary>
    public class BindableCard: Card, ICard
    {
        public BindableCard(ICardMechanics mechanics, GameExtension ext, Include inc)
            : base(mechanics, ext, inc) { }

        public override ICardMechanics Mechanics { get; protected set; }


    }
}
