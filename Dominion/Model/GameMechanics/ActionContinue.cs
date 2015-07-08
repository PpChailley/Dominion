namespace gbd.Dominion.Model.GameMechanics
{
    /// <summary>
    /// Describes what can be played after this card
    /// </summary>
    public enum ActionContinue
    {

        /// <summary>
        /// Card is not an action card
        /// </summary>
        NotAnAction,

        /// <summary>
        /// Does not give extra actions, thus may terminate turn
        /// </summary>
        Terminal,

        /// <summary>
        /// Does not give extra actions, and may terminate turn
        /// If more actions available, can bring new options to hand
        /// </summary>
        TerminalDraw,

        /// <summary>
        /// Gives one extra action, so turn can continue
        /// </summary>
        Cantrip,

        /// <summary>
        /// Gives more than one extra action
        /// </summary>
        ActionProvider,

        /// <summary>
        /// Gives at least one extra action and allows to select top of deck for next draw
        /// </summary>
        Scryer

    }
}