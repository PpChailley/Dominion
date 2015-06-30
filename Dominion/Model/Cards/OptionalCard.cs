using gbd.Dominion.Contents;

namespace gbd.Dominion.Model.Cards
{

    /// <summary>
    /// A card that can be included in the supply or not, as selected by the game creator. 
    /// Like Platinum or any Ruins
    /// </summary>
    public abstract class OptionalCard: Card
    {
        protected OptionalCard(ICardMechanics mechanics, GameExtension ext, Include inc) 
            : base(mechanics, ext, inc) { }
    }
}
