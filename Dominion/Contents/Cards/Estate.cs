using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;

namespace gbd.Dominion.Contents.Cards
{
    public class Estate : AlwaysInSupplyCard, ICard
    {
        public Estate(ICardMechanics mechanics) : base(mechanics){}

        public override ICardMechanics Mechanics { get; protected set; }
 
    }
}