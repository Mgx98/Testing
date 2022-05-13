namespace Entities.Enemies
{
    /// <summary>
    ///     This enum defines whether the enemy knows or not of your existence.
    /// </summary>
    public enum NotifiedState
    {
        /// <summary>
        ///     Enemy doesn't know where the player is.
        /// </summary>
        Unknown,

        /// <summary>
        ///     Enemy knows where the player is.
        /// </summary>
        Known,

        /// <summary>
        ///     Enemy is searching for the player, "feels" player is close.
        /// </summary>
        Notified
    }
}