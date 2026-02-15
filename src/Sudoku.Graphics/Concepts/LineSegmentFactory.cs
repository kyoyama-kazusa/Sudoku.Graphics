namespace Sudoku.Concepts;

/// <summary>
/// Provides an easy way to create <see cref="LineSegment"/> instances.
/// </summary>
/// <seealso cref="LineSegment"/>
public static class LineSegmentFactory
{
	/// <summary>
	/// Returns a list of <see cref="LineSegment"/> instances indicating light-up segments to be shown,
	/// forming a outline cage that includes all the specified cells.
	/// </summary>
	/// <param name="cellIndices">The cell indices.</param>
	/// <param name="mapper">The mapper.</param>
	/// <returns>A list of <see cref="LineSegment"/> instances.</returns>
	public static LineSegment[] GetOutlineSegments(Absolute[] cellIndices, PointMapper mapper)
	{
		var dictionary = GetLightupDirections(cellIndices, false, mapper, out _);
		var result = new LineSegment[dictionary.Count];
		var i = 0;
		foreach (var (cellIndex, directions) in dictionary)
		{
			result[i++] = new(cellIndex, directions);
		}
		return result;
	}

	/// <summary>
	/// Creates a <see cref="Dictionary{TKey, TValue}"/> of <see cref="Absolute"/> and <see cref="Direction"/> key-value pairs,
	/// indicating lightup segments of cells to be shown.
	/// </summary>
	/// <param name="cellIndices">The cell indices.</param>
	/// <param name="isCyclicRuleChecked">
	/// A <see cref="bool"/> value indicating cycling row and column gaps will be considered as connected.
	/// </param>
	/// <param name="mapper">The mapper.</param>
	/// <param name="absoluteCellIndices">Absolute cell indices.</param>
	/// <returns>The result dictionary of light-up segments.</returns>
	[SuppressMessage("Style", "IDE0028:Simplify collection initialization", Justification = "<Pending>")]
	internal static Dictionary<Absolute, Direction> GetLightupDirections(
		Relative[] cellIndices,
		bool isCyclicRuleChecked,
		PointMapper mapper,
		out HashSet<Absolute> absoluteCellIndices
	) => GetLightupDirectionsCore(
		new(
			from cell in cellIndices
			let absoluteIndex = mapper.ToAbsoluteIndex(cell)
			select KeyValuePair.Create(absoluteIndex, Direction.Up | Direction.Down | Direction.Left | Direction.Right)
		),
		isCyclicRuleChecked,
		mapper,
		out absoluteCellIndices
	);

	/// <inheritdoc cref="GetLightupDirections(Relative[], bool, PointMapper, out HashSet{Absolute})"/>
	[SuppressMessage("Style", "IDE0028:Simplify collection initialization", Justification = "<Pending>")]
	internal static Dictionary<Absolute, Direction> GetLightupDirections(
		Absolute[] cellIndices,
		bool isCyclicRuleChecked,
		PointMapper mapper,
		out HashSet<Absolute> absoluteCellIndices
	) => GetLightupDirectionsCore(
		new(
			from cell in cellIndices
			select KeyValuePair.Create(cell, Direction.Up | Direction.Down | Direction.Left | Direction.Right)
		),
		isCyclicRuleChecked,
		mapper,
		out absoluteCellIndices
	);

	/// <summary>
	/// Creates a <see cref="Dictionary{TKey, TValue}"/> of <see cref="Absolute"/> and <see cref="Direction"/> key-value pairs,
	/// indicating lightup segments of cells to be shown.
	/// </summary>
	/// <param name="lineSegmentsDictionary">The original entry dictionary.</param>
	/// <param name="isCyclicRuleChecked">
	/// A <see cref="bool"/> value indicating cycling row and column gaps will be considered as connected.
	/// </param>
	/// <param name="mapper">The mapper.</param>
	/// <param name="absoluteCellIndices">Absolute cell indices.</param>
	/// <returns>The result dictionary of light-up segments.</returns>
	private static Dictionary<Absolute, Direction> GetLightupDirectionsCore(
		Dictionary<Absolute, Direction> lineSegmentsDictionary,
		bool isCyclicRuleChecked,
		PointMapper mapper,
		out HashSet<Absolute> absoluteCellIndices
	)
	{
		absoluteCellIndices = [.. lineSegmentsDictionary.Keys];

		// Iterate on each cell (absolute), to find for adjacent cells.
		foreach (var cell in lineSegmentsDictionary.Keys)
		{
			foreach (var direction in Direction.AllDirections)
			{
				if (absoluteCellIndices.Contains(mapper.GetAdjacentAbsoluteCellWith(cell, direction, isCyclicRuleChecked)))
				{
					// This direction contains that cell - we should remove this direction.
					lineSegmentsDictionary[cell] &= ~direction;
				}
			}
		}
		return lineSegmentsDictionary;
	}
}
