using gbd.Dominion.Contents;
using Ninject;

namespace gbd.Dominion.Model.Cards
{
    /// <summary>
    /// A standard card that can be randomly included in the supply by the game creation process 
    /// and count towards the 10 piles limit.
    /// Like most cards : Village, Hermit, Lighthouse, ...
    /// </summary>
    public abstract class SelectableCard: Card
    {

        public override GameSet PresentInSet
        {
            get { return GameSet.Selectable; }
        }



    }
}
