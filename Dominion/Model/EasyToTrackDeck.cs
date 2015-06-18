using System.Collections.Generic;
using gbd.Dominion.Model.Zones;
using gbd.Dominion.Test.Utilities;
using Ninject;

namespace gbd.Dominion.Model
{
    public class EasyToTrackDeck : AbstractDeck, IDeck
    {
        public EasyToTrackDeck(IDiscardPile discard, ILibrary lib, IBattleField bf, IHand hand) 
            : base(discard, lib, bf, hand)  {}

        [Inject]
        public EasyToTrackDeck(ILibrary lib)
            : base(lib) { }


    }
}