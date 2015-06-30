using gbd.Dominion.Contents;

namespace gbd.Dominion.Model.Cards
{
    /// <summary>
    /// A card that appears in the supply if and only if another card is selected.
    /// Like Madman, that appears only if Hermit is selected
    /// </summary>
    public abstract class ConditionalCard: Card
    {
        protected ConditionalCard(ICardMechanics mechanics, GameExtension ext, Include inc) 
            : base(mechanics, ext, inc) { }
    }
}
