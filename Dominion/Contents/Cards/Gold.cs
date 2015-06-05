using org.gbd.Dominion.Model;
using org.gbd.Dominion.Model.Cards;

namespace org.gbd.Dominion.Contents
{
    public class Gold : BaseSetCard
    {
        public Gold()
        {
            Mechanics.Cost = new Resources(6);
            Mechanics.Types.Add(new Treasure(3));
        }
    }
}
