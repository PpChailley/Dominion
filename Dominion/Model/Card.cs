namespace org.gbd.Dominion.Model
{
    public class Card : ICard
    {

        private readonly CardMechanics _mechanics = new CardMechanics();

        public CardMechanics Mechanics
        {
            get { return _mechanics; }
        }
    }
}