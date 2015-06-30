using System.Linq;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.GameMechanics.Actions;
using Ninject;

namespace gbd.Dominion.Injection
{
    internal class TrashThis : GameAction, IGameAction
    {
        public override void Do()
        {
            var game = IoC.Kernel.Get<IGame>();

            game
                .CurrentPlayer
                .Deck.BattleField.Cards.Last()
                .MoveTo(game.Trash);
        }
    }
}