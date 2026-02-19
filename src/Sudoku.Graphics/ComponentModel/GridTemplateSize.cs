namespace Sudoku.ComponentModel;

/// <summary>
/// Represents logical size of a <see cref="GridTemplate"/> instance.
/// </summary>
public sealed record GridTemplateSize : IEqualityOperators<GridTemplateSize, GridTemplateSize, bool>
{
	/// <summary>
	/// Initializes a <see cref="GridTemplate"/> instance.
	/// </summary>
	public GridTemplateSize()
	{
	}

	/// <summary>
	/// Initializes a <see cref="GridTemplateSize"/> instance.
	/// </summary>
	/// <param name="rowsCount"><inheritdoc cref="RowsCount" path="/summary"/></param>
	/// <param name="columnsCount"><inheritdoc cref="ColumnsCount" path="/summary"/></param>
	/// <param name="vector"><inheritdoc cref="Vector" path="/summary"/></param>
	/// <seealso cref="GridTemplate"/>
	[SetsRequiredMembers]
	[JsonConstructor]
	public GridTemplateSize(Absolute rowsCount, Absolute columnsCount, DirectionVector vector)
	{
		RowsCount = rowsCount;
		ColumnsCount = columnsCount;
		Vector = vector;
	}


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
	/// Indicates empty cells count reserved to be empty.
	/// </summary>
	public required DirectionVector Vector { get; init; }


	/// <inheritdoc/>
	public bool Equals([NotNullWhen(true)] GridTemplateSize? other)
		=> other is not null && RowsCount == other.RowsCount && ColumnsCount == other.ColumnsCount && Vector == other.Vector;

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
			return new(0, 0, DirectionVector.Zero);
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
		return new(maxRowsCount, maxColumnsCount, DirectionVector.Zero);
	}
}
