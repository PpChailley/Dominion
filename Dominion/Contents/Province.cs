using org.gbd.Dominion.Model;
using org.gbd.Dominion.Model.Cards;

namespace org.gbd.Dominion.Contents
{
    public class Province : BaseSetCard
    {
        public Province()
        {
            Mechanics.Cost = new Resources(8);
            Mechanics.Types.Add(new Victory(6));
        }
    }
}