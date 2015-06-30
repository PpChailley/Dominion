using gbd.Dominion.Contents;

namespace gbd.Dominion.Model.Cards
{
    public abstract class PrintedCard
    {

        public GameExtension Extension { get; private set; }
        public Include PresentInSet { get; private set; }


        protected PrintedCard(GameExtension ext, Include inc)
        {
            Extension = ext;
            PresentInSet = inc;
        }


    }
}
