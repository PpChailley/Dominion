using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;

namespace gbd.Dominion.Contents.Cards
{
    public class Estate : AlwaysInSupplyCard, ICard
    {
        public override ICardMechanics Mechanics { get; set; }
        public Estate()
        {
            //Mechanics.Cost = new Resources(2);
            //Mechanics.Types.Add(new VictoryType(1));
        }
    }
}