namespace Sudoku.ComponentModel;

/// <summary>
/// Represents logical size of a <see cref="GridTemplate"/> instance.
/// </summary>
/// <seealso cref="GridTemplate"/>
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
	public Absolute AbsoluteRowsCount => RowsCount + Vector.Up + Vector.Down;

	/// <summary>
	/// Indiactes the number of columns. The number of columns should be an absolute value,
	/// including reserved regions (used by drawing outside-like puzzles).
	/// </summary>
	public Absolute AbsoluteColumnsCount => ColumnsCount + Vector.Left + Vector.Right;

	/// <summary>
	/// Indicates empty cells count reserved to be empty. By default it's <see cref="DirectionVector.Zero"/>.
	/// </summary>
	/// <seealso cref="DirectionVector.Zero"/>
	public DirectionVector Vector { get; init; } = DirectionVector.Zero;


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


	/// <summary>
	/// Creates a <see cref="GridTemplateSize"/> instance via the specified list of templates.
	/// Such templates will be drawn into one <see cref="Canvas"/> instance, aligning as top-left cell <c>(0, 0)</c>.
	/// </summary>
	/// <param name="templates">The templates.</param>
	/// <returns>A <see cref="GridTemplateSize"/> instance that is the minimal size, covering all templates specified.</returns>
	/// <seealso cref="Canvas"/>
	public static GridTemplateSize Create(params ReadOnlySpan<GridTemplate> templates)
	{
		if (templates.IsEmpty)
		{
			return default;
		}

		var (maxRowsCount, maxColumnsCount) = (0, 0);
		foreach (var template in templates)
		{
			if (template.Mapper.AbsoluteRowsCount is var r && r >= maxRowsCount)
			{
				maxRowsCount = r;
			}
			if (template.Mapper.AbsoluteColumnsCount is var c && c >= maxColumnsCount)
			{
				maxColumnsCount = c;
			}
		}
		return new() { RowsCount = maxRowsCount, ColumnsCount = maxColumnsCount };
	}
}
