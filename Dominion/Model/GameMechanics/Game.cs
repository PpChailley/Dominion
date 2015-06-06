using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using org.gbd.Dominion.Model.Zones;
using org.gbd.Dominion.Tools;

namespace org.gbd.Dominion.Model.GameMechanics
{
    public class Game : IGame
    {


        public static int MoveCards(IEnumerable<ICard> toMove, IZone from, IZone to, Position positionInTargetCollection)
        {
            foreach (var card in toMove.ToList())
            {
                if (@from.Cards.Contains(card) == false)
                    throw new InvalidOperationException(String.Format("Card {0} is not in source collection {1}", card, @from));

                @from.Cards.Remove(card);
                to.Cards.Add(card);
                card.ClearInPlayAttributes();
            }
            
            return toMove.Count();
        }

        public static void MoveCards(IZone from, IZone to, int amount = 1, Position positionFrom = Position.Top, Position positionTo = Position.Top)
        {
            MoveCards(@from.Get(amount, positionFrom), @from, to, positionTo);
        }

        public static void MoveCards(ILibrary from, IZone to, int amount = 1, Position positionFrom = Position.Top, Position positionTo = Position.Top)
        {
            if (@from.TotalCardsAvailable < amount)
                throw new NotEnoughCardsException();

            if (@from.Cards.Count < amount)
            {
                int toMoveAfterShuffle = amount - @from.Cards.Count;
                MoveCards(@from.Get(@from.Cards.Count, positionFrom), @from, to, positionTo);
                @from.ShuffleDiscardToLibrary();
                MoveCards(@from.Get(toMoveAfterShuffle, positionFrom), @from, to, positionTo);
            }
            else
            {
                MoveCards(@from.Get(amount, positionFrom), @from, to, positionTo);
            }
        }

        public static IGame G { get; set; }

        static Game()
        {
            G = IoC.Kernel.Get<IGame>();
        }


        public Game()
        {
            Players = IoC.Kernel.Get<ICollection<IPlayer>>();
        }



        public IPlayer CurrentPlayer { get; private set; }
        
        public ISupplyZone SupplyZone { get; private set; }


        


        public ICollection<IPlayer> Players { get; set; }




        public void MakeReadyToStart()
        {
            Init();

            foreach (var player in Players)
            {
                player.GetReadyToStartGame();
            }

            SupplyZone.MakeReadyToStartGame();

            throw new NotImplementedException();
        }


        public void Init()
        {
            throw new NotImplementedException();
        }

        

    }
    
}