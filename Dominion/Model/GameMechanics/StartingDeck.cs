using gbd.Dominion.Model.Zones;
using Ninject;

namespace gbd.Dominion.Model.GameMechanics
{
    public class StartingDeck : AbstractDeck, IDeck
    {
        public StartingDeck(ILibrary lib, IDiscardPile discard, IBattleField field, IHand hand) : 
            base(lib, discard, field, hand) { }
    }
}