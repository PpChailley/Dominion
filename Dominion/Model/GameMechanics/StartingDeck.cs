using gbd.Dominion.Model.Zones;
using Ninject;

namespace gbd.Dominion.Model.GameMechanics
{
    public class StartingDeck : AbstractDeck, IDeck
    {

        [Inject]
        public StartingDeck(ILibrary lib)
            : base(lib) { }
    }
}