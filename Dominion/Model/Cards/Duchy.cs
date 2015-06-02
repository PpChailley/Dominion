namespace org.gbd.Dominion.Model.Cards
{
    public class Duchy : Card, ICard
    {
        public Duchy()
        {
            Mechanics.VictoryPoints = 3;
            Mechanics.Cost = new Resources(5);
        }
    }
}