namespace Sudoku.ComponentModel.GridTemplates;

/// <summary>
/// Represents a grid template that specifies a list of thick and thin lines.
/// </summary>
public class SpecifiedTemplate : Template
{
	/// <summary>
	/// Initializes a <see cref="SpecifiedTemplate"/> instance, via the mapper.
	/// </summary>
	/// <param name="mapper">The mapper instance.</param>
	[JsonConstructor]
	[SetsRequiredMembers]
	public SpecifiedTemplate(PointMapper mapper) => Mapper = mapper;


	/// <summary>
	/// Indicates thick line segments. By default it's an empty array.
	/// </summary>
	public LineSegment[] ThickLineSegments { get; init; } = [];

	/// <summary>
	/// Indicates thin line segments. By default it's an empty array.
	/// </summary>
	public LineSegment[] ThinLineSegments { get; init; } = [];


	/// <inheritdoc/>
	protected sealed override void GuardStatements(SKCanvas canvas)
	{
		var maxCellIndex = Mapper.AbsoluteRowsCount * Mapper.AbsoluteColumnsCount;
		foreach (var (cellIndex, directions) in ThickLineSegments)
		{
			checkRange(cellIndex, maxCellIndex);
			checkDirections(directions);
		}
		foreach (var (cellIndex, directions) in ThinLineSegments)
		{
			checkRange(cellIndex, maxCellIndex);
			checkDirections(directions);
		}


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		static void checkRange(Absolute cellIndex, Absolute maxCellIndex)
		{
			if (cellIndex < 0 || cellIndex >= maxCellIndex)
			{
				throw new ArgumentException($"Invalid cell index. Expected cell range is between 0 and {maxCellIndex}.");
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		static void checkDirections(Direction directions)
		{
			if (directions < 0 || directions > (Direction.Up | Direction.Down | Direction.Left | Direction.Right))
			{
				throw new ArgumentException($"Invalid direction '{directions}'.");
			}
		}
	}

	/// <inheritdoc/>
	protected sealed override void DrawBorderRectangle(SKCanvas canvas)
	{
	}

	/// <inheritdoc/>
	protected sealed override void DrawGridLines(SKCanvas canvas)
	{
		using var thickLinesPaint = CreateThickLinesPaint();
		using var thinLinesPaint = CreateThinLinesPaint();
		foreach (var (cellIndex, directions) in ThickLineSegments)
		{
			drawLine(
				Mapper.GetPoint(cellIndex, Alignment.TopLeft),
				Mapper.GetPoint(cellIndex, Alignment.TopRight),
				Mapper.GetPoint(cellIndex, Alignment.BottomLeft),
				Mapper.GetPoint(cellIndex, Alignment.BottomRight),
				directions,
				thickLinesPaint
			);
		}
		foreach (var (cellIndex, directions) in ThinLineSegments)
		{
			drawLine(
				Mapper.GetPoint(cellIndex, Alignment.TopLeft),
				Mapper.GetPoint(cellIndex, Alignment.TopRight),
				Mapper.GetPoint(cellIndex, Alignment.BottomLeft),
				Mapper.GetPoint(cellIndex, Alignment.BottomRight),
				directions,
				thinLinesPaint
			);
		}


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void drawLine(SKPoint topLeft, SKPoint topRight, SKPoint bottomLeft, SKPoint bottomRight, Direction directions, SKPaint paint)
		{
			if (directions.HasFlag(Direction.Up))
			{
				canvas.DrawLine(topLeft, topRight, paint);
			}
			if (directions.HasFlag(Direction.Down))
			{
				canvas.DrawLine(bottomLeft, bottomRight, paint);
			}
			if (directions.HasFlag(Direction.Left))
			{
				canvas.DrawLine(topLeft, bottomLeft, paint);
			}
			if (directions.HasFlag(Direction.Right))
			{
				canvas.DrawLine(topRight, bottomRight, paint);
			}
		}
	}
}
