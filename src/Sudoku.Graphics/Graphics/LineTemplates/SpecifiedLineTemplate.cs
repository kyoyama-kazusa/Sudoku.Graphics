namespace Sudoku.Graphics.LineTemplates;

/// <summary>
/// Represents a line template that specifies a list of thick and thin lines.
/// </summary>
/// <param name="thickLineSegments"><inheritdoc cref="ThickLineSegments" path="/summary"/></param>
/// <param name="thinLineSegments"><inheritdoc cref="ThickLineSegments" path="/summary"/></param>
public sealed class SpecifiedLineTemplate(LineSegment[] thickLineSegments, LineSegment[] thinLineSegments) : RectangularBlockLineTemplate
{
	/// <summary>
	/// Initializes a <see cref="SpecifiedLineTemplate"/> instance.
	/// </summary>
	public SpecifiedLineTemplate() : this([], [])
	{
	}


	/// <summary>
	/// Indicates thick line segments.
	/// </summary>
	public LineSegment[] ThickLineSegments { get; init; } = thickLineSegments;

	/// <summary>
	/// Indicates thin line segments.
	/// </summary>
	public LineSegment[] ThinLineSegments { get; init; } = thinLineSegments;


	/// <inheritdoc/>
	protected override void GuardStatements(PointMapper mapper, SKCanvas canvas, CanvasDrawingOptions options)
	{
		var maxCellIndex = mapper.AbsoluteRowsCount * mapper.AbsoluteColumnsCount;
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
	protected override void DrawBorderRectangle(PointMapper mapper, SKCanvas canvas, CanvasDrawingOptions options)
	{
	}

	/// <inheritdoc/>
	protected override void DrawGridLines(PointMapper mapper, SKCanvas canvas, CanvasDrawingOptions options)
	{
		using var thickLinesPaint = CreateThickLinesPaint(mapper, options);
		using var thinLinesPaint = CreateThinLinesPaint(mapper, options);
		foreach (var (cellIndex, directions) in ThickLineSegments)
		{
			drawLine(
				mapper.GetPoint(cellIndex, CellCornerType.TopLeft),
				mapper.GetPoint(cellIndex, CellCornerType.TopRight),
				mapper.GetPoint(cellIndex, CellCornerType.BottomLeft),
				mapper.GetPoint(cellIndex, CellCornerType.BottomRight),
				directions,
				thickLinesPaint
			);
		}
		foreach (var (cellIndex, directions) in ThinLineSegments)
		{
			drawLine(
				mapper.GetPoint(cellIndex, CellCornerType.TopLeft),
				mapper.GetPoint(cellIndex, CellCornerType.TopRight),
				mapper.GetPoint(cellIndex, CellCornerType.BottomLeft),
				mapper.GetPoint(cellIndex, CellCornerType.BottomRight),
				directions,
				thinLinesPaint
			);
		}


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void drawLine(SKPoint topLeft, SKPoint topRight, SKPoint bottomLeft, SKPoint bottomRight, Direction directions, SKPaint paint)
		{
			if ((directions & Direction.Up) == Direction.Up)
			{
				canvas.DrawLine(topLeft, topRight, paint);
			}
			if ((directions & Direction.Down) == Direction.Down)
			{
				canvas.DrawLine(bottomLeft, bottomRight, paint);
			}
			if ((directions & Direction.Left) == Direction.Left)
			{
				canvas.DrawLine(topLeft, bottomLeft, paint);
			}
			if ((directions & Direction.Right) == Direction.Right)
			{
				canvas.DrawLine(topRight, bottomRight, paint);
			}
		}
	}
}
