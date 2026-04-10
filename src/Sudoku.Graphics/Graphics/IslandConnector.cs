namespace Sudoku.Graphics;

/// <summary>
/// Represents an island connector.
/// </summary>
public abstract class IslandConnector :
	ICloneable,
	IEquatable<IslandConnector>,
	IEqualityOperators<IslandConnector, IslandConnector, bool>
{
	/// <summary>
	/// Indicates the start cell.
	/// </summary>
	public required Absolute StartCell { get; init; }

	/// <summary>
	/// Indicates the end cell.
	/// </summary>
	public required Absolute EndCell { get; init; }

	/// <summary>
	/// Represents equality contract.
	/// </summary>
	protected abstract Type EqualityContract { get; }


	/// <inheritdoc/>
	public sealed override bool Equals([NotNullWhen(true)] object? obj) => Equals(obj as IslandConnector);

	/// <inheritdoc/>
	public abstract bool Equals([NotNullWhen(true)] IslandConnector? other);

	/// <inheritdoc/>
	public abstract override int GetHashCode();

	/// <inheritdoc/>
	public abstract override string ToString();

	/// <inheritdoc cref="ICloneable.Clone"/>
	public abstract IslandConnector Clone();

	/// <inheritdoc/>
	object ICloneable.Clone() => Clone();


	/// <inheritdoc/>
	public static bool operator ==(IslandConnector? left, IslandConnector? right)
		=> (left, right) switch { (null, null) => true, (not null, not null) => left.Equals(right), _ => false };

	/// <inheritdoc/>
	public static bool operator !=(IslandConnector? left, IslandConnector? right) => !(left == right);
}
