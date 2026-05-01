namespace Sudoku.Graphics;

/// <summary>
/// Represents logical size of a <see cref="Template"/> instance.
/// </summary>
/// <seealso cref="Template"/>
public readonly record struct GridTemplateSize() : IEqualityOperators<GridTemplateSize, GridTemplateSize, bool>
{
	/// <summary>
	/// Indicates the number of rows in main sudoku grid.
	/// </summary>
	public required Absolute RowsCount { get; init; }

	/// <summary>
	/// Indicates the number of columns in main sudoku grid.
	/// </summary>
	public required Absolute ColumnsCount { get; init; }

	/// <summary>
	/// Indicates the number of rows. The number of rows should be an absolute value,
	/// including reserved regions (used by drawing outside-like puzzles).
	/// </summary>
	public Absolute AbsoluteRowsCount => RowsCount + Vector.Top + Vector.Bottom;

	/// <summary>
	/// Indiactes the number of columns. The number of columns should be an absolute value,
	/// including reserved regions (used by drawing outside-like puzzles).
	/// </summary>
	public Absolute AbsoluteColumnsCount => ColumnsCount + Vector.Left + Vector.Right;

	/// <summary>
	/// Indicates empty cells count reserved to be empty. By default it's <see cref="Thickness{T}.Zero"/>.
	/// </summary>
	/// <seealso cref="Thickness{T}.Zero"/>
	public Thickness<Relative> Vector { get; init; } = Thickness<Relative>.Zero;


	/// <inheritdoc/>
	public bool Equals(GridTemplateSize other)
		=> RowsCount == other.RowsCount && ColumnsCount == other.ColumnsCount && Vector == other.Vector;

	/// <inheritdoc/>
	public override int GetHashCode() => HashCode.Combine(RowsCount, ColumnsCount, Vector);

	private bool PrintMembers(StringBuilder builder)
	{
		builder.Append("Size = ");
		builder.Append(RowsCount);
		builder.Append('x');
		builder.Append(ColumnsCount);
		builder.Append(", ");
		builder.Append(nameof(Vector));
		builder.Append(" = ");
		builder.Append(Vector.ToString());
		return true;
	}
}
