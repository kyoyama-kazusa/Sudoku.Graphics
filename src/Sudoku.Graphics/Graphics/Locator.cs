namespace Sudoku.Graphics;

/// <summary>
/// Represents a locator object (absolute cell index, relative cell index or candidate position).
/// </summary>
public readonly union Locator(Absolute, Relative, CandidatePosition) : ILocator<Locator>
{
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

	/// <inheritdoc/>
	public override int GetHashCode()
		=> this switch
		{
			Absolute a => a.GetHashCode(),
			Relative r => r.GetHashCode(),
			CandidatePosition c => c.GetHashCode(),
			null => 0
		};

	/// <inheritdoc/>
	public float GetLocatorMeasurer(float cellSize)
		=> this switch
		{
			Absolute a => a.GetLocatorMeasurer(cellSize),
			Relative r => r.GetLocatorMeasurer(cellSize),
			CandidatePosition c => c.GetLocatorMeasurer(cellSize),
			null => 0
		};

	/// <inheritdoc/>
	/// <remarks>
	/// <para>
	/// By design of candidate drawing system, we will split a cell into a square subgrid of size <i>n</i> by <i>n</i>,
	/// where <i>n</i> is equal to value of property <see cref="CandidatePosition.SubgridSize"/>.
	/// </para>
	/// <para>
	/// Then, we define an absolute internal index to describe a cell will be drawn, which is in range [0, <i>n</i> * <i>n</i>) -
	/// property <see cref="CandidatePosition.InnerIndex"/>.
	/// </para>
	/// </remarks>
	/// <seealso cref="CandidatePosition.SubgridSize"/>
	/// <seealso cref="CandidatePosition.InnerIndex"/>
	public Relative GetCandidatesCountInEachRow()
		=> this switch
		{
			Absolute a => a.GetCandidatesCountInEachRow(),
			Relative r => r.GetCandidatesCountInEachRow(),
			CandidatePosition c => c.GetCandidatesCountInEachRow(),
			null => 0
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
