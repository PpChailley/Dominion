namespace gbd.Dominion.Model.Zones
{
    public interface ILibrary: IZone, IMutableZone
    {
        void Ready(IDeck deck);
        IDeck ParentDeck { get; }
    }
}