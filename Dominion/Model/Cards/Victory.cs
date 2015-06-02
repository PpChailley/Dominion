namespace org.gbd.Dominion.Model.Cards
{
    public class Victory : ICardType
    {
        public int VictoryPoints;

        public Victory(int points)
        {
            VictoryPoints = points;
        }
    }
}