namespace Sudoku.Graphics;

/// <summary>
/// Represents an <see cref="int"/> value that describes an absolute index.
/// </summary>
/// <param name="value">The value.</param>
[JsonConverter(typeof(IInteger<Absolute>.Converter))]
[DebuggerDisplay($$"""{{{nameof(ToString)}}(),nq}""")]
[SuppressMessage("Usage", "CA2231:Overload operator equals on overriding value type Equals", Justification = "<Pending>")]
public readonly struct Absolute(int value) : IInteger<Absolute>, ILocator<Absolute>
{
	/// <summary>
	/// The backing value.
	/// </summary>
	private readonly int _value = value;


	/// <inheritdoc/>
	public bool IsInvalid => _value < 0;

	/// <inheritdoc/>
	int IInteger<Absolute>.Value => _value;


	/// <inheritdoc/>
	public override bool Equals([NotNullWhen(true)] object? obj) => obj is Absolute comparer && Equals(comparer);

	/// <inheritdoc/>
	public bool Equals(Absolute other) => _value == other._value;

	/// <inheritdoc/>
	public bool IsSideWith(Absolute other, Direction4 direction, PointMapper mapper, bool isInStrictDirection)
	{
		var columnsCount = mapper.AbsoluteColumnsCount;
		var (row1, column1) = (this / columnsCount, this % columnsCount);
		var (row2, column2) = (other / columnsCount, other % columnsCount);
		return (isInStrictDirection, direction) switch
		{
			(true, Direction4.Up) => row1 < row2 && column1 == column2,
			(_, Direction4.Up) => this < other,
			(true, Direction4.Down) => row1 > row2 && column1 == column2,
			(_, Direction4.Down) => row1 > row2 && column1 == column2,
			(true, Direction4.Left) => column1 < column2 && row1 == row2,
			(true, Direction4.Right) => column1 > column2 && row1 == row2,
			(_, Direction4.Left) => throw new NotSupportedException("This type of case cannot be well-defined."),
			(_, Direction4.Right) => throw new NotSupportedException("This type of case cannot be well-defined."),
			_ => throw new ArgumentOutOfRangeException(nameof(direction))
		};
	}

	/// <summary>
	/// Calculates whether the specified cell is lying in the specified orientation with the specified cell.
	/// </summary>
	/// <param name="other">The other cell.</param>
	/// <param name="orientation">The orientation.</param>
	/// <param name="mapper">The mapper.</param>
	/// <returns>A <see cref="bool"/> result indicating that.</returns>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Throws when <paramref name="orientation"/> is not defined or <see cref="Orientation4.None"/>.
	/// </exception>
	/// <seealso cref="Orientation4.None"/>
	public bool IsOrientationWith(Absolute other, Orientation4 orientation, PointMapper mapper)
	{
		var columnsCount = mapper.AbsoluteColumnsCount;
		var (row1, column1) = (this / columnsCount, this % columnsCount);
		var (row2, column2) = (other / columnsCount, other % columnsCount);
		return orientation switch
		{
			Orientation4.Horizontal => row1 == row2 && column1 == column2 - 1,
			Orientation4.Vertical => row1 == row2 - 1 && column1 == column2,
			Orientation4.Slash => row1 == row2 - 1 && column1 == column2 + 1,
			Orientation4.Backslash => row1 == row2 - 1 && column1 == column2 - 1,
			_ => throw new ArgumentOutOfRangeException(nameof(orientation))
		};
	}

	/// <inheritdoc/>
	public float GetLocatorMeasurer(float cellSize) => cellSize;

	/// <inheritdoc cref="object.GetHashCode"/>
	public override int GetHashCode() => _value;

	/// <inheritdoc/>
	public int CompareTo(Absolute other) => _value.CompareTo(other._value);

	/// <inheritdoc cref="object.ToString"/>
	public override string ToString() => _value.ToString();

	/// <summary>
	/// Projects the specified absolute cell index into relative one.
	/// </summary>
	/// <param name="mapper">The point mapper instance.</param>
	/// <returns>The result relative index.</returns>
	public Relative ToRelative(PointMapper mapper)
	{
		var absoluteColumnsCount = mapper.AbsoluteColumnsCount;
		var row = this / absoluteColumnsCount;
		var column = this % absoluteColumnsCount;
		var resultRow = row - mapper.Vector.Top;
		var resultColumn = column - mapper.Vector.Left;
		return resultRow * mapper.ColumnsCount + resultColumn;
	}

	/// <summary>
	/// Gets the adjacent cell at the specified direction of the current absolute cell index.
	/// </summary>
	/// <param name="direction">The direction.</param>
	/// <param name="isCyclicChecking">
	/// Indicates whether the cell overflown in the relative grid will be included to be checked or not.
	/// </param>
	/// <param name="mapper">The point mapper instance.</param>
	/// <returns>Target cell absolute index.</returns>
	public Absolute GetAdjacentAbsoluteIn(Direction4 direction, bool isCyclicChecking, PointMapper mapper)
	{
		var rowsCount = mapper.AbsoluteRowsCount;
		var columnsCount = mapper.AbsoluteColumnsCount;
		var row = this / columnsCount;
		var column = this % columnsCount;
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

	/// <inheritdoc/>
	public Relative GetCandidatesCountInEachRow() => (double)_value >> Math.Sqrt >> Math.Ceiling >> Convert.ToInt32;


	/// <inheritdoc/>
	public static bool IsAlignedAs(LocatorGridAlignment gridAlignment, Absolute first, Absolute second, PointMapper mapper)
	{
		var (rowsCount, columnsCount) = (mapper.AbsoluteRowsCount, mapper.AbsoluteColumnsCount);
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

	/// <summary>
	/// Determine whether two <see cref="Absolute"/> cells are in one line (row or column).
	/// </summary>
	/// <param name="left">The left instance.</param>
	/// <param name="right">The right instance.</param>
	/// <param name="mapper">The mapper instance.</param>
	/// <param name="houseType">The house type.</param>
	/// <returns>A <see cref="bool"/> result.</returns>
	public static bool IsInOneLine(Absolute left, Absolute right, PointMapper mapper, out HouseType houseType)
	{
		var columnsCount = mapper.ColumnsCount;
		var leftRow = left / columnsCount;
		var leftColumn = left % columnsCount;
		var rightRow = right / columnsCount;
		var rightColumn = right % columnsCount;
		if (leftRow == rightRow)
		{
			houseType = HouseType.Row;
			return true;
		}
		if (leftColumn == rightColumn)
		{
			houseType = HouseType.Column;
			return true;
		}
		houseType = HouseType.Unknown;
		return false;
	}

	/// <summary>
	/// Determine whether two <see cref="Absolute"/> cells are adjacent with each other.
	/// </summary>
	/// <param name="left">The left instance.</param>
	/// <param name="right">The right instance.</param>
	/// <param name="mapper">The mapper instance.</param>
	/// <param name="houseType">The house type.</param>
	/// <returns>A <see cref="bool"/> result.</returns>
	public static bool IsAdjacent(Absolute left, Absolute right, PointMapper mapper, out HouseType houseType)
	{
		var columnsCount = mapper.ColumnsCount;
		var leftRow = left / columnsCount;
		var leftColumn = left % columnsCount;
		var rightRow = right / columnsCount;
		var rightColumn = right % columnsCount;
		(houseType, var result) = (Math.Abs(leftRow - rightRow), Math.Abs(leftColumn - rightColumn)) switch
		{
			(0, 1) => (HouseType.Row, true),
			(1, 0) => (HouseType.Column, true),
			_ => (HouseType.Unknown, false)
		};
		return result;
	}

	/// <summary>
	/// Gets the detailed adjacent relations between two cells specified,
	/// and return the relative direction of <paramref name="left"/>.
	/// If there's no adjacent relation between two cells, <see cref="Direction8.None"/> will be returned.
	/// </summary>
	/// <param name="left">The left instance.</param>
	/// <param name="right">The right instance.</param>
	/// <param name="mapper">The point mapper instance.</param>
	/// <returns>The direction instance.</returns>
	public static Direction8 GetAdjacentRelation(Absolute left, Absolute right, PointMapper mapper)
	{
		var columnsCount = mapper.AbsoluteColumnsCount;
		var leftRow = left / columnsCount;
		var leftColumn = left % columnsCount;
		var rightRow = right / columnsCount;
		var rightColumn = right % columnsCount;
		return (leftRow - rightRow, leftColumn - rightColumn) switch
		{
			(-1, -1) => Direction8.LeftUp,
			(-1, 0) => Direction8.Up,
			(-1, 1) => Direction8.RightUp,
			(0, -1) => Direction8.Left,
			(0, 1) => Direction8.Right,
			(1, -1) => Direction8.LeftDown,
			(1, 0) => Direction8.Down,
			(1, 1) => Direction8.RightDown,
			_ => Direction8.None
		};
	}


	/// <inheritdoc/>
	public static implicit operator int(Absolute value) => value._value;

	/// <inheritdoc/>
	public static implicit operator Absolute(int value) => new(value);
}
