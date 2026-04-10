namespace Sudoku.Graphics;

/// <summary>
/// Represents an island connector that won't bend the connection.
/// </summary>
public sealed class DirectIslandConnector : IslandConnector
{
	/// <inheritdoc/>
	protected override Type EqualityContract => typeof(DirectIslandConnector);


	/// <inheritdoc/>
	public override bool Equals([NotNullWhen(true)] IslandConnector? other)
		=> other is DirectIslandConnector comparer && StartCell == comparer.StartCell && EndCell == comparer.EndCell;

	/// <inheritdoc/>
	public override int GetHashCode() => HashCode.Combine(EqualityContract, StartCell, EndCell);

	/// <inheritdoc/>
	public override string ToString()
		=> $$"""{{nameof(DirectIslandConnector)}} { {{nameof(StartCell)}} = {{StartCell}}, {{nameof(EndCell)}} = {{EndCell}} }""";

	/// <inheritdoc/>
	public override DirectIslandConnector Clone() => new() { StartCell = StartCell, EndCell = EndCell };
}
