namespace Sudoku.Graphics;

/// <summary>
/// Represents a point mapper instance.
/// </summary>
public sealed record PointMapper : IEqualityOperators<PointMapper, PointMapper, bool>
{
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
	public Thickness<Relative> Vector => TemplateSize.Vector;

	/// <summary>
	/// Indicates size information of the target grid to be drawn.
	/// </summary>
	public required GridTemplateSize TemplateSize { get; init; }


	/// <inheritdoc/>
	public bool Equals([NotNullWhen(true)] PointMapper? other)
		=> other is not null && CellSize == other.CellSize && Margin == other.Margin && TemplateSize == other.TemplateSize;

	/// <summary>
	/// Determine whether two cell indices are aligned as the specified type of alignment in grid.
	/// </summary>
	/// <param name="gridAlignment">The grid alignment to be checked.</param>
	/// <param name="first">The first value.</param>
	/// <param name="second">The second value.</param>
	/// <returns>A <see cref="bool"/> result indicating that.</returns>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Throws when <paramref name="gridAlignment"/> is not defined or <see cref="LocatorGridAlignment.None"/>.
	/// </exception>
	public bool IsAlignedAs(LocatorGridAlignment gridAlignment, Absolute first, Absolute second)
	{
		var (rowsCount, columnsCount) = (AbsoluteRowsCount, AbsoluteColumnsCount);
		var row1 = first / columnsCount;
		var column1 = first % columnsCount;
		var row2 = second / columnsCount;
		var column2 = second % columnsCount;
		return gridAlignment switch
		{
			LocatorGridAlignment.FirstRow or LocatorGridAlignment.LastRow
				=> row1 == row2 && row1 == (gridAlignment == LocatorGridAlignment.FirstRow ? 0 : rowsCount - 1),
			LocatorGridAlignment.FirstColumn or LocatorGridAlignment.LastColumn
				=> column1 == column2 && column1 == (gridAlignment == LocatorGridAlignment.FirstColumn ? 0 : columnsCount - 1),
			_
				=> throw new ArgumentOutOfRangeException(nameof(gridAlignment))
		};
	}

	/// <inheritdoc cref="IsAlignedAs(LocatorGridAlignment, Absolute, Absolute)"/>
	public bool IsAlignedAs(LocatorGridAlignment gridAlignment, Relative first, Relative second)
		=> IsAlignedAs(gridAlignment, GetAbsoluteIndex(first), GetAbsoluteIndex(second));

	/// <summary>
	/// Determine whether two <see cref="CandidatePosition"/> instances are aligned as the specified type of alignment in grid.
	/// </summary>
	/// <param name="gridAlignment">The grid alignment to be checked.</param>
	/// <param name="first">The first value.</param>
	/// <param name="second">The second value.</param>
	/// <returns>A <see cref="bool"/> result indicating that.</returns>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Throws when <paramref name="gridAlignment"/> is not defined or <see cref="LocatorGridAlignment.None"/>.
	/// </exception>
	/// <seealso cref="CandidatePosition"/>
	public bool IsAlignedAs(LocatorGridAlignment gridAlignment, CandidatePosition first, CandidatePosition second)
		=> IsAlignedAs(gridAlignment, first.Cell, second.Cell);

	/// <summary>
	/// Determine whether two generic locator instances are aligned as the specified type of alignment in grid.
	/// </summary>
	/// <typeparam name="TLocator">The type of locator instance.</typeparam>
	/// <param name="gridAlignment">The grid alignment to be checked.</param>
	/// <param name="first">The first value.</param>
	/// <param name="second">The second value.</param>
	/// <returns>A <see cref="bool"/> result indicating that.</returns>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Throws when <paramref name="gridAlignment"/> is not defined or <see cref="LocatorGridAlignment.None"/>.
	/// </exception>
	public bool IsAlignedAs<TLocator>(LocatorGridAlignment gridAlignment, TLocator first, TLocator second)
		where TLocator : unmanaged, ILocator<TLocator>
		=> (first, second) switch
		{
			(Absolute a, Absolute b) => IsAlignedAs(gridAlignment, a, b),
			(Relative a, Relative b) => IsAlignedAs(gridAlignment, a, b),
			(CandidatePosition a, CandidatePosition b) => IsAlignedAs(gridAlignment, a, b),
			_ => throw new NotSupportedException("The specified type of locator is not supported.")
		};

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
		var resultRow = row + Vector.Top;
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
	public Absolute GetAbsoluteIndex(Relative relativeCellIndex, Direction4 outsideDirection, Absolute offset)
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
			Direction4.Up => -(AbsoluteColumnsCount * offset),
			Direction4.Down => +(AbsoluteColumnsCount * offset),
			Direction4.Left => -offset,
			Direction4.Right => +offset,
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
		var resultRow = row - Vector.Top;
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
	public Absolute GetAdjacentAbsoluteCellWith(Absolute absoluteCellIndex, Direction4 direction, bool isCyclicChecking)
	{
		var rowsCount = AbsoluteRowsCount;
		var columnsCount = AbsoluteColumnsCount;
		var row = absoluteCellIndex / columnsCount;
		var column = absoluteCellIndex % columnsCount;
		return direction switch
		{
			Direction4.Up when row >= 1 => (row - 1) * columnsCount + column,
			Direction4.Up when row == 0 && isCyclicChecking => (rowsCount - 1) * columnsCount + column,
			Direction4.Down when row < rowsCount => (row + 1) * columnsCount + column,
			Direction4.Down when row == rowsCount && isCyclicChecking => 0 * columnsCount + column,
			Direction4.Left when column >= 1 => row * columnsCount + column - 1,
			Direction4.Left when column == 0 && isCyclicChecking => row * columnsCount + columnsCount - 1,
			Direction4.Right when column < columnsCount => row * columnsCount + column + 1,
			Direction4.Right when column == columnsCount && isCyclicChecking => row + columnsCount + 0,
			_ => -1
		};
	}

	/// <summary>
	/// Returns the position (point) of the specified alignment type of the specified cell.
	/// </summary>
	/// <param name="absoluteCellIndex">Absolute cell index.</param>
	/// <param name="alignment">The alignment.</param>
	/// <returns>The point instance that represents the target position.</returns>
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
	/// <returns>The point instance that represents the target position.</returns>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Throws when <paramref name="alignment"/> is not defined or <see cref="Alignment.None"/>.
	/// </exception>
	/// <seealso cref="Alignment.None"/>
	public SKPoint GetPoint(Absolute absoluteRowIndex, Absolute absoluteColumnIndex, Alignment alignment)
	{
		var topLeft = new SKPoint(CellSize * absoluteColumnIndex + Margin, CellSize * absoluteRowIndex + Margin);
		return alignment switch
		{
			Alignment.Center => topLeft + (CellSize / 2, CellSize / 2),
			Alignment.TopLeft => topLeft,
			Alignment.TopRight => topLeft + (CellSize, 0),
			Alignment.BottomLeft => topLeft + (0, CellSize),
			Alignment.BottomRight => topLeft + (CellSize, CellSize),
			_ => throw new ArgumentOutOfRangeException(nameof(alignment))
		};
	}

	/// <summary>
	/// Returns the position (point) of the specified alignment type of the specified cell or candidate.
	/// </summary>
	/// <typeparam name="TLocator">The type of locator (cell or candidate).</typeparam>
	/// <param name="locator">The locator object (cell or candidate).</param>
	/// <param name="alignment">The alignment.</param>
	/// <returns>The point instance that represents the target position.</returns>
	/// <exception cref="NotSupportedException">
	/// Throws when type <typeparamref name="TLocator"/> is not <see cref="Absolute"/>,
	/// <see cref="Relative"/> or <see cref="CandidatePosition"/>.
	/// </exception>
	public SKPoint GetPoint<TLocator>(TLocator locator, Alignment alignment)
		where TLocator : unmanaged, ILocator<TLocator>
		=> locator switch
		{
			Absolute cell => GetPoint(cell, alignment),
			Relative cell => GetPoint(GetAbsoluteIndex(cell), alignment),
			CandidatePosition candidate => GetPoint(candidate, alignment),
			_ => throw new NotSupportedException($"The specified type '{typeof(TLocator).Name}' is not supported - it must be of type '{nameof(Absolute)}', '{nameof(Relative)}' or '{nameof(CandidatePosition)}'.")
		};

	/// <summary>
	/// Returns the position (point) of the specified alignment type of the specified candidate (absolute).
	/// </summary>
	/// <param name="candidatePosition">Absolute candidate position.</param>
	/// <param name="alignment">The alignment.</param>
	/// <returns>The point instance that represents the target position.</returns>
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
		var topLeft = cellTopLeft + (candidateColumnIndex * candidateSize, candidateRowIndex * candidateSize);
		return alignment switch
		{
			Alignment.Center => topLeft + (candidateSize / 2, candidateSize / 2),
			Alignment.TopLeft => topLeft,
			Alignment.TopRight => topLeft + (candidateSize, 0),
			Alignment.BottomLeft => topLeft + (0, candidateSize),
			Alignment.BottomRight => topLeft + (candidateSize, candidateSize),
			_ => throw new ArgumentOutOfRangeException(nameof(alignment))
		};
	}

	/// <inheritdoc cref="GetPointBetween(Absolute, Absolute)"/>
	public SKPoint GetPointBetween(Relative cell1, Relative cell2)
		=> GetPointBetween(GetAbsoluteIndex(cell1), GetAbsoluteIndex(cell2));

	/// <inheritdoc cref="GetPointBetweenWithAdjacentRelation(Absolute, Absolute, out Direction8)"/>
	public SKPoint GetPointBetween(Relative cell1, Relative cell2, out Direction8 adjacentRelation)
		=> GetPointBetweenWithAdjacentRelation(GetAbsoluteIndex(cell1), GetAbsoluteIndex(cell2), out adjacentRelation);

	/// <summary>
	/// Gets a point that is the center point of two cells; this method doesn't require two cells are adjacent with each other.
	/// </summary>
	/// <param name="cell1">The cell 1.</param>
	/// <param name="cell2">The cell 2.</param>
	/// <returns>The center point of two adjacent cells.</returns>
	public SKPoint GetPointBetween(Absolute cell1, Absolute cell2)
	{
		var p1 = GetPoint(cell1, Alignment.Center);
		var p2 = GetPoint(cell2, Alignment.Center);
		return p1 == p2 ? p1 : new((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
	}

	/// <summary>
	/// Gets a point that is the center point of two <b>adjacent</b> cells.
	/// </summary>
	/// <param name="cell1">The cell 1.</param>
	/// <param name="cell2">The cell 2.</param>
	/// <param name="adjacentRelation">The adjacent direction between two cells.</param>
	/// <returns>The center point of two adjacent cells.</returns>
	/// <exception cref="ArgumentException">Throws when the specified pair of cells are not adjacent with each other.</exception>
	public SKPoint GetPointBetweenWithAdjacentRelation(Absolute cell1, Absolute cell2, out Direction8 adjacentRelation)
	{
		if (Absolute.GetAdjacentRelation(cell1, cell2, this) is not (var direction and not Direction8.None))
		{
			const string errorInfo = $"The specified pair of cells '{nameof(cell1)}' and '{nameof(cell2)}' are not adjacent with each other.";
			throw new ArgumentException(errorInfo);
		}

#pragma warning disable IDE0055
		adjacentRelation = direction;
		return GetPoint(cell1, Alignment.Center) + adjacentRelation switch
		{
			Direction8.Up			=> (             0,   CellSize / 2),
			Direction8.Down			=> (             0, - CellSize / 2),
			Direction8.Left			=> (  CellSize / 2,              0),
			Direction8.Right		=> (- CellSize / 2,              0),
			Direction8.LeftUp		=> (  CellSize / 2,   CellSize / 2),
			Direction8.RightUp		=> (- CellSize / 2,   CellSize / 2),
			Direction8.LeftDown		=> (  CellSize / 2, - CellSize / 2),
			Direction8.RightDown	=> (- CellSize / 2, - CellSize / 2),
			_ => throw new UnreachableException()
		};
#pragma warning restore IDE0055
	}

	/// <summary>
	/// Creates a new <see cref="PointMapper"/> instance via the specified offset, replacing with new value.
	/// </summary>
	/// <param name="vector">The direction vector as offset.</param>
	/// <returns>The result <see cref="PointMapper"/> instance.</returns>
	public PointMapper WithOffset(Thickness<Relative> vector) => this with { TemplateSize = TemplateSize with { Vector = vector } };

	/// <summary>
	/// Creates a new <see cref="PointMapper"/> instance via the specified offset, adding to original template size direction vector.
	/// </summary>
	/// <param name="vector">The direction vector as offset.</param>
	/// <returns>The result <see cref="PointMapper"/> instance.</returns>
	public PointMapper AddOffset(Thickness<Relative> vector)
		=> this with { TemplateSize = TemplateSize with { Vector = TemplateSize.Vector + vector } };

	private bool PrintMembers(StringBuilder builder)
	{
		builder.Append($"{nameof(CellSize)} = {CellSize:0.0###}, ");
		builder.Append($"{nameof(Margin)} = {Margin:0.0###}, ");
		builder.Append($"{nameof(TemplateSize)} = {TemplateSize}");
		return true;
	}
}
