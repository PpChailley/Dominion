using gbd.Dominion.Injection;
using Ninject;

namespace gbd.Dominion.Model.GameMechanics
{
    public class PlayerStatus
    {

        public Resources Resources;
        public int Actions { get; set; }
        public int Buys { get; set; }

        public PlayerStatus()
        {
            Actions = 0;
            Resources = new Resources(0);
            Buys = 0;
        }


        public void StartTurn()
        {
            Actions = 1;
            Buys = 1;
            Resources = Resources.Zero;
        }

        public void EndTurn()
        {
            Actions = 0;
            Buys = 0;
            Resources = Resources.Zero;
        }

    }
}