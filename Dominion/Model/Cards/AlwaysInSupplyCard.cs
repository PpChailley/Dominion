using gbd.Dominion.Contents;

namespace gbd.Dominion.Model.Cards
{
    /// <summary>
    /// A card that is always in the supply, whatever the game. Like copper, curse, province...
    /// </summary>
    public abstract class AlwaysInSupplyCard: Card
    {
        protected AlwaysInSupplyCard(ICardMechanics mechanics, GameExtension ext, Include inc) 
            : base(mechanics, ext, inc) { }
    }
}
