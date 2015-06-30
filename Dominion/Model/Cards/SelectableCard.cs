using System;
using gbd.Dominion.Contents;
using Ninject;

namespace gbd.Dominion.Model.Cards
{
    /// <summary>
    /// A standard card that can be randomly included in the supply by the game creation process 
    /// and counts towards the 10 piles limit.
    /// Like most cards : Village, Hermit, Lighthouse, ...
    /// </summary>
    public abstract class SelectableCard: Card
    {

        protected SelectableCard(ICardMechanics mechanics, GameExtension ext, Include inc) 
            : base(mechanics, ext, inc) { }
    }
}
