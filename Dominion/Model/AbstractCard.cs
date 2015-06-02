using System;

namespace org.gbd.Dominion.Model
{
    public abstract class AbstractCard: ICard
    {
        public int Id;
        public String Name;
        public String Text;

        // TODO: Change type to more precise 
        public Object Illustration;

        public CardMechanics C;


        public abstract CardMechanics Mechanics { get; }
    }
}
