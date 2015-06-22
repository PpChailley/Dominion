using gbd.Dominion.Model.Zones;
using Ninject;

namespace gbd.Dominion.Model
{
    public class TestDeck : AbstractDeck, IDeck
    {
        public TestDeck(IDiscardPile discard, ILibrary lib, IBattleField bf, IHand hand) 
            : base(discard, lib, bf, hand)  {}

        [Inject]
        public TestDeck(ILibrary lib)
            : base(lib) { }


    }
}