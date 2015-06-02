namespace org.gbd.Dominion.Model.Cards
{
    public class Estate: Card, ICard
    {
        public Estate()
        {
            Mechanics.VictoryPoints = 1;
            Mechanics.Cost = new Cost(2);
        }
    }
}