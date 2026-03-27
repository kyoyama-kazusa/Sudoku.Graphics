namespace Sudoku.Graphics;

/// <summary>
/// Represents an <see cref="int"/> value that describes an absolute index.
/// </summary>
/// <param name="value">The value.</param>
[JsonConverter(typeof(ValueConverter<Absolute>))]
[DebuggerDisplay($$"""{{{nameof(ToString)}}(),nq}""")]
[SuppressMessage("Usage", "CA2231:Overload operator equals on overriding value type Equals", Justification = "<Pending>")]
public readonly struct Absolute(int value) : IInteger<Absolute>, ILocator<Absolute>
{
	/// <summary>
	/// The backing value.
	/// </summary>
	private readonly int _value = value;


	/// <inheritdoc/>
	int IInteger<Absolute>.Value => _value;

	/// <inheritdoc/>
	[UnscopedRef]
	ref readonly int IInteger<Absolute>.ValueRef => ref _value;


	/// <inheritdoc/>
	public override bool Equals([NotNullWhen(true)] object? obj) => obj is Absolute comparer && Equals(comparer);

	/// <inheritdoc/>
	public bool Equals(Absolute other) => _value == other._value;

	/// <inheritdoc cref="object.GetHashCode"/>
	public override int GetHashCode() => _value;

	/// <inheritdoc/>
	public int CompareTo(Absolute other) => _value.CompareTo(other._value);

	/// <inheritdoc cref="object.ToString"/>
	public override string ToString() => _value.ToString();


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
		var columnsCount = mapper.ColumnsCount;
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
	public static implicit operator Absolute(int value) => new(value);

	/// <inheritdoc/>
	public static implicit operator int(Absolute value) => value._value;
}
