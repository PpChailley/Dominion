using org.gbd.Dominion.Model;
using org.gbd.Dominion.Model.Cards;

namespace org.gbd.Dominion.Contents
{
    public class Estate : BaseSetCard
    {
        public Estate()
        {
            Mechanics.Cost = new Resources(2);
            Mechanics.Types.Add(new Victory(1));
        }
    }
}