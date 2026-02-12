namespace Sudoku.Graphics;

/// <summary>
/// Represents a collection of <see cref="SerializableColor"/> instances.
/// </summary>
/// <seealso cref="SerializableColor"/>
public sealed class SerializableColorSet : List<SerializableColor>
{
	/// <inheritdoc cref="List{T}.Slice(int, int)"/>
	public new SerializableColorSet Slice(int start, int length) => [.. base.Slice(start, length)];
}
