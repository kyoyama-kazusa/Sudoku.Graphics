namespace Sudoku.Graphics;

/// <summary>
/// Represents an island connector that will make double corner.
/// </summary>
public sealed class DoubleCornerIslandConnector : IslandConnector
{
	/// <summary>
	/// Indicates the offset.
	/// </summary>
	public required Relative Offset { get; init; }

	/// <summary>
	/// Indicates the start connected direction.
	/// </summary>
	public required Direction4 StartConnectedDirection { get; init; }

	/// <summary>
	/// Indicates the end connection direction.
	/// </summary>
	public required Direction4 EndConnectedDirection { get; init; }

	/// <inheritdoc/>
	protected override Type EqualityContract => typeof(DoubleCornerIslandConnector);


	/// <inheritdoc/>
	public override bool Equals([NotNullWhen(true)] IslandConnector? other)
		=> other is DoubleCornerIslandConnector comparer
		&& StartCell == comparer.StartCell && EndCell == comparer.EndCell && Offset == comparer.Offset
		&& StartConnectedDirection == comparer.StartConnectedDirection && EndConnectedDirection == comparer.StartConnectedDirection;

	/// <inheritdoc/>
	public override int GetHashCode() => HashCode.Combine(EqualityContract, StartCell, EndCell, Offset, StartConnectedDirection, EndConnectedDirection);

	/// <inheritdoc/>
	public override string ToString()
		=> $$"""{{nameof(DoubleCornerIslandConnector)}} { {{nameof(StartCell)}} = {{StartCell}}, {{nameof(EndCell)}} = {{EndCell}}, {{nameof(Offset)}} = {{Offset}}, {{nameof(StartConnectedDirection)}} = {{StartConnectedDirection}}, {{nameof(EndConnectedDirection)}} = {{EndConnectedDirection}} }""";

	/// <inheritdoc/>
	public override DoubleCornerIslandConnector Clone()
		=> new()
		{
			StartCell = StartCell,
			EndCell = EndCell,
			Offset = Offset,
			StartConnectedDirection = StartConnectedDirection,
			EndConnectedDirection = EndConnectedDirection
		};
}
