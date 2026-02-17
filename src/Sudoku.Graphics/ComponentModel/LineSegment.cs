namespace Sudoku.ComponentModel;

/// <summary>
/// Represents an instance that describes possible shown line segments under the specified directions specified of the specified cell.
/// </summary>
/// <param name="cellIndex">The cell index.</param>
/// <param name="directions">The shown directions.</param>
[method: JsonConstructor]
public readonly struct LineSegment(Absolute cellIndex, Direction directions) :
	IEquatable<LineSegment>,
	IEqualityOperators<LineSegment, LineSegment, bool>
{
	/// <summary>
	/// Indicates the directions to be shown.
	/// </summary>
	public Direction Directions { get; } = directions;

	/// <summary>
	/// Indicates absolute cell index.
	/// </summary>
	public Absolute CellIndex { get; } = cellIndex;


	/// <summary>
	/// Deconstruct instance into multiple values.
	/// </summary>
	public void Deconstruct(out Absolute cellIndex, out Direction directions) => (cellIndex, directions) = (CellIndex, Directions);

	/// <inheritdoc/>
	public override bool Equals([NotNullWhen(true)] object? obj) => obj is LineSegment comparer && Equals(comparer);

	/// <inheritdoc/>
	public bool Equals(LineSegment other) => CellIndex == other.CellIndex && Directions == other.Directions;

	/// <inheritdoc cref="object.GetHashCode"/>
	public override int GetHashCode() => HashCode.Combine(CellIndex, Directions);

	/// <inheritdoc cref="object.ToString"/>
	public override string ToString()
		=> $$"""{{nameof(LineSegment)}} { {{nameof(CellIndex)}} = {{CellIndex}}, {{nameof(Directions)}} = {{Directions}} }""";


	/// <inheritdoc/>
	public static bool operator ==(LineSegment left, LineSegment right) => left.Equals(right);

	/// <inheritdoc/>
	public static bool operator !=(LineSegment left, LineSegment right) => !(left == right);
}
