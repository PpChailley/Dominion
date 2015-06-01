using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.gbd.Dominion.Model.Actions
{
    public class OpponentDraw
    {
        public static readonly String OpponentChoseMessage = "Which player(s) should discard?";


        public PlayersDesignation Who;
        
        public int Amount;

        public 

    }

    public class PlayersDesignation 
    {
        public PlayerChoice P;

        public PlayersDesignation(PlayerChoice player)
        {
            throw new NotImplementedException();
        }

        public PlayersDesignation(ICollection<PlayerChoice> players)
        {
            throw new NotImplementedException();
        }
    }

    public enum PlayerChoice
    {
        Current,
        All,
        Left, 
        Right,
        
    }
}
