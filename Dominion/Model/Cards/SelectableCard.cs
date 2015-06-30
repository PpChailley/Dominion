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
        protected SelectableCard(ICardMechanics mechanics) : base(mechanics) { }


        public override GameSet PresentInSet
        {
            get { return GameSet.Selectable; }
        }

        public override GameExtension Extension { get; protected set; }


        // TODO: force injection of extension
        // protected SelectableCard(CardMechanics mechanics, GameExtension extension) : base(mechanics)
        // see aborted branch X_InjectEnumValues: a595f5396fd137bdb32c5b1ef9548ccc56f3368b
        [Inject]
        protected SelectableCard(CardMechanics mechanics, GameExtension extension) : base(mechanics)
        {
            Extension = extension;
        }

    }
}
