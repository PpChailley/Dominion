using gbd.Dominion.Model.Zones;

namespace gbd.Dominion.Model.GameMechanics
{
    public class StartingDeck : AbstractDeck, IDeck
    {
        public StartingDeck(ILibrary lib, IDiscardPile discard, IBattleField field, IHand hand) : 
            base(lib, discard, field, hand) { }
    }
}