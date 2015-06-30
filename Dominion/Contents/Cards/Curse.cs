using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;

namespace gbd.Dominion.Contents.Cards
{
    public class Curse: AlwaysInSupplyCard
    {
        public Curse(ICardMechanics mechanics) : base(mechanics) { }
        public override ICardMechanics Mechanics { get; protected set; }

        
    }
}

