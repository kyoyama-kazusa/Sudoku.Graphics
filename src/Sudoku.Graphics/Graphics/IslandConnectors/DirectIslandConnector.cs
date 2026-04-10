namespace Sudoku.Graphics.IslandConnectors;

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
	public override DirectIslandConnector Clone() => new() { StartCell = StartCell, EndCell = EndCell };

	/// <inheritdoc/>
	protected override void PrintMembers(StringBuilder builder)
	{
		AppendMemberString(builder, StartCell);
		AppendMemberString(builder, EndCell);
	}
}
