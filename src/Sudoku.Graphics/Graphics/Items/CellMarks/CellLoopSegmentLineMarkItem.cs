namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents a cell loop segment line mark item.
/// </summary>
public sealed record CellLoopSegmentLineMarkItem : CellMarkItem
{
	/// <summary>
	/// Indicates all directions that the segment line will occupy.
	/// </summary>
	public required Direction4 OccupiedDirections { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.Cell_LoopSegmentLine;

	/// <inheritdoc/>
	public required override Scale StrokeWidthScale { get; init; }

	/// <inheritdoc/>
	public override Scale CornerRadiusScale { get; init; }

	/// <inheritdoc/>
	public required override SerializableColor StrokeColor { get; init; }


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var directionFlags = OccupiedDirections & (Direction4.Up | Direction4.Down | Direction4.Left | Direction4.Right);
		if (directionFlags == Direction4.None)
		{
			// Nothing to draw.
			return;
		}

		var mapper = canvas.Templates[TemplateIndex].Mapper;
		var cellSize = mapper.CellSize;
		using var strokePaint = new SKPaint
		{
			IsAntialias = true,
			Style = SKPaintStyle.Stroke,
			StrokeWidth = StrokeWidthScale.Measure(cellSize),
			Color = StrokeColor,
			StrokeJoin = SKStrokeJoin.Round,
			StrokeCap = SKStrokeCap.Round
		};

		var backingCanvas = canvas.BackingCanvas;
		var center = mapper.GetPoint(Cell, Alignment.Center);

		var directions = new List<Direction4>(2);
		if (directionFlags.HasFlag(Direction4.Up))
		{
			directions.Add(Direction4.Up);
		}
		if (directionFlags.HasFlag(Direction4.Down) && directions.Count < 2)
		{
			directions.Add(Direction4.Down);
		}
		if (directionFlags.HasFlag(Direction4.Left) && directions.Count < 2)
		{
			directions.Add(Direction4.Left);
		}
		if (directionFlags.HasFlag(Direction4.Right) && directions.Count < 2)
		{
			directions.Add(Direction4.Right);
		}

		using var path = SegmentRoundCornerBuilder.BuildPath(
			center,
			cellSize,
			directions[0],
			directions is [_, var second] ? second : Direction4.None,
			CornerRadiusScale
		);
		backingCanvas.DrawPath(path, strokePaint);
	}
}

/// <summary>
/// Provides a cell path builder.
/// </summary>
file static class SegmentRoundCornerBuilder
{
	/// <summary>
	/// Builds an <see cref="SKPath"/> instance.
	/// </summary>
	/// <param name="center">The center point of cell.</param>
	/// <param name="cellSize">The size of cell.</param>
	/// <param name="direction1">The first direction.</param>
	/// <param name="direction2">The second direction.</param>
	/// <param name="cornerRadiusScale">The scale of corner radius, related to half of cell size.</param>
	/// <returns>The <see cref="SKPath"/> instance.</returns>
	public static SKPath BuildPath(SKPoint center, float cellSize, Direction4 direction1, Direction4 direction2, Scale cornerRadiusScale)
	{
		var halfCellSize = cellSize / 2;
		var r = cornerRadiusScale.Clamp01().Measure(halfCellSize);

		var path = new SKPath();

		// Only one direction.
		if (direction1 == Direction4.None && direction2 != Direction4.None)
		{
			path.MoveTo(center);
			path.LineTo(borderPoint(center, halfCellSize, direction2));
			return path;
		}
		if (direction2 == Direction4.None && direction1 != Direction4.None)
		{
			path.MoveTo(center);
			path.LineTo(borderPoint(center, halfCellSize, direction1));
			return path;
		}

		// Two directions.
		if (direction1 == Direction4.None && direction2 == Direction4.None)
		{
			return path;
		}

		// Unify order of directions.
		if (areOpposite(direction1, direction2))
		{
			// They are opposite with each other - just draw a line.
			path.MoveTo(borderPoint(center, halfCellSize, direction1));
			path.LineTo(borderPoint(center, halfCellSize, direction2));
			return path;
		}

		if (r <= 0)
		{
			// There's no corner radius.
			path.MoveTo(borderPoint(center, halfCellSize, direction1));
			path.LineTo(center);
			path.LineTo(borderPoint(center, halfCellSize, direction2));
			return path;
		}

		// Build path via different cases.
		if (isPair(direction1, direction2, Direction4.Up, Direction4.Right))
		{
			upRight(path, center, halfCellSize, r);
		}
		else if (isPair(direction1, direction2, Direction4.Right, Direction4.Down))
		{
			rightDown(path, center, halfCellSize, r);
		}
		else if (isPair(direction1, direction2, Direction4.Down, Direction4.Left))
		{
			downLeft(path, center, halfCellSize, r);
		}
		else/* if (isPair(direction1, direction2, Direction4.Left, Direction4.Up))*/
		{
			leftUp(path, center, halfCellSize, r);
		}

		return path;


		static void upRight(SKPath path, SKPoint c, float half, float r)
		{
			path.MoveTo(c.X, c.Y - half);
			path.LineTo(c.X, c.Y - r);
			path.ArcTo(new SKRect(c.X, c.Y - 2 * r, c.X + 2 * r, c.Y), 180, -90, false);
			path.LineTo(c.X + half, c.Y);
		}

		static void rightDown(SKPath path, SKPoint c, float half, float r)
		{
			path.MoveTo(c.X + half, c.Y);
			path.LineTo(c.X + r, c.Y);
			path.ArcTo(new SKRect(c.X, c.Y, c.X + 2 * r, c.Y + 2 * r), 270, -90, false);
			path.LineTo(c.X, c.Y + half);
		}

		static void downLeft(SKPath path, SKPoint c, float half, float r)
		{
			path.MoveTo(c.X, c.Y + half);
			path.LineTo(c.X, c.Y + r);
			path.ArcTo(new SKRect(c.X - 2 * r, c.Y, c.X, c.Y + 2 * r), 0, -90, false);
			path.LineTo(c.X - half, c.Y);
		}

		static void leftUp(SKPath path, SKPoint c, float half, float r)
		{
			path.MoveTo(c.X - half, c.Y);
			path.LineTo(c.X - r, c.Y);
			path.ArcTo(new SKRect(c.X - 2 * r, c.Y - 2 * r, c.X, c.Y), 90, -90, false);
			path.LineTo(c.X, c.Y - half);
		}

		static SKPoint borderPoint(SKPoint c, float half, Direction4 dir)
			=> dir switch
			{
				Direction4.Up => new SKPoint(c.X, c.Y - half),
				Direction4.Right => new SKPoint(c.X + half, c.Y),
				Direction4.Down => new SKPoint(c.X, c.Y + half),
				Direction4.Left => new SKPoint(c.X - half, c.Y),
				_ => c
			};

		static bool areOpposite(Direction4 a, Direction4 b)
			=> a == Direction4.Up && b == Direction4.Down || a == Direction4.Down && b == Direction4.Up
			|| a == Direction4.Left && b == Direction4.Right || a == Direction4.Right && b == Direction4.Left;

		static bool isPair(Direction4 a, Direction4 b, Direction4 x, Direction4 y) => a == x && b == y || a == y && b == x;
	}
}
