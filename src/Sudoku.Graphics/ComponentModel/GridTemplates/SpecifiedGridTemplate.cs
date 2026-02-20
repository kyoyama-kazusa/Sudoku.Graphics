namespace Sudoku.ComponentModel.GridTemplates;

/// <summary>
/// Represents a grid template that specifies a list of thick and thin lines.
/// </summary>
public sealed class SpecifiedGridTemplate : IndividualGridTemplate
{
	/// <summary>
	/// Indicates thick line segments.
	/// </summary>
	public required LineSegment[] ThickLineSegments { get; init; }

	/// <summary>
	/// Indicates thin line segments.
	/// </summary>
	public required LineSegment[] ThinLineSegments { get; init; }


	/// <inheritdoc/>
	protected override void GuardStatements(SKCanvas canvas, CanvasDrawingOptions options)
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
	protected override void DrawBorderRectangle(SKCanvas canvas, CanvasDrawingOptions options)
	{
	}

	/// <inheritdoc/>
	protected override void DrawGridLines(SKCanvas canvas, CanvasDrawingOptions options)
	{
		using var thickLinesPaint = CreateThickLinesPaint(options);
		using var thinLinesPaint = CreateThinLinesPaint(options);
		foreach (var (cellIndex, directions) in ThickLineSegments)
		{
			drawLine(
				Mapper.GetPoint(cellIndex, CellAlignment.TopLeft),
				Mapper.GetPoint(cellIndex, CellAlignment.TopRight),
				Mapper.GetPoint(cellIndex, CellAlignment.BottomLeft),
				Mapper.GetPoint(cellIndex, CellAlignment.BottomRight),
				directions,
				thickLinesPaint
			);
		}
		foreach (var (cellIndex, directions) in ThinLineSegments)
		{
			drawLine(
				Mapper.GetPoint(cellIndex, CellAlignment.TopLeft),
				Mapper.GetPoint(cellIndex, CellAlignment.TopRight),
				Mapper.GetPoint(cellIndex, CellAlignment.BottomLeft),
				Mapper.GetPoint(cellIndex, CellAlignment.BottomRight),
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
