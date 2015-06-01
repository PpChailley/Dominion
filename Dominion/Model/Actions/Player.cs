using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace org.gbd.Dominion.Model.Actions
{
    public class Player
    {
        public static ICollection<Player> get(ICollection<PlayerChoice> designatedPlayers)
        {
            return designatedPlayers.Select(player => get(player)).ToList();
        }

        private static Player get(PlayerChoice designatedPlayer)
        {
            switch (designatedPlayer)
            {
                case PlayerChoice.Current:
                    return CurrentPlayer;

                case PlayerChoice.Left:
                case PlayerChoice.Right:
                    throw new NotImplementedException();

                default :
                    throw new NotImplementedException();


            }
        }

        public Player CurrentPlayer
        {
            get
            {
                throw new NotImplementedException();
            }
        }



        public String Name;

        public int CurrentScore
        {
            get
            {
                throw new NotImplementedException();
            }
        }




    }
}