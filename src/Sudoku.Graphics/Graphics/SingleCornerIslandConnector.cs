namespace Sudoku.Graphics;

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
		&& StartCell == comparer.StartCell && EndCell == comparer.EndCell && ConnectedDirection == comparer.ConnectedDirection;

	/// <inheritdoc/>
	public override int GetHashCode() => HashCode.Combine(EqualityContract, StartCell, EndCell, ConnectedDirection);

	/// <inheritdoc/>
	public override string ToString()
		=> $$"""{{nameof(SingleCornerIslandConnector)}} { {{nameof(StartCell)}} = {{StartCell}}, {{nameof(EndCell)}} = {{EndCell}}, {{nameof(ConnectedDirection)}} = {{ConnectedDirection}} }""";

	/// <inheritdoc/>
	public override SingleCornerIslandConnector Clone()
		=> new() { StartCell = StartCell, EndCell = EndCell, ConnectedDirection = ConnectedDirection };
}
