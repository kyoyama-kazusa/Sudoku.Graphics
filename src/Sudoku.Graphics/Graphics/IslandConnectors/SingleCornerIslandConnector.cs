namespace Sudoku.Graphics.IslandConnectors;

/// <summary>
/// Represents an island connector that will make one corner.
/// </summary>
public sealed class SingleCornerIslandConnector : IslandConnector
{
	/// <summary>
	/// Indicates the connected direction.
	/// </summary>
	public required Direction4 ConnectedDirection { get; init; }

	/// <inheritdoc/>
	protected override Type EqualityContract => typeof(SingleCornerIslandConnector);


	/// <inheritdoc/>
	public override bool Equals([NotNullWhen(true)] IslandConnector? other)
		=> other is SingleCornerIslandConnector comparer
		&& ConnectedDirection == comparer.ConnectedDirection;

	/// <inheritdoc/>
	public override int GetHashCode() => HashCode.Combine(EqualityContract, ConnectedDirection);

	/// <inheritdoc/>
	public override SingleCornerIslandConnector Clone() => new() { ConnectedDirection = ConnectedDirection };

	/// <inheritdoc/>
	protected override void PrintMembers(StringBuilder builder) => AppendMemberString(builder, ConnectedDirection);
}
