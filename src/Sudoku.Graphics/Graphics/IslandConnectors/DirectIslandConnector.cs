namespace Sudoku.Graphics.IslandConnectors;

/// <summary>
/// Represents an island connector that won't bend the connection.
/// </summary>
public sealed class DirectIslandConnector : IslandConnector
{
	/// <inheritdoc/>
	protected override Type EqualityContract => typeof(DirectIslandConnector);


	/// <inheritdoc/>
	public override bool Equals([NotNullWhen(true)] IslandConnector? other) => other is DirectIslandConnector;

	/// <inheritdoc/>
	public override int GetHashCode() => HashCode.Combine(EqualityContract);

	/// <inheritdoc/>
	public override DirectIslandConnector Clone() => new();

	/// <inheritdoc/>
	protected override void PrintMembers(StringBuilder builder)
	{
	}
}
