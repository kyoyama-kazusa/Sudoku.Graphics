namespace Sudoku.Graphics;

/// <summary>
/// Represents a point mapper instance.
/// </summary>
public sealed record PointMapper : IEqualityOperators<PointMapper, PointMapper, bool>
{
	/// <summary>
	/// Initializes a <see cref="PointMapper"/> instance via the specified cell size, margin and template size.
	/// </summary>
	/// <param name="cellSize"><inheritdoc cref="CellSize" path="/summary"/></param>
	/// <param name="margin"><inheritdoc cref="Margin" path="/summary"/></param>
	/// <param name="templateSize"><inheritdoc cref="TemplateSize" path="/summary"/></param>
	[SetsRequiredMembers]
	[JsonConstructor]
	public PointMapper(float cellSize, float margin, GridTemplateSize templateSize)
	{
		CellSize = cellSize;
		Margin = margin;
		TemplateSize = templateSize;
	}

	/// <summary>
	/// Initializes a <see cref="PointMapper"/> instance via the specified information.
	/// </summary>
	/// <param name="cellSize"><inheritdoc cref="CellSize" path="/summary"/></param>
	/// <param name="margin"><inheritdoc cref="Margin" path="/summary"/></param>
	/// <param name="rowsCount"><inheritdoc cref="RowsCount" path="/summary"/></param>
	/// <param name="columnsCount"><inheritdoc cref="ColumnsCount" path="/summary"/></param>
	/// <param name="vector"><inheritdoc cref="Vector" path="/summary"/></param>
	[SetsRequiredMembers]
	public PointMapper(float cellSize, float margin, Absolute rowsCount, Absolute columnsCount, DirectionVector vector) :
		this(cellSize, margin, new(rowsCount, columnsCount, vector))
	{
	}


	/// <summary>
	/// Indicates cell width and height of pixels. By design, cell width is equal to cell height,
	/// so this property doesn't return an instance of either type <see cref="SKSize"/> or <see cref="SKSizeI"/>.
	/// </summary>
	/// <seealso cref="SKSize"/>
	/// <seealso cref="SKSizeI"/>
	public required float CellSize { get; init; }

	/// <summary>
	/// Indicates margin (pixel size of empty spaces between the fact sudoku grid and borders of the picture).
	/// </summary>
	public required float Margin { get; init; }

	/// <inheritdoc cref="GridTemplateSize.RowsCount"/>
	public Absolute RowsCount => TemplateSize.RowsCount;

	/// <inheritdoc cref="GridTemplateSize.ColumnsCount"/>
	public Absolute ColumnsCount => TemplateSize.ColumnsCount;

	/// <inheritdoc cref="GridTemplateSize.AbsoluteRowsCount"/>
	public Absolute AbsoluteRowsCount => TemplateSize.AbsoluteRowsCount;

	/// <inheritdoc cref="GridTemplateSize.AbsoluteColumnsCount"/>
	public Absolute AbsoluteColumnsCount => TemplateSize.AbsoluteColumnsCount;

	/// <inheritdoc cref="GridTemplateSize.Vector"/>
	public DirectionVector Vector => TemplateSize.Vector;

	/// <summary>
	/// Indicates size information of the target grid to be drawn.
	/// </summary>
	public required GridTemplateSize TemplateSize { get; init; }


	/// <inheritdoc/>
	public bool Equals([NotNullWhen(true)] PointMapper? other)
		=> other is not null && CellSize == other.CellSize && Margin == other.Margin && TemplateSize == other.TemplateSize;

	/// <inheritdoc/>
	public override int GetHashCode() => HashCode.Combine(CellSize, Margin, TemplateSize);

	/// <summary>
	/// Projects the specified relative cell index into absolute one.
	/// </summary>
	/// <param name="relativeCellIndex">Relative cell index.</param>
	/// <returns>The result absolute index.</returns>
	public Absolute GetAbsoluteIndex(Relative relativeCellIndex)
	{
		var row = relativeCellIndex / ColumnsCount;
		var column = relativeCellIndex % ColumnsCount;
		var resultRow = row + Vector.Up;
		var resultColumn = column + Vector.Left;
		return resultRow * AbsoluteColumnsCount + resultColumn;
	}

	/// <summary>
	/// Projects the specified relative cell index into absolute one;
	/// with the specified direction as outside offset one, and an offset value <paramref name="offset"/>
	/// indicating the number of advanced steps of cells.
	/// </summary>
	/// <param name="relativeCellIndex">Relative cell index.</param>
	/// <param name="outsideDirection">The outside direction.</param>
	/// <param name="offset">The offset. For negative values, it'll negate <paramref name="outsideDirection"/> also.</param>
	/// <returns>The result absolute index.</returns>
	/// <exception cref="ArgumentException">Throws when <paramref name="outsideDirection"/> is not a flag.</exception>
	/// <exception cref="ArgumentOutOfRangeException">Throws when <paramref name="outsideDirection"/> is not defined.</exception>
	public Absolute GetAbsoluteIndex(Relative relativeCellIndex, Direction outsideDirection, Absolute offset)
	{
		ArgumentException.Assert(BitOperations.IsPow2((int)outsideDirection));
		ArgumentOutOfRangeException.ThrowIfUndefined(outsideDirection);

		if (offset < 0)
		{
			// Negate directions if offset is negative.
			offset = -offset;
			outsideDirection = outsideDirection.Negated;
		}

		// a + b switch {} <=> a + (b switch {})
		return GetAbsoluteIndex(relativeCellIndex) + outsideDirection switch
		{
			Direction.Up => -(AbsoluteColumnsCount * offset),
			Direction.Down => +(AbsoluteColumnsCount * offset),
			Direction.Left => -offset,
			Direction.Right => +offset,
			_ => throw new UnreachableException()
		};
	}

	/// <summary>
	/// Projects the specified absolute cell index into relative one.
	/// </summary>
	/// <param name="absoluteCellIndex">Absolute cell index.</param>
	/// <returns>The result relative index.</returns>
	public Relative GetRelativeIndex(Absolute absoluteCellIndex)
	{
		var absoluteColumnsCount = AbsoluteColumnsCount;
		var row = absoluteCellIndex / absoluteColumnsCount;
		var column = absoluteCellIndex % absoluteColumnsCount;
		var resultRow = row - Vector.Up;
		var resultColumn = column - Vector.Left;
		return resultRow * ColumnsCount + resultColumn;
	}

	/// <summary>
	/// Gets the adjacent cell at the specified direction of the specified absolute cell index.
	/// </summary>
	/// <param name="absoluteCellIndex">Absolute cell index.</param>
	/// <param name="direction">The direction.</param>
	/// <param name="isCyclicChecking">Indicates whether the cell overflown in the relative grid will be included to be checked or not.</param>
	/// <returns>Target cell absolute index.</returns>
	public Absolute GetAdjacentAbsoluteCellWith(Absolute absoluteCellIndex, Direction direction, bool isCyclicChecking)
	{
		var rowsCount = AbsoluteRowsCount;
		var columnsCount = AbsoluteColumnsCount;
		var row = absoluteCellIndex / columnsCount;
		var column = absoluteCellIndex % columnsCount;
		return direction switch
		{
			Direction.Up when row >= 1 => (row - 1) * columnsCount + column,
			Direction.Up when row == 0 && isCyclicChecking => (rowsCount - 1) * columnsCount + column,
			Direction.Down when row < rowsCount => (row + 1) * columnsCount + column,
			Direction.Down when row == rowsCount && isCyclicChecking => 0 * columnsCount + column,
			Direction.Left when column >= 1 => row * columnsCount + column - 1,
			Direction.Left when column == 0 && isCyclicChecking => row * columnsCount + columnsCount - 1,
			Direction.Right when column < columnsCount => row * columnsCount + column + 1,
			Direction.Right when column == columnsCount && isCyclicChecking => row + columnsCount + 0,
			_ => -1
		};
	}

	/// <summary>
	/// Returns the position (point) of the specified alignment type of the specified cell.
	/// </summary>
	/// <param name="absoluteCellIndex">Absolute cell index.</param>
	/// <param name="alignment">The alignment.</param>
	/// <returns>The point instance that represents the target center position.</returns>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Throws when <paramref name="alignment"/> is not defined or <see cref="Alignment.None"/>.
	/// </exception>
	/// <seealso cref="Alignment.None"/>
	public SKPoint GetPoint(Absolute absoluteCellIndex, Alignment alignment)
	{
		var columnsCount = AbsoluteColumnsCount;
		return GetPoint(absoluteCellIndex / columnsCount, absoluteCellIndex % columnsCount, alignment);
	}

	/// <summary>
	/// Returns the position (point) of the specified alignment type of the specified cell.
	/// </summary>
	/// <param name="absoluteRowIndex">Absolute row index.</param>
	/// <param name="absoluteColumnIndex">Absolute column index.</param>
	/// <param name="alignment">The alignment type.</param>
	/// <returns>The point instance that represents the target center position.</returns>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Throws when <paramref name="alignment"/> is not defined or <see cref="Alignment.None"/>.
	/// </exception>
	/// <seealso cref="Alignment.None"/>
	public SKPoint GetPoint(Absolute absoluteRowIndex, Absolute absoluteColumnIndex, Alignment alignment)
	{
		var topLeft = new SKPoint(CellSize * absoluteColumnIndex + Margin, CellSize * absoluteRowIndex + Margin);
		return alignment switch
		{
			Alignment.Center => topLeft + new SKPoint(CellSize / 2, CellSize / 2),
			Alignment.TopLeft => topLeft,
			Alignment.TopRight => topLeft + new SKPoint(CellSize, 0),
			Alignment.BottomLeft => topLeft + new SKPoint(0, CellSize),
			Alignment.BottomRight => topLeft + new SKPoint(CellSize, CellSize),
			_ => throw new ArgumentOutOfRangeException(nameof(alignment))
		};
	}

	/// <summary>
	/// Returns the position (point) of the specified alignment type of the specified candidate (absolute).
	/// </summary>
	/// <param name="candidatePosition">Absolute candidate position.</param>
	/// <param name="alignment">The alignment.</param>
	/// <returns>The point instance that represents the target center position.</returns>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Throws when <paramref name="alignment"/> is not defined or <see cref="Alignment.None"/>.
	/// </exception>
	/// <seealso cref="Alignment.None"/>
	public SKPoint GetPoint(CandidatePosition candidatePosition, Alignment alignment)
	{
		var (cell, subgridSize, innerIndex) = candidatePosition;
		var cellTopLeft = GetPoint(cell, Alignment.TopLeft);
		var candidateSize = CellSize / subgridSize;
		var candidateRowIndex = innerIndex / subgridSize;
		var candidateColumnIndex = innerIndex % subgridSize;
		var topLeft = cellTopLeft + new SKPoint(candidateColumnIndex * candidateSize, candidateRowIndex * candidateSize);
		return alignment switch
		{
			Alignment.Center => topLeft + new SKPoint(candidateSize / 2, candidateSize / 2),
			Alignment.TopLeft => topLeft,
			Alignment.TopRight => topLeft + new SKPoint(candidateSize, 0),
			Alignment.BottomLeft => topLeft + new SKPoint(0, candidateSize),
			Alignment.BottomRight => topLeft + new SKPoint(candidateSize, candidateSize),
			_ => throw new ArgumentOutOfRangeException(nameof(alignment))
		};
	}

	/// <summary>
	/// Creates a new <see cref="PointMapper"/> instance via the specified offset, replacing with new value.
	/// </summary>
	/// <param name="vector">The direction vector as offset.</param>
	/// <returns>The result <see cref="PointMapper"/> instance.</returns>
	public PointMapper WithOffset(DirectionVector vector) => this with { TemplateSize = TemplateSize with { Vector = vector } };

	/// <summary>
	/// Creates a new <see cref="PointMapper"/> instance via the specified offset, adding to original template size direction vector.
	/// </summary>
	/// <param name="vector">The direction vector as offset.</param>
	/// <returns>The result <see cref="PointMapper"/> instance.</returns>
	public PointMapper AddOffset(DirectionVector vector)
		=> this with { TemplateSize = TemplateSize with { Vector = TemplateSize.Vector + vector } };

	private bool PrintMembers(StringBuilder builder)
	{
		builder.Append($"{nameof(CellSize)} = {CellSize:0.0###}, ");
		builder.Append($"{nameof(Margin)} = {Margin:0.0###}, ");
		builder.Append($"{nameof(TemplateSize)} = {TemplateSize}");
		return true;
	}
}
