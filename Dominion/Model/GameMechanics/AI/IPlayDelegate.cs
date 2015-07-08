namespace gbd.Dominion.Model.GameMechanics.AI
{
    public interface IPlayDelegate: IAiSpecializedDelegate
    {
        void PlayTurn();
    }
}