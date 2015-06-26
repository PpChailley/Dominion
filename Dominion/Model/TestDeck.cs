using gbd.Dominion.Model.Zones;
using Ninject;

namespace gbd.Dominion.Model
{
    public class TestDeck : AbstractDeck, IDeck
    {

        [Inject]
        public TestDeck(ILibrary lib)
            : base(lib) { }


    }
}