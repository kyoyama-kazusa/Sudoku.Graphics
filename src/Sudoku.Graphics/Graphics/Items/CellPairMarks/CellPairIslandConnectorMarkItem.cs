namespace Sudoku.Graphics.Items.CellPairMarks;

/// <summary>
/// Represents cell pair island connector mark item.
/// </summary>
public sealed record CellPairIslandConnectorMarkItem : CellPairMarkItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellPair_IslandConnector;

	/// <inheritdoc/>
	public override Scale CornerRadiusScale { get; init; }

	/// <inheritdoc/>
	public required override Scale StrokeWidthScale { get; init; }

	/// <inheritdoc/>
	public required override SerializableColor StrokeColor { get; init; }

	/// <summary>
	/// Indicates island connector.
	/// </summary>
	public required IslandConnector IslandConnector { get; init; }


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var backingCanvas = canvas.BackingCanvas;
		var mapper = canvas.Mapper;
		var cellSize = mapper.CellSize;
		var startPoint = mapper.GetPoint(Cell1, Alignment.TopLeft);
		var endPoint = mapper.GetPoint(Cell2, Alignment.TopLeft);
		var connector = IslandConnector switch
		{
			DirectIslandConnector => new Connector { StartPoint = startPoint, EndPoint = endPoint },
			SingleCornerIslandConnector { ConnectedDirection: var startDirection } => new()
			{
				StartPoint = startPoint,
				EndPoint = endPoint,
				StartDirection = startDirection
			},
			DoubleCornerIslandConnector
			{
				StartConnectedDirection: var startDirection,
				EndConnectedDirection: var endDirection,
				Offset: var offset
			} => new()
			{
				StartPoint = startPoint,
				EndPoint = endPoint,
				StartDirection = startDirection,
				EndDirection = endDirection,
				Offset = offset
			},
			_ => throw new NotSupportedException()
		};
		using var path = ConnectorRenderer.BuildPath(connector, cellSize, CornerRadiusScale);
		using var strokePaint = new SKPaint
		{
			IsAntialias = true,
			Style = SKPaintStyle.Stroke,
			StrokeWidth = StrokeWidthScale.Measure(cellSize),
			Color = StrokeColor,
			StrokeJoin = SKStrokeJoin.Round,
			StrokeCap = SKStrokeCap.Round
		};

		backingCanvas.DrawPath(path, strokePaint);
	}
}

/// <summary>
/// Represents a file-local connector type that can interact with drawing.
/// </summary>
file sealed class Connector
{
	/// <summary>
	/// Indicates the start point.
	/// </summary>
	public required SKPoint StartPoint { get; init; }

	/// <summary>
	/// Indicates the end point.
	/// </summary>
	public required SKPoint EndPoint { get; init; }

	/// <summary>
	/// Indicates the start direction.
	/// This property is not <see langword="null"/> when <see cref="CellPairIslandConnectorMarkItem.IslandConnector"/>
	/// is either of type <see cref="SingleCornerIslandConnector"/> and <see cref="DoubleCornerIslandConnector"/>.
	/// </summary>
	/// <seealso cref="CellPairIslandConnectorMarkItem.IslandConnector"/>
	/// <seealso cref="SingleCornerIslandConnector"/>
	/// <seealso cref="DoubleCornerIslandConnector"/>
	public Direction4? StartDirection { get; init; }

	/// <summary>
	/// Indicates the end direction.
	/// This property is not <see langword="null"/> when <see cref="CellPairIslandConnectorMarkItem.IslandConnector"/>
	/// is either of type <see cref="SingleCornerIslandConnector"/> and <see cref="DoubleCornerIslandConnector"/>.
	/// </summary>
	/// <seealso cref="CellPairIslandConnectorMarkItem.IslandConnector"/>
	/// <seealso cref="SingleCornerIslandConnector"/>
	/// <seealso cref="DoubleCornerIslandConnector"/>
	public Direction4? EndDirection { get; init; }

	/// <summary>
	/// Indicates the offset to draw. 1 is for drawing with 0.5x cell width as offset, 2 is for 1.5x cell width.
	/// This property becomes valid if <see cref="CellPairIslandConnectorMarkItem.IslandConnector"/>
	/// is <see cref="DoubleCornerIslandConnector"/>, and so on.
	/// </summary>
	/// <seealso cref="CellPairIslandConnectorMarkItem.IslandConnector"/>
	/// <seealso cref="DoubleCornerIslandConnector"/>
	public int Offset { get; init; }
}

/// <summary>
/// Represents a connector renderer.
/// </summary>
file static class ConnectorRenderer
{
	/// <summary>
	/// Represents epsilon.
	/// </summary>
	private const float Epsilon = 1E-3F;


	/// <summary>
	/// Try to build a path to draw.
	/// </summary>
	/// <param name="connector">The connector.</param>
	/// <param name="cellSize">The size of cell.</param>
	/// <param name="cornerRadiusScale">The scale of corner radius, related to half size of cell.</param>
	/// <returns>A valid path instance.</returns>
	/// <exception cref="ArgumentException">Throws when parameter <paramref name="connector"/> is invalid.</exception>
	public static SKPath BuildPath(Connector connector, float cellSize, Scale cornerRadiusScale)
		=> connector switch
		{
			{ StartDirection: null, EndDirection: null } => BuildStraightPath(connector, cellSize, cornerRadiusScale),
			{ StartDirection: not null, EndDirection: null } => BuildOneTurnPath(connector, cellSize, cornerRadiusScale),
			{ StartDirection: not null, EndDirection: not null } => BuildTwoTurnPath(connector, cellSize, cornerRadiusScale),
			_ => throw new ArgumentException("Invalid connector configuration.", nameof(connector))
		};

	/// <summary>
	/// Try to build a straight path.
	/// </summary>
	/// <param name="c">The connector.</param>
	/// <param name="cellSize">The size of cell.</param>
	/// <param name="cornerRadiusScale">The scale of corner radius, related to half size of cell.</param>
	/// <returns>A valid path instance.</returns>
	/// <exception cref="InvalidOperationException">Throws when two cells are not in a same row or column.</exception>
	private static SKPath BuildStraightPath(Connector c, float cellSize, Scale cornerRadiusScale)
	{
		var points = new List<SKPoint>();
		if (float.NearlyEquals(c.StartPoint.Y, c.EndPoint.Y, Epsilon))
		{
			// Same row - left => right or right => left.
			var startLeft = c.StartPoint.X <= c.EndPoint.X;
			var aSide = startLeft ? Direction4.Right : Direction4.Left;
			var bSide = startLeft ? Direction4.Left : Direction4.Right;
			points.Add(GetSideCenter(startLeft ? c.StartPoint : c.EndPoint, aSide, cellSize));
			points.Add(GetSideCenter(startLeft ? c.EndPoint : c.StartPoint, bSide, cellSize));
		}
		else if (float.NearlyEquals(c.StartPoint.X, c.EndPoint.X, Epsilon))
		{
			// Same column - up => down or down => up.
			var startTop = c.StartPoint.Y <= c.EndPoint.Y;
			var aSide = startTop ? Direction4.Down : Direction4.Up;
			var bSide = startTop ? Direction4.Up : Direction4.Down;
			points.Add(GetSideCenter(startTop ? c.StartPoint : c.EndPoint, aSide, cellSize));
			points.Add(GetSideCenter(startTop ? c.EndPoint : c.StartPoint, bSide, cellSize));
		}
		else
		{
			throw new InvalidOperationException("Straight connector requires same row or same column.");
		}

		return BuildRoundedPath(points, cornerRadiusScale.Measure(cellSize / 2));
	}

	/// <summary>
	/// Creates a one-turn path.
	/// </summary>
	/// <param name="c">The connector.</param>
	/// <param name="cellSize">The size of cell.</param>
	/// <param name="cornerRadiusScale">The scale of corner radius, related to half size of cell.</param>
	/// <returns>A valid path.</returns>
	private static SKPath BuildOneTurnPath(Connector c, float cellSize, Scale cornerRadiusScale)
	{
		var startDir = c.StartDirection!.Value;
		var start = GetSideCenter(c.StartPoint, startDir, cellSize);

		// Smae row / column, create a straight line instead.
		if (IsHorizontal(startDir) && float.NearlyEquals(c.StartPoint.Y, c.EndPoint.Y, Epsilon))
		{
			return BuildStraightPath(new() { StartPoint = c.StartPoint, EndPoint = c.EndPoint }, cellSize, cornerRadiusScale);
		}
		if (!IsHorizontal(startDir) && float.NearlyEquals(c.StartPoint.X, c.EndPoint.X, Epsilon))
		{
			return BuildStraightPath(new() { StartPoint = c.StartPoint, EndPoint = c.EndPoint }, cellSize, cornerRadiusScale);
		}

		SKPoint end, elbow;
		if (IsHorizontal(startDir))
		{
			// Start direction is horizontal orientation - horizontal => vertical.
			var endSide = c.StartPoint.Y < c.EndPoint.Y ? Direction4.Up : Direction4.Down;
			end = GetSideCenter(c.EndPoint, endSide, cellSize);
			elbow = new(end.X, start.Y);
		}
		else
		{
			// Start direction is vertical - vertical => horizontal.
			var endSide = c.StartPoint.X < c.EndPoint.X ? Direction4.Left : Direction4.Right;
			end = GetSideCenter(c.EndPoint, endSide, cellSize);
			elbow = new(start.X, end.Y);
		}

		return BuildRoundedPath([start, elbow, end], cornerRadiusScale.Measure(cellSize / 2));
	}

	/// <summary>
	/// Creates a two-turn path.
	/// </summary>
	/// <param name="c">The connector.</param>
	/// <param name="cellSize">The size of cell.</param>
	/// <param name="cornerRadiusScale">The scale of corner radius, related to half size of cell.</param>
	/// <returns>A valid path instance.</returns>
	/// <exception cref="InvalidOperationException">Throws start and end direction are not in a same orientation.</exception>
	private static SKPath BuildTwoTurnPath(Connector c, float cellSize, Scale cornerRadiusScale)
	{
		var startDir = c.StartDirection!.Value;
		var endDir = c.EndDirection!.Value;
		if (IsHorizontal(startDir) != IsHorizontal(endDir))
		{
			const string errorInfo = $"Two-turn connector currently expects {nameof(Connector.StartDirection)} and {nameof(Connector.EndDirection)} on the same axis.";
			throw new InvalidOperationException(errorInfo);
		}

		var start = GetSideCenter(c.StartPoint, startDir, cellSize);
		var end = GetSideCenter(c.EndPoint, endDir, cellSize);
		var offset = Math.Max(1, c.Offset);
		var outPixels = (offset - 0.5F) * cellSize;
		var points = new List<SKPoint> { start };
		if (IsHorizontal(startDir))
		{
			// Horizontal => vertical => horizontal.
			var sx = startDir == Direction4.Right ? 1F : -1F;
			var p1 = new SKPoint(start.X + sx * outPixels, start.Y);
			var p2 = new SKPoint(p1.X, end.Y);
			points.Add(p1);
			points.Add(p2);
			points.Add(end);
		}
		else
		{
			// Vertical => horizontal => vertical.
			var sy = startDir == Direction4.Down ? 1F : -1F;
			var p1 = new SKPoint(start.X, start.Y + sy * outPixels);
			var p2 = new SKPoint(end.X, p1.Y);
			points.Add(p1);
			points.Add(p2);
			points.Add(end);
		}

		return BuildRoundedPath(points, cornerRadiusScale.Measure(cellSize / 2));
	}

	/// <summary>
	/// Try to build a rounded path.
	/// </summary>
	/// <param name="points">The points.</param>
	/// <param name="radius">The corner radius.</param>
	/// <returns>A valid path.</returns>
	private static SKPath BuildRoundedPath(List<SKPoint> points, float radius)
	{
		var path = new SKPath();
		if (points.Count == 0)
		{
			return path;
		}

		path.MoveTo(points[0]);

		if (points.Count == 1)
		{
			return path;
		}

		if (radius <= 0)
		{
			for (var i = 1; i < points.Count; i++)
			{
				path.LineTo(points[i]);
			}
			return path;
		}

		for (var i = 1; i < points.Count - 1; i++)
		{
			var a = points[i - 1];
			var b = points[i];
			var c = points[i + 1];
			var d1 = UnitVector(a, b);
			var d2 = UnitVector(b, c);
			var len1 = Distance(a, b);
			var len2 = Distance(b, c);
			var cut = MathF.Min(radius, MathF.Min(len1, len2) * 0.5F);
			if (cut <= Epsilon)
			{
				path.LineTo(b);
				continue;
			}

			var t1 = new SKPoint(b.X - d1.X * cut, b.Y - d1.Y * cut);
			var t2 = new SKPoint(b.X + d2.X * cut, b.Y + d2.Y * cut);
			path.LineTo(t1);
			path.QuadTo(b, t2);
		}

		path.LineTo(points[^1]);
		return path;
	}

	/// <summary>
	/// Get center point of the specified side of border.
	/// </summary>
	/// <param name="topLeft">The top-left point.</param>
	/// <param name="side">The side direction.</param>
	/// <param name="cellSize">The cell size.</param>
	/// <returns>The point.</returns>
	/// <exception cref="ArgumentOutOfRangeException">Throws when <paramref name="side"/> is not defined.</exception>
	private static SKPoint GetSideCenter(SKPoint topLeft, Direction4 side, float cellSize)
		=> side switch
		{
			Direction4.Up => new SKPoint(topLeft.X + cellSize * 0.5F, topLeft.Y),
			Direction4.Right => new SKPoint(topLeft.X + cellSize, topLeft.Y + cellSize * 0.5F),
			Direction4.Down => new SKPoint(topLeft.X + cellSize * 0.5F, topLeft.Y + cellSize),
			Direction4.Left => new SKPoint(topLeft.X, topLeft.Y + cellSize * 0.5f),
			_ => throw new ArgumentOutOfRangeException(nameof(side))
		};

	/// <summary>
	/// Indicates whether the specififed direction is horizontal or not.
	/// </summary>
	/// <param name="side">The direction to check.</param>
	/// <returns>A <see cref="bool"/> result.</returns>
	private static bool IsHorizontal(Direction4 side) => side is Direction4.Left or Direction4.Right;

	/// <summary>
	/// Unit vector.
	/// </summary>
	/// <param name="from">The from point.</param>
	/// <param name="to">The to point.</param>
	/// <returns>The unit vector.</returns>
	private static SKPoint UnitVector(SKPoint from, SKPoint to)
	{
		var dx = to.X - from.X;
		var dy = to.Y - from.Y;
		return MathF.Abs(dx) >= MathF.Abs(dy) ? new(MathF.Sign(dx), 0f) : new(0, MathF.Sign(dy));
	}

	/// <summary>
	/// Get distance of two points.
	/// </summary>
	/// <param name="a">The first point.</param>
	/// <param name="b">The second point.</param>
	/// <returns>The distance between them.</returns>
	private static float Distance(SKPoint a, SKPoint b)
	{
		var dx = b.X - a.X;
		var dy = b.Y - a.Y;
		return MathF.Sqrt(dx * dx + dy * dy);
	}
}
