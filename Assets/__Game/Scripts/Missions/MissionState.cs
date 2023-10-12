public enum MissionState
{
    /// <summary>
    /// Available for passing
    /// </summary>
    Active,

    /// <summary>
    /// Unavailable for passing, hidden for the player
    /// </summary>
    Blocked,

    /// <summary>
    /// The mission is unavailable for passing, but not hidden for the player. Only an Activated mission can be temporarily blocked.
    /// </summary>
    TemporarilyBlocked,

    /// <summary>
    /// already completed mission continues to be displayed on the map. It is impossible to re-pass already completed missions.
    /// </summary>
    Completed,
}