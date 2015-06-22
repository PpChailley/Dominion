using gbd.Dominion.Model.Zones;
using Ninject;

namespace gbd.Dominion.Model.GameMechanics
{
    public class StartingDeck : AbstractDeck, IDeck
    {
        public StartingDeck(IDiscardPile discard, ILibrary lib, IBattleField bf, IHand hand) 
            : base(discard, lib, bf, hand) {}

        [Inject]
        public StartingDeck(ILibrary lib)
            : base(lib) { }
    }
}