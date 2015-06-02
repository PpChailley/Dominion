namespace org.gbd.Dominion.Model.Cards
{
    public class Province : Card, ICard
    {
        public Province()
        {
            Mechanics.VictoryPoints = 6;
            Mechanics.Cost = new Resources(8);
        }
    }
}