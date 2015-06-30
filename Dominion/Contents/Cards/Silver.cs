using gbd.Dominion.Model.Cards;

namespace gbd.Dominion.Contents.Cards
{
    public class Silver : Card, ICard
    {
      public Silver(ICardMechanics mechanics, GameExtension ext, Include inc)
            : base(mechanics, ext, inc) { }

      public override ICardMechanics Mechanics { get; protected set; }


    }
}
