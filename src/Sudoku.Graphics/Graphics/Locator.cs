namespace Sudoku.Graphics;

/// <summary>
/// Represents a locator object (absolute cell index, relative cell index or candidate position).
/// </summary>
[Union]
[StructLayout(LayoutKind.Explicit)]
public readonly struct Locator : ILocator<Locator>, IUnion
{
	/// <summary>
	/// The type of backing value (0 = <see cref="Absolute"/>, 1 = <see cref="Relative"/>, 2 = <see cref="CandidatePosition"/>).
	/// </summary>
	[FieldOffset(0)]
	private readonly int _type;

	/// <summary>
	/// The backing value of type <see cref="Absolute"/>.
	/// </summary>
	[FieldOffset(4)]
	private readonly Absolute _absolute;

	/// <summary>
	/// The backing value of type <see cref="Relative"/>.
	/// </summary>
	[FieldOffset(4)]
	private readonly Relative _relative;

	/// <summary>
	/// The backing value of type <see cref="CandidatePosition"/>.
	/// </summary>
	[FieldOffset(4)]
	private readonly CandidatePosition _candidate;


	/// <summary>
	/// Creates a <see cref="Locator"/> object via <see cref="Absolute"/> instance.
	/// </summary>
	/// <param name="absolute">The instance.</param>
	public Locator(Absolute absolute)
	{
		_absolute = absolute;
		_type = 0;
	}

	/// <summary>
	/// Creates a <see cref="Locator"/> object via <see cref="Relative"/> instance.
	/// </summary>
	/// <param name="relative">The instance.</param>
	public Locator(Relative relative)
	{
		_relative = relative;
		_type = 1;
	}

	/// <summary>
	/// Creates a <see cref="Locator"/> object via <see cref="CandidatePosition"/> instance.
	/// </summary>
	/// <param name="candidate">The instance.</param>
	public Locator(CandidatePosition candidate)
	{
		_candidate = candidate;
		_type = 2;
	}


	/// <inheritdoc/>
	public object Value => _type switch { 0 => _absolute, 1 => _relative, _ => _candidate };


	/// <inheritdoc/>
	public override bool Equals([NotNullWhen(true)] object? obj)
		=> obj switch
		{
			Locator l => Equals(l),
			Absolute a => Equals(a),
			Relative r => Equals(r),
			CandidatePosition c => Equals(c),
			_ => false
		};

	/// <inheritdoc/>
	public bool Equals(Locator other)
		=> (this, other) switch
		{
			(Absolute a, Absolute b) => a == b,
			(Relative a, Relative b) => a == b,
			(CandidatePosition a, CandidatePosition b) => a == b,
			_ => false
		};

	/// <inheritdoc/>
	public bool IsSideWith(Locator other, Direction4 direction, PointMapper mapper, bool isInStrictDirection)
		=> (this, other) switch
		{
			(Absolute a, Absolute b) => a.IsSideWith(b, direction, mapper, isInStrictDirection),
			(Relative a, Relative b) => a.IsSideWith(b, direction, mapper, isInStrictDirection),
			(CandidatePosition a, CandidatePosition b) => a.IsSideWith(b, direction, mapper, isInStrictDirection),
			_ => throw new NotSupportedException($"Type mismatches - parameter '{nameof(other)}' should hold same type with the current instance.")
		};

	/// <summary>
	/// Try to get the backing value of type <see cref="Absolute"/> if available.
	/// </summary>
	/// <param name="result">The value.</param>
	/// <returns>A <see cref="bool"/> result indicating whether the type matches.</returns>
	public bool TryGetValue(out Absolute result)
	{
		if (_type == 0)
		{
			result = _absolute;
			return true;
		}
		result = default;
		return false;
	}

	/// <summary>
	/// Try to get the backing value of type <see cref="Relative"/> if available.
	/// </summary>
	/// <param name="result">The value.</param>
	/// <returns>A <see cref="bool"/> result indicating whether the type matches.</returns>
	public bool TryGetValue(out Relative result)
	{
		if (_type == 1)
		{
			result = _relative;
			return true;
		}
		result = default;
		return false;
	}

	/// <summary>
	/// Try to get the backing value of type <see cref="CandidatePosition"/> if available.
	/// </summary>
	/// <param name="result">The value.</param>
	/// <returns>A <see cref="bool"/> result indicating whether the type matches.</returns>
	public bool TryGetValue(out CandidatePosition result)
	{
		if (_type == 2)
		{
			result = _candidate;
			return true;
		}
		result = CandidatePosition.Invalid;
		return false;
	}

	/// <inheritdoc/>
	public override int GetHashCode()
		=> this switch
		{
			Absolute a => a.GetHashCode(),
			Relative r => r.GetHashCode(),
			CandidatePosition c => c.GetHashCode()
		};

	/// <inheritdoc/>
	public float GetLocatorMeasurer(float cellSize)
		=> this switch
		{
			Absolute a => a.GetLocatorMeasurer(cellSize),
			Relative r => r.GetLocatorMeasurer(cellSize),
			CandidatePosition c => c.GetLocatorMeasurer(cellSize)
		};

	/// <inheritdoc/>
	public Relative GetCandidatesCountInEachRow()
		=> this switch
		{
			Absolute a => a.GetCandidatesCountInEachRow(),
			Relative r => r.GetCandidatesCountInEachRow(),
			CandidatePosition c => c.GetCandidatesCountInEachRow()
		};


	/// <inheritdoc/>
	public static bool IsAlignedAs(LocatorGridAlignment gridAlignment, Locator first, Locator second, PointMapper mapper)
		=> (first, second) switch
		{
			(Absolute a, Absolute b) => Absolute.IsAlignedAs(gridAlignment, a, b, mapper),
			(Relative a, Relative b) => Relative.IsAlignedAs(gridAlignment, a, b, mapper),
			(CandidatePosition a, CandidatePosition b) => CandidatePosition.IsAlignedAs(gridAlignment, a, b, mapper),
			_ => throw new NotSupportedException($"Type mismatches - parameters '{nameof(first)}' and '{nameof(second)}' should hold same type.")
		};


	/// <inheritdoc/>
	public static bool operator ==(Locator left, Locator right) => left.Equals(right);

	/// <inheritdoc/>
	public static bool operator !=(Locator left, Locator right) => !(left == right);
}
