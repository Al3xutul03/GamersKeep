namespace Repository.Enums.Behaviours;

/// <summary>
/// Enum type for different include behaviours in the querry builder
/// </summary>
public enum IncludeBehaviour
{
    /// <summary>
    /// No inclusions should be made in the querry, only the data in the queried entity
    /// </summary>
    NoInclude,

    /// <summary>
    /// The querry attempts to include all tables it is directly
    /// linked to, this is more taxing on performance
    /// </summary>
    AllIncludes,

    /// <summary>
    /// Use the query given in the parameters
    /// </summary>
    GivenIncludes,
}