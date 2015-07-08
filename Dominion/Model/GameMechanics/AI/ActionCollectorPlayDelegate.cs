using System;
using System.Collections.Generic;
using System.Linq;
using gbd.Dominion.Injection;
using gbd.Dominion.Model.Cards;
using Ninject;

namespace gbd.Dominion.Model.GameMechanics.AI
{
    internal class ActionCollectorPlayDelegate: AbstractAiSpecializedDelegate, IPlayDelegate
    {
        public void PlayTurn()
        {
            while (Ai.Player.Deck.Hand.Cards.Any(c => c.Mechanics.GetCardType<ActionType>() != null))
            {
                var selected = BestAction(Ai.Player.Deck.Hand.Cards);

                if (selected == null)
                    throw new InvalidOperationException("Hand has >0 action but no action selected");

                Ai.Player.PlayAction(selected);
            }

            IoC.Kernel.Get<IGame>().EndActionsPhase();

            Ai.Player.Deck.Hand.Cards
                .Where(c => c.Mechanics.GetCardType<TreasureType>() != null)
                .ToList()
                .ForEach(c => Ai.Player.PlayTreasure(c));

            Ai.Buy();
        }

        private ICard BestAction(IList<ICard> hand)
        {
            return
                hand.FirstOrDefault(c => c.Continue == ActionContinue.Scryer) ??
                hand.FirstOrDefault(c => c.Continue == ActionContinue.ActionProvider) ??
                hand.FirstOrDefault(c => c.Continue == ActionContinue.Cantrip) ??
                hand.FirstOrDefault(c => c.Continue == ActionContinue.TerminalDraw) ??
                hand.FirstOrDefault(c => c.Continue == ActionContinue.Terminal);
        }
    }
}