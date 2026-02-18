namespace Sudoku.ComponentModel;

/// <summary>
/// Represents logical size of a <see cref="LineTemplate"/> instance.
/// </summary>
/// <param name="rowsCount"><inheritdoc cref="RowsCount" path="/summary"/></param>
/// <param name="columnsCount"><inheritdoc cref="ColumnsCount" path="/summary"/></param>
/// <param name="vector"><inheritdoc cref="Vector" path="/summary"/></param>
/// <seealso cref="LineTemplate"/>
[method: SetsRequiredMembers]
public sealed class LineTemplateSize(Absolute rowsCount, Absolute columnsCount, DirectionVector vector) :
	IEquatable<LineTemplateSize>,
	IEqualityOperators<LineTemplateSize, LineTemplateSize, bool>
{
	/// <summary>
	/// Initializes a <see cref="LineTemplateSize"/> instance via the other <see cref="LineTemplateSize"/> instance,
	/// copying all properties from it.
	/// </summary>
	/// <param name="other">The other instance to be copied.</param>
	[SetsRequiredMembers]
	public LineTemplateSize(LineTemplateSize other) : this(other.RowsCount, other.ColumnsCount, other.Vector)
	{
	}


	/// <summary>
	/// Indicates the number of rows in main sudoku grid.
	/// </summary>
	public required Absolute RowsCount { get; init; } = rowsCount;

	/// <summary>
	/// Indicates the number of columns in main sudoku grid.
	/// </summary>
	public required Absolute ColumnsCount { get; init; } = columnsCount;

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
	public required DirectionVector Vector { get; init; } = vector;


	/// <inheritdoc/>
	public override bool Equals([NotNullWhen(true)] object? obj) => Equals(obj as LineTemplateSize);

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


	/// <inheritdoc/>
	public static bool operator ==(LineTemplateSize? left, LineTemplateSize? right)
		=> (left, right) switch { (null, null) => true, (not null, not null) => left.Equals(right), _ => false };

	/// <inheritdoc/>
	public static bool operator !=(LineTemplateSize? left, LineTemplateSize? right) => !(left == right);
}
