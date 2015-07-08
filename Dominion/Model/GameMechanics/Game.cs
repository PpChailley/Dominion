using System;
using System.Collections.Generic;
using System.Linq;
using gbd.Dominion.Model.Zones;
using gbd.Tools.Clr;
using Ninject;
using NLog;

namespace gbd.Dominion.Model.GameMechanics
{
    public class Game : IGame
    {
        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();


        [Inject]
        public Game(IList<IPlayer> players, ISupplyZone supplyZone, ITrashZone trash)
        {
            Players = players;
            SupplyZone = supplyZone;
            Trash = trash;

            CurrentPlayer = players.FirstOrDefault();
            TurnIndex = 0;
        }


        public ISupplyZone SupplyZone { get; private set; }

        public IList<IPlayer> Players { get; private set; }

        
        public IPlayer CurrentPlayer { get; private set; }

        public ITrashZone Trash { get; set; }

        public GamePhase CurrentPhase { get; set; }

        public int TurnIndex { get; private set; }


        public IList<IPlayer> GetPlayers(PlayerChoice who)
        {
            var toreturn = new List<IPlayer>();

            switch (who)
            {
                case PlayerChoice.Current:
                    toreturn.Add(CurrentPlayer);
                    break;

                case PlayerChoice.Left:
                    toreturn.Add(Players.BeforeRoundRobin(CurrentPlayer));
                    break;

                case PlayerChoice.Right:
                    toreturn.Add(Players.AfterRoundRobin(CurrentPlayer));
                    break;

                case PlayerChoice.Opponents:
                    toreturn.AddRange(Players.Except( new[] {CurrentPlayer} ));
                    break;


                default:
                    throw new NotImplementedException();
            }


            return toreturn;
        }



        public void Ready()
        {
            Players.ToList().ForEach(p => p.Ready());
            SupplyZone.Ready();
            Log.Info("Current player is now {0} (first time)", CurrentPlayer);
            CurrentPlayer = Players.First();
            CurrentPlayer.StartTurn();
            Log.Info("Entering ACTION Phase");
            CurrentPhase = GamePhase.Action;
        }



        public void NextTurn()
        {
            Log.Info("Entering CLEANUP Phase");
            CurrentPhase = GamePhase.Cleanup;
            CurrentPlayer.EndTurn();
            CurrentPlayer = Players.AfterRoundRobin(CurrentPlayer);
            TurnIndex += CurrentPlayer == Players[0] ? 1 : 0;
            Log.Info("Current player is now {0} - Turn {1}", CurrentPlayer, TurnIndex);
            CurrentPlayer.StartTurn();
            Log.Info("Entering ACTION Phase");
            CurrentPhase = GamePhase.Action;
        }

        public void EndActionsPhase()
        {
            Log.Info("Entering BUY Phase");
            CurrentPhase = GamePhase.Buy;
        }

        
    }
    
}