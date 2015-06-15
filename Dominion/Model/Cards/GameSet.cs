namespace org.gbd.Dominion.Model.Cards
{
    public enum GameSet
    {
        /// <summary>
        /// A card that is always included in the supply at game start. 
        /// Like Copper, Curse, Province
        /// </summary>
        AlwaysIncluded,

        /// <summary>
        /// A card that can be included in the supply or not, as selected by the game creator, 
        /// and DO NOT count towards the 10 piles limit. 
        /// Like Platinum or any Ruins
        /// </summary>
        Optional,

        /// <summary>
        /// A standard card that can be randomly included in the supply by the game creation process 
        /// and count towards the 10 piles limit.
        /// Like most cards : Village, Hermit, Lighthouse, ...
        /// </summary>
        Selectable,

        /// <summary>
        /// A card that is included or not in the supply depending on a complex condition.
        /// Like Madman, that is included if and only if the supply contains Hermit
        /// </summary>
        Conditional,

        /// <summary>
        /// A card that is included in the supply only in test games
        /// </summary>
        TestCards
    }
}