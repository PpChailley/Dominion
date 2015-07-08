using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;

namespace gbd.Dominion.Contents.Cards
{
    public class Silver : Card, ICard
    {
      public Silver(ICardMechanics mechanics, GameExtension ext, Include inc)
            : base(mechanics, ext, inc) { }


      public ActionContinue Continue { get { return ActionContinue.NotAnAction; } }

      public override ICardMechanics Mechanics { get; protected set; }


    }
}
