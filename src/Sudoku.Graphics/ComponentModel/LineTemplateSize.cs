namespace Sudoku.ComponentModel;

/// <summary>
/// Represents logical size of a <see cref="LineTemplate"/> instance.
/// </summary>
public sealed record LineTemplateSize : IEqualityOperators<LineTemplateSize, LineTemplateSize, bool>
{
	/// <summary>
	/// Initializes a <see cref="LineTemplate"/> instance.
	/// </summary>
	public LineTemplateSize()
	{
	}

	/// <summary>
	/// Initializes a <see cref="LineTemplateSize"/> instance.
	/// </summary>
	/// <param name="rowsCount"><inheritdoc cref="RowsCount" path="/summary"/></param>
	/// <param name="columnsCount"><inheritdoc cref="ColumnsCount" path="/summary"/></param>
	/// <param name="vector"><inheritdoc cref="Vector" path="/summary"/></param>
	/// <seealso cref="LineTemplate"/>
	[SetsRequiredMembers]
	[JsonConstructor]
	public LineTemplateSize(Absolute rowsCount, Absolute columnsCount, DirectionVector vector)
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
	public bool Equals([NotNullWhen(true)] LineTemplateSize? other)
		=> other is not null && RowsCount == other.RowsCount && ColumnsCount == other.ColumnsCount && Vector == other.Vector;

	/// <inheritdoc/>
	public override int GetHashCode() => HashCode.Combine(RowsCount, ColumnsCount, Vector);

	/// <summary>
	/// Creates a new <see cref="LineTemplateSize"/> instance via the specified offset of the current instance.
	/// </summary>
	/// <param name="rowsCount">The number of rows.</param>
	/// <param name="columnsCount">The number of columns.</param>
	/// <returns>A new <see cref="LineTemplateSize"/> instance.</returns>
	public LineTemplateSize WithOffset(Relative rowsCount, Relative columnsCount)
		=> new(RowsCount, ColumnsCount, Vector + new DirectionVector(columnsCount, 0, rowsCount, 0));

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
	/// Creates a <see cref="LineTemplateSize"/> instance via the specified list of templates.
	/// Such templates will be drawn into one <see cref="Canvas"/> instance, aligning as top-left cell <c>(0, 0)</c>.
	/// </summary>
	/// <param name="templates">The templates.</param>
	/// <returns>A <see cref="LineTemplateSize"/> instance that is the minimal size, covering all templates specified.</returns>
	/// <seealso cref="Canvas"/>
	public static LineTemplateSize Create(params ReadOnlySpan<LineTemplate> templates)
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
