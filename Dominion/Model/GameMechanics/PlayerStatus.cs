using gbd.Dominion.Injection;
using Ninject;

namespace gbd.Dominion.Model.GameMechanics
{
    public class PlayerStatus
    {

        public Resources Resources = IoC.Kernel.Get<Resources>();
        public int AvailableActions { get; set; }
        public int AvailableBuys { get; set; }

        
        public void StartTurn()
        {
            AvailableActions = 1;
            AvailableBuys = 1;
            Resources.Reset();
        }

    }
}