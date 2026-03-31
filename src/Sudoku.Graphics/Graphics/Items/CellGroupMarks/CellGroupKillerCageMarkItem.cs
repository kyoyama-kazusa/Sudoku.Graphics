namespace Sudoku.Graphics.Items.CellGroupMarks;

/// <summary>
/// Represents a cell group killer cage mark item.
/// </summary>
public sealed record CellGroupKillerCageMarkItem : CellGroupMarkItem
{
	/// <summary>
	/// Indicates the padding top of the boundary of text drawn.
	/// </summary>
	public float PaddingTop { get; init; } = 0;

	/// <summary>
	/// Indicates the padding bottom of the boundary of text drawn.
	/// </summary>
	public float PaddingBottom { get; init; } = 0;

	/// <summary>
	/// Indicates the padding left of the boundary of text drawn.
	/// </summary>
	public float PaddingLeft { get; init; } = 0;

	/// <summary>
	/// Indicates the padding right of the boundary of text drawn.
	/// </summary>
	public float PaddingRight { get; init; } = 0;

	/// <summary>
	/// The X value of offset to the text to be drawn.
	/// </summary>
	public float OffsetX { get; init; } = 0;

	/// <summary>
	/// The Y value of offset to the text to be drawn.
	/// </summary>
	public float OffsetY { get; init; } = 0;

	/// <summary>
	/// Indicates text to be drawn in killer cage.
	/// If a cage this instance represents doesn't provide a text, this property can be left <see langword="null"/>.
	/// </summary>
	public string? Text { get; init; }

	/// <inheritdoc/>
	public override string? TextFontName { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellGroup_KillerCage;

	/// <inheritdoc/>
	public override SKFontStyleWeight FontWeight { get; init; } = SKFontStyleWeight.Normal;

	/// <inheritdoc/>
	public override SKFontStyleWidth FontWidth { get; init; } = SKFontStyleWidth.Normal;

	/// <inheritdoc/>
	public override SKFontStyleSlant FontSlant { get; init; } = SKFontStyleSlant.Upright;

	/// <summary>
	/// Indicates the text color.
	/// </summary>
	public SerializableColor TextColor { get; init; }

	/// <summary>
	/// Indicates text background color.
	/// </summary>
	public SerializableColor TextBackgroundColor { get; init; }

	/// <inheritdoc/>
	public required override SerializableColor StrokeColor { get; init; }

	/// <inheritdoc/>
	public override SerializableColor FillColor { get; init; }

	/// <summary>
	/// Indicates the scale of size of each cells drawn, related to cell size.
	/// </summary>
	public required Scale ShortSideScale { get; init; }

	/// <inheritdoc/>
	public required override Scale FontSizeScale { get; init; }

	/// <inheritdoc/>
	public override Scale CornerRadiusScale { get; init; }

	/// <inheritdoc/>
	public required override Scale StrokeWidthScale { get; init; }

	/// <summary>
	/// Indicates the dash sequence.
	/// </summary>
	public LineDashSequence DashSequence { get; init; } = [];


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var backingCanvas = canvas.BackingCanvas;
		var mapper = canvas.Templates[TemplateIndex].Mapper;
		var cellSize = mapper.CellSize;
		var points = from cell in Cells select mapper.GetPoint(cell, Alignment.TopLeft);
		var shortSide = ShortSideScale.Measure(cellSize);
		var cornerRadius = CornerRadiusScale.Measure(shortSide);
		if (!CellOutlineBuilder.TryBuildKillerCagePath(points, cellSize, cornerRadius, shortSide, out var pathInfo))
		{
			return;
		}

		using var strokePaint = new SKPaint
		{
			IsAntialias = true,
			Style = SKPaintStyle.Stroke,
			StrokeWidth = StrokeWidthScale.Measure(cellSize),
			Color = StrokeColor,
			StrokeCap = SKStrokeCap.Round,
			StrokeJoin = SKStrokeJoin.Round,
			PathEffect = DashSequence
		};
		using var fillPaint = new SKPaint { IsAntialias = true, Style = SKPaintStyle.Fill, Color = FillColor };

		// Draw killer cage.
		using var path = pathInfo.Path;
		backingCanvas.DrawPath(path, strokePaint);
		backingCanvas.DrawPath(path, fillPaint);

		// Draw text.
		if (Text is not null)
		{
			ArgumentNullException.ThrowIfNull(TextFontName);

			using var typeface = SKTypeface.FromFamilyName(TextFontName, FontWeight, FontWidth, FontSlant);
			var factSize = FontSizeScale.Measure(cellSize);
			using var textFont = new SKFont(typeface, factSize) { Subpixel = true };
			using var textPaint = new SKPaint
			{
				Style = SKPaintStyle.Fill,
				Color = TextColor,
				IsAntialias = true,
				StrokeWidth = factSize,
				StrokeJoin = SKStrokeJoin.Round
			};
			using var textCoverFillPaint = new SKPaint { Style = SKPaintStyle.Fill, Color = TextBackgroundColor, IsAntialias = true };

			backingCanvas.DrawTextWithCover(
				pathInfo.TopLeftTurnPoint.AlignYAsBaseline(textFont),
				Text,
				SKTextAlign.Left,
				CoverStyle.Rectangle,
				textFont,
				textPaint,
				null,
				textCoverFillPaint,
				PaddingTop,
				PaddingBottom,
				PaddingLeft,
				PaddingRight,
				new(OffsetX, OffsetY)
			);
		}
	}
}

/// <summary>
/// Represents killer cage builder.
/// </summary>
file static class CellOutlineBuilder
{
	/// <summary>
	/// Try to build a path of a killer cage.
	/// </summary>
	/// <param name="points">The points.</param>
	/// <param name="cellSize">The size of each cell.</param>
	/// <param name="cornerRadius">The corner radius.</param>
	/// <param name="shortSide">The short side of cage.</param>
	/// <param name="result">The result cage information (path and its top-left point).</param>
	/// <returns>A <see cref="bool"/> result indicating the cage can be built or not.</returns>
	public static bool TryBuildKillerCagePath(
		ReadOnlySpan<SKPoint> points,
		float cellSize,
		float cornerRadius,
		float shortSide,
		out (SKPath Path, SKPoint TopLeftTurnPoint) result
	)
	{
		var margin = (cellSize - shortSide) / 2;
		var originX = points.Min(static p => p.X);
		var originY = points.Min(static p => p.Y);
		var cells = new HashSet<GridPoint>();
		foreach (var p in points)
		{
			var gx = (int)MathF.Round((p.X - originX) / cellSize);
			var gy = (int)MathF.Round((p.Y - originY) / cellSize);
			cells.Add(new(gx, gy));
		}

		var boundaryEdges = BuildBoundaryEdges(cells);
		var cycle = TraceClockwiseBoundaryCycle(boundaryEdges);
		if (cycle.Count < 4)
		{
			result = default;
			return false;
		}

		var insetCorners = new List<SKPoint>();
		var topLeftTurnPoint = default(SKPoint?);
		var n = cycle.Count;
		for (var i = 0; i < n; i++)
		{
			var previous = cycle[(i - 1 + n) % n];
			var current = cycle[i];
			var next = cycle[(i + 1) % n];

			var d1 = GetDirection(previous, current);
			var d2 = GetDirection(current, next);
			if (d1.X == d2.X && d1.Y == d2.Y)
			{
				continue;
			}

			var r1 = RightNormal(d1);
			var r2 = RightNormal(d2);
			var inset = new SKPoint(originX + current.X * cellSize + (r1.X + r2.X) * margin, originY + current.Y * cellSize + (r1.Y + r2.Y) * margin);
			insetCorners.Add(inset);
			if (topLeftTurnPoint is not var (x, y) || inset.Y < y || Math.Abs(inset.Y - y) < 1E-3F && inset.X < x)
			{
				topLeftTurnPoint = inset;
			}
		}

		// Build a path. Here we don't use 'using' keyword because it will be used by its caller.
		var path = buildRoundedPolygonPath(insetCorners.AsSpan(), cornerRadius);
		result = (path, topLeftTurnPoint ?? default);
		return true;


		static HashSet<GridEdge> BuildBoundaryEdges(HashSet<GridPoint> cells)
		{
			var edges = new HashSet<GridEdge>();
			foreach (var c in cells)
			{
				ToggleEdge(edges, new(c.X, c.Y), new(c.X + 1, c.Y)); // Top.
				ToggleEdge(edges, new(c.X + 1, c.Y), new(c.X + 1, c.Y + 1)); // Right.
				ToggleEdge(edges, new(c.X + 1, c.Y + 1), new(c.X, c.Y + 1)); // Bottom.
				ToggleEdge(edges, new(c.X, c.Y + 1), new(c.X, c.Y)); // Left.
			}
			return edges;
		}

		static void ToggleEdge(HashSet<GridEdge> edges, GridPoint a, GridPoint b)
		{
			var e = new GridEdge(a, b);
			if (!edges.Add(e))
			{
				edges.Remove(e);
			}
		}

		static List<GridPoint> TraceClockwiseBoundaryCycle(HashSet<GridEdge> edges)
		{
			var adj = new Dictionary<GridPoint, List<GridPoint>>();
			foreach (var e in edges)
			{
				addAdj(e.A, e.B);
				addAdj(e.B, e.A);
			}

			// Starts with the top-left point, and draw the path, in clockwise order.
			var start = (from p in adj.Keys orderby p.Y, p.X select p).First();
			var startNeighbors = adj[start];
			var next = default(GridPoint);
			var foundRight = false;
			foreach (var n in startNeighbors)
			{
				if (n.Y == start.Y && n.X > start.X)
				{
					next = n;
					foundRight = true;
					break;
				}
			}
			if (!foundRight)
			{
				next = startNeighbors[0];
			}

			var cycle = new List<GridPoint> { start };
			var prev = start;
			var cur = next;
			while (!cur.Equals(start))
			{
				cycle.Add(cur);
				var neighbors = adj[cur];
				var candidate = neighbors[0].Equals(prev) ? neighbors[1] : neighbors[0];
				prev = cur;
				cur = candidate;
			}

			return cycle;


			void addAdj(GridPoint from, GridPoint to)
			{
				if (!adj.TryGetValue(from, out var list))
				{
					list = new(2);
					adj[from] = list;
				}
				list.Add(to);
			}
		}

		static GridPoint GetDirection(GridPoint from, GridPoint to) => new(Math.Sign(to.X - from.X), Math.Sign(to.Y - from.Y));

		static GridPoint RightNormal(GridPoint direction) => new(-direction.Y, direction.X);

		static SKPath buildRoundedPolygonPath(ReadOnlySpan<SKPoint> points, float radius)
		{
			var path = new SKPath();
			if (points.Length == 1)
			{
				path.MoveTo(points[0]);
				path.Close();
				return path;
			}

			if (radius <= 0)
			{
				path.MoveTo(points[0]);
				for (var i = 1; i < points.Length; i++)
				{
					path.LineTo(points[i]);
				}
				path.Close();
				return path;
			}

			var n = points.Length;
			for (var i = 0; i < n; i++)
			{
				var previous = points[(i - 1 + n) % n];
				var current = points[i];
				var next = points[(i + 1) % n];
				var directionIn = normalize(new SKPoint(current.X - previous.X, current.Y - previous.Y));
				var directionOut = normalize(new SKPoint(next.X - current.X, next.Y - current.Y));
				var p1 = new SKPoint(current.X - directionIn.X * radius, current.Y - directionIn.Y * radius);
				var p2 = new SKPoint(current.X + directionOut.X * radius, current.Y + directionOut.Y * radius);
				if (i == 0)
				{
					path.MoveTo(p1);
				}
				else
				{
					path.LineTo(p1);
				}

				// Create a corner radius - From 'p1' to 'p2', via center of circle 'cur'.
				path.ArcTo(radius, radius, 0, SKPathArcSize.Small, getSweepDirection(previous, current, next), p2.X, p2.Y);
			}

			path.Close();
			return path;
		}

		static SKPoint normalize(SKPoint v)
		{
			var length = MathF.Sqrt(v.X * v.X + v.Y * v.Y);
			return length <= 1E-6F ? default : new(v.X / length, v.Y / length);
		}

		static SKPathDirection getSweepDirection(SKPoint previous, SKPoint current, SKPoint next)
		{
			var inVector = new SKPoint(current.X - previous.X, current.Y - previous.Y);
			var outVector = new SKPoint(next.X - current.X, next.Y - current.Y);
			var cross = inVector.X * outVector.Y - inVector.Y * outVector.X;
			return cross >= 0 ? SKPathDirection.Clockwise : SKPathDirection.CounterClockwise;
		}
	}
}

/// <summary>
/// Represents a grid point.
/// </summary>
/// <param name="X">Indicates X value of the point.</param>
/// <param name="Y">Indicates Y value of the point.</param>
file readonly record struct GridPoint(int X, int Y);

/// <summary>
/// Represents a grid edge of a killer cage.
/// </summary>
file readonly struct GridEdge : IEquatable<GridEdge>
{
	/// <summary>
	/// Initializes a <see cref="GridEdge"/> instance via two <see cref="GridPoint"/> instances.
	/// </summary>
	/// <param name="p1">Indicates the first point instance.</param>
	/// <param name="p2">Indicates the second point instance.</param>
	public GridEdge(GridPoint p1, GridPoint p2)
	{
		if (p1.X < p2.X || p1.X == p2.X && p1.Y <= p2.Y)
		{
			A = p1;
			B = p2;
		}
		else
		{
			A = p2;
			B = p1;
		}
	}


	/// <summary>
	/// Indicates the first point instance.
	/// </summary>
	public GridPoint A { get; }

	/// <summary>
	/// Indicates the second point instance.
	/// </summary>
	public GridPoint B { get; }


	/// <inheritdoc/>
	public override bool Equals([NotNullWhen(true)] object? obj) => obj is GridEdge other && Equals(other);

	/// <inheritdoc/>
	public bool Equals(GridEdge other) => A == other.A && B == other.B;

	/// <inheritdoc cref="object.GetHashCode"/>
	public override int GetHashCode() => HashCode.Combine(A, B);
}
