using Ninject;

namespace gbd.Dominion.Model.GameMechanics.AI
{
    internal abstract class AbstractAiSpecializedDelegate : IAiSpecializedDelegate
    {

        public IAi Ai { get; private set; }


        public IAiSpecializedDelegate Init(IAi ai)
        {
            Ai = ai;
            return this;
        }
    }
}