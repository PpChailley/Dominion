using Ninject;
using org.gbd.Dominion.Tools;

namespace org.gbd.Dominion.Model.GameMechanics
{
    public class PlayerStatus
    {

        public Resources Resources = IoC.Kernel.Get<Resources>();
        public int AvailableActions = 0;
        public int AvailableBuys = 0;

        
        public void StartTurn()
        {
            AvailableActions = 1;
            AvailableBuys = 1;
            Resources.Reset();
        }

    }
}