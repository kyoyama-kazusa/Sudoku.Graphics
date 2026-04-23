namespace Sudoku.Graphics;

/// <summary>
/// Represents an <see cref="int"/> value that describes a relative index.
/// </summary>
/// <param name="value">The value.</param>
[JsonConverter(typeof(IInteger<Relative>.Converter))]
[DebuggerDisplay($$"""{{{nameof(ToString)}}(),nq}""")]
[SuppressMessage("Usage", "CA2231:Overload operator equals on overriding value type Equals", Justification = "<Pending>")]
public readonly struct Relative(int value) : IInteger<Relative>, ILocator<Relative>
{
	/// <summary>
	/// The backing value.
	/// </summary>
	private readonly int _value = value;


	/// <inheritdoc/>
	public bool IsInvalid => _value < 0;

	/// <inheritdoc/>
	int IInteger<Relative>.Value => _value;


	/// <inheritdoc/>
	public override bool Equals([NotNullWhen(true)] object? obj) => obj is Relative comparer && Equals(comparer);

	/// <inheritdoc/>
	public bool Equals(Relative other) => _value == other._value;

	/// <inheritdoc/>
	public bool IsSideWith(Relative other, Direction4 direction, PointMapper mapper, bool isInStrictDirection)
	{
		var a = ToAbsolute(mapper);
		var b = other.ToAbsolute(mapper);
		return a.IsSideWith(b, direction, mapper, isInStrictDirection);
	}

	/// <inheritdoc/>
	public float GetLocatorMeasurer(float cellSize) => cellSize;

	/// <inheritdoc cref="object.GetHashCode"/>
	public override int GetHashCode() => _value;

	/// <inheritdoc/>
	public int CompareTo(Relative other) => _value.CompareTo(other._value);

	/// <inheritdoc cref="object.ToString"/>
	public override string ToString() => _value.ToString();

	/// <summary>
	/// Projects the current relative cell index into absolute one.
	/// </summary>
	/// <param name="mapper">The point mapper instance.</param>
	/// <returns>The result absolute index.</returns>
	public Absolute ToAbsolute(PointMapper mapper)
	{
		var row = this / mapper.ColumnsCount;
		var column = this % mapper.ColumnsCount;
		var resultRow = row + mapper.Vector.Top;
		var resultColumn = column + mapper.Vector.Left;
		return resultRow * mapper.AbsoluteColumnsCount + resultColumn;
	}

	/// <summary>
	/// Projects the specified relative cell index into absolute one;
	/// with the specified direction as outside offset one, and an offset value <paramref name="offset"/>
	/// indicating the number of advanced steps of cells.
	/// </summary>
	/// <param name="outsideDirection">The outside direction.</param>
	/// <param name="offset">The offset. For negative values, it'll negate <paramref name="outsideDirection"/> also.</param>
	/// <param name="mapper">The point mapper instance.</param>
	/// <returns>The result absolute index.</returns>
	/// <exception cref="ArgumentException">Throws when <paramref name="outsideDirection"/> is not a flag.</exception>
	/// <exception cref="ArgumentOutOfRangeException">Throws when <paramref name="outsideDirection"/> is not defined.</exception>
	public Absolute ToAbsolute(Direction4 outsideDirection, Absolute offset, PointMapper mapper)
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
		return ToAbsolute(mapper) + outsideDirection switch
		{
			Direction4.Up => -(mapper.AbsoluteColumnsCount * offset),
			Direction4.Down => +(mapper.AbsoluteColumnsCount * offset),
			Direction4.Left => -offset,
			Direction4.Right => +offset,
			_ => throw new UnreachableException()
		};
	}


	/// <inheritdoc/>
	public static bool IsAlignedAs(LocatorGridAlignment gridAlignment, Relative first, Relative second, PointMapper mapper)
		=> Absolute.IsAlignedAs(gridAlignment, first.ToAbsolute(mapper), second.ToAbsolute(mapper), mapper);


	/// <inheritdoc/>
	public static implicit operator Relative(int value) => new(value);

	/// <inheritdoc/>
	public static implicit operator int(Relative value) => value._value;

	/// <summary>
	/// Explicit cast from <see cref="Absolute"/> to <see cref="Relative"/> value.
	/// </summary>
	/// <param name="value">The value.</param>
	public static explicit operator Relative(Absolute value) => (int)value;

	/// <summary>
	/// Explicit cast from <see cref="Relative"/> to <see cref="Absolute"/> value.
	/// </summary>
	/// <param name="value">The value.</param>
	public static explicit operator Absolute(Relative value) => (int)value;
}
