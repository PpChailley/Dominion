using gbd.Dominion.Model.Zones;
using Ninject;

namespace gbd.Dominion.Model
{
    public class TestDeck : AbstractDeck, IDeck
    {
        public TestDeck(ILibrary lib, IDiscardPile discard, IBattleField field, IHand hand) : 
            base(lib, discard, field, hand) { }
    }
}