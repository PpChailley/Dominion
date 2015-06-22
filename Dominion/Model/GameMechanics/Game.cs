using System;
using System.Collections.Generic;
using System.Linq;
using gbd.Dominion.Model.Zones;
using gbd.Dominion.Tools;
using gbd.Tools.Cli;
using Ninject;

namespace gbd.Dominion.Model.GameMechanics
{
    public class Game : IGame
    {
        [Inject]
        public Game(IList<IPlayer> players, ISupplyZone supplyZone)
        {
            Players = players;
            SupplyZone = supplyZone;
            CurrentPlayer = IoC.Kernel.Get<Player>();
        }


        public ISupplyZone SupplyZone { get; private set; }

        public IList<IPlayer> Players { get; private set; }

        
        public IPlayer CurrentPlayer { get; private set; }
 


        public IList<IPlayer> GetPlayers(PlayerChoice who)
        {
            var toreturn = new List<IPlayer>();

            switch (who)
            {
                case PlayerChoice.Current:
                    toreturn.Add(CurrentPlayer);
                    break;

                case PlayerChoice.Left:
                    toreturn.Add(Players.Before(CurrentPlayer));
                    break;

                case PlayerChoice.Right:
                    toreturn.Add(Players.After(CurrentPlayer));
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
            Init();

            Players.ToList().ForEach(p => p.Ready());

            SupplyZone.Ready();

            CurrentPlayer = Players.First();
        }


        public void Init()
        {
            // Nothing to do yet
        }

        

    }
    
}