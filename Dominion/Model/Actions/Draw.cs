using System.Media;

namespace org.gbd.Dominion.Model.Actions
{
    public class Draw: GameAction
    {
        public PlayersDesignation DesignatedPlayer;
        public int Amount;


        public Draw(int amount = 1)
        {
            Amount = amount;
        }


        public override void Do()
        {
            Player.get(DesignatedPlayer).Deck.Draw(this.Amount);
        }

        

    }
}
