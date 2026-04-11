namespace Sudoku.Graphics.Templating;

/// <summary>
/// Provides an easy way to create <see cref="LineSegment"/> instances.
/// </summary>
/// <seealso cref="LineSegment"/>
public static class LineSegmentFactory
{
	/// <summary>
	/// Returns a list of <see cref="LineSegment"/> instances indicating light-up segments to be shown,
	/// including all cell borders (but not outline).
	/// </summary>
	/// <param name="cells">The cells.</param>
	/// <param name="mapper">The mapper.</param>
	/// <returns>A list of <see cref="LineSegment"/> instances.</returns>
	public static LineSegment[] GetInline(Absolute[] cells, PointMapper mapper)
	{
		var result = new List<LineSegment>();
		result.AddRange(
			from cell in cells
			select new LineSegment(cell, Direction4.Up | Direction4.Down | Direction4.Left | Direction4.Right)
		);
		var outlines = GetOutline(cells, mapper).ToDictionary(static kvp => kvp.CellIndex, static kvp => kvp.Directions);
		foreach (ref var cellDirections in CollectionsMarshal.AsSpan(result))
		{
			var cell = cellDirections.CellIndex;
			cellDirections = new(cell, cellDirections.Directions & ~outlines[cell]);
		}
		return [.. result];
	}

	/// <summary>
	/// Returns a list of <see cref="LineSegment"/> instances indicating light-up segments to be shown,
	/// forming a outline cage that includes all the specified cells.
	/// </summary>
	/// <param name="cells">The cell indices.</param>
	/// <param name="mapper">The mapper.</param>
	/// <returns>A list of <see cref="LineSegment"/> instances.</returns>
	public static LineSegment[] GetOutline(Absolute[] cells, PointMapper mapper)
	{
		var dictionary = GetLightupDirections(cells, false, mapper, out _);
		var result = new LineSegment[dictionary.Count];
		var i = 0;
		foreach (var (cellIndex, directions) in dictionary)
		{
			result[i++] = new(cellIndex, directions);
		}
		return result;
	}

	/// <summary>
	/// Gets except range of the specified segments.
	/// </summary>
	/// <param name="originalSegments">The original segments.</param>
	/// <param name="mapper">The mapper.</param>
	/// <returns>Except range of line segments.</returns>
	public static LineSegment[] GetExceptRange(LineSegment[] originalSegments, PointMapper mapper)
	{
		var result = new Dictionary<Absolute, LineSegment>();
		for (var i = 0; i < mapper.AbsoluteRowsCount; i++)
		{
			for (var j = 0; j < mapper.AbsoluteColumnsCount; j++)
			{
				var cellIndex = i * mapper.AbsoluteColumnsCount + j;
				result.Add(cellIndex, new(cellIndex, Direction4.Up | Direction4.Down | Direction4.Left | Direction4.Right));
			}
		}

		foreach (var (cellIndex, directions) in originalSegments)
		{
			if (result.TryGetValue(cellIndex, out var value) && value.Directions is var targetDirections)
			{
				targetDirections &= ~directions;
				result[cellIndex] = new(cellIndex, targetDirections);
			}
		}

		return [.. result.Values];
	}

	/// <summary>
	/// Creates a <see cref="Dictionary{TKey, TValue}"/> of <see cref="Absolute"/> and <see cref="Direction4"/> key-value pairs,
	/// indicating lightup segments of cells to be shown.
	/// </summary>
	/// <param name="cells">The cell indices.</param>
	/// <param name="isCyclicRuleChecked">
	/// A <see cref="bool"/> value indicating cycling row and column gaps will be considered as connected.
	/// </param>
	/// <param name="mapper">The mapper.</param>
	/// <param name="absoluteCellIndices">Absolute cell indices.</param>
	/// <returns>The result dictionary of light-up segments.</returns>
	internal static Dictionary<Absolute, Direction4> GetLightupDirections(
		Relative[] cells,
		bool isCyclicRuleChecked,
		PointMapper mapper,
		out HashSet<Absolute> absoluteCellIndices
	) => GetLightupDirectionsCore(
		new([
			..
			from cell in cells
			let absoluteIndex = cell.ToAbsolute(mapper)
			select KeyValuePair.Create(absoluteIndex, Direction4.Up | Direction4.Down | Direction4.Left | Direction4.Right)
		]),
		isCyclicRuleChecked,
		mapper,
		out absoluteCellIndices
	);

	/// <inheritdoc cref="GetLightupDirections(Relative[], bool, PointMapper, out HashSet{Absolute})"/>
	internal static Dictionary<Absolute, Direction4> GetLightupDirections(
		Absolute[] cells,
		bool isCyclicRuleChecked,
		PointMapper mapper,
		out HashSet<Absolute> absoluteCellIndices
	) => GetLightupDirectionsCore(
		new([
			..
			from cell in cells
			select KeyValuePair.Create(cell, Direction4.Up | Direction4.Down | Direction4.Left | Direction4.Right)
		]),
		isCyclicRuleChecked,
		mapper,
		out absoluteCellIndices
	);

	/// <summary>
	/// Creates a <see cref="Dictionary{TKey, TValue}"/> of <see cref="Absolute"/> and <see cref="Direction4"/> key-value pairs,
	/// indicating lightup segments of cells to be shown.
	/// </summary>
	/// <param name="lineSegmentsDictionary">The original entry dictionary.</param>
	/// <param name="isCyclicRuleChecked">
	/// A <see cref="bool"/> value indicating cycling row and column gaps will be considered as connected.
	/// </param>
	/// <param name="mapper">The mapper.</param>
	/// <param name="cells">Absolute cell indices.</param>
	/// <returns>The result dictionary of light-up segments.</returns>
	private static Dictionary<Absolute, Direction4> GetLightupDirectionsCore(
		Dictionary<Absolute, Direction4> lineSegmentsDictionary,
		bool isCyclicRuleChecked,
		PointMapper mapper,
		out HashSet<Absolute> cells
	)
	{
		cells = [.. lineSegmentsDictionary.Keys];

		// Iterate on each cell (absolute), to find for adjacent cells.
		foreach (var cell in lineSegmentsDictionary.Keys)
		{
			foreach (var direction in Direction4.AllDirections)
			{
				if (cells.Contains(cell.GetAdjacentAbsoluteIn(direction, isCyclicRuleChecked, mapper)))
				{
					// This direction contains that cell - we should remove this direction.
					lineSegmentsDictionary[cell] &= ~direction;
				}
			}
		}
		return lineSegmentsDictionary;
	}
}
