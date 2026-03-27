namespace Sudoku.Graphics;

public partial class SKCanvasDrawings
{
	extension(SKCanvas @this)
	{
		/// <summary>
		/// Draws a killer cage.
		/// </summary>
		/// <param name="cells">The cells.</param>
		/// <param name="sizeScale">The scale of size of short side, related to a cell size.</param>
		/// <param name="strokeColor">The stroke color of the cage outline.</param>
		/// <param name="strokeWidthScale">The scale of stroke width, related to cell size.</param>
		/// <param name="fillColor">The fill color.</param>
		/// <param name="dashSequence">The dash sequence of the cage outline.</param>
		/// <param name="cornerRadiusScale">The scale of corner radius of the cage, related to cage short side.</param>
		/// <param name="text">The text.</param>
		/// <param name="textFontName">The text font name.</param>
		/// <param name="textSizeScale">The scale of text, related to cell size.</param>
		/// <param name="textWeight">The font weight.</param>
		/// <param name="textWidth">The font width.</param>
		/// <param name="textSlant">The font slant.</param>
		/// <param name="textColor">The font color.</param>
		/// <param name="textBackgroundColor">The text background color.</param>
		/// <param name="textPaddingTop">The padding top of the boundary of text drawn.</param>
		/// <param name="textPaddingBottom">The padding bottom of the boundary of text drawn.</param>
		/// <param name="textPaddingLeft">The padding left of the boundary of text drawn.</param>
		/// <param name="textPaddingRight">The padding right of the boundary of text drawn.</param>
		/// <param name="textOffsetX">The X value of offset to the text to be drawn.</param>
		/// <param name="textOffsetY">The Y value of offset to the text to be drawn.</param>
		/// <param name="mapper">The point mapper instance.</param>
		/// <exception cref="ArgumentNullException">
		/// Throws when <paramref name="textFontName"/> is <see langword="null"/>,
		/// but <paramref name="text"/> is not <see langword="null"/>.
		/// </exception>
		public void DrawKillerCage(
			ReadOnlySpan<Absolute> cells,
			Scale sizeScale,
			SerializableColor strokeColor,
			Scale strokeWidthScale,
			SerializableColor fillColor,
			LineDashSequence dashSequence,
			Scale cornerRadiusScale,
			string? text,
			string? textFontName,
			Scale textSizeScale,
			SKFontStyleWeight textWeight,
			SKFontStyleWidth textWidth,
			SKFontStyleSlant textSlant,
			SerializableColor textColor,
			SerializableColor textBackgroundColor,
			float textPaddingTop,
			float textPaddingBottom,
			float textPaddingLeft,
			float textPaddingRight,
			float textOffsetX,
			float textOffsetY,
			PointMapper mapper
		)
		{
			var cellSize = mapper.CellSize;
			var points = from cell in cells select mapper.GetPoint(cell, Alignment.TopLeft);
			var shortSide = sizeScale.Measure(cellSize);
			var cornerRadius = cornerRadiusScale.Measure(shortSide);
			if (!CellOutlineBuilder.TryBuildKillerCagePath(points, cellSize, cornerRadius, shortSide, out var pathInfo))
			{
				return;
			}

			using var strokePaint = new SKPaint
			{
				IsAntialias = true,
				Style = SKPaintStyle.Stroke,
				StrokeWidth = strokeWidthScale.Measure(cellSize),
				Color = strokeColor,
				StrokeCap = SKStrokeCap.Round,
				StrokeJoin = SKStrokeJoin.Round,
				PathEffect = dashSequence
			};
			using var fillPaint = new SKPaint { IsAntialias = true, Style = SKPaintStyle.Fill, Color = fillColor };

			// Draw killer cage.
			using var path = pathInfo.Path;
			@this.DrawPath(path, strokePaint);
			@this.DrawPath(path, fillPaint);

			// Draw text.
			if (text is not null)
			{
				ArgumentNullException.ThrowIfNull(textFontName);

				using var typeface = SKTypeface.FromFamilyName(textFontName, textWeight, textWidth, textSlant);
				var factSize = textSizeScale.Measure(mapper.CellSize);
				using var textFont = new SKFont(typeface, factSize) { Subpixel = true };
				using var textPaint = new SKPaint
				{
					Style = SKPaintStyle.Fill,
					Color = textColor,
					IsAntialias = true,
					StrokeWidth = factSize,
					StrokeJoin = SKStrokeJoin.Round
				};

				textFont.GetFontMetrics(out var metrics);
				var killerTopLeft = pathInfo.TopLeftTurnPoint;
				killerTopLeft += new SKPoint(0, (metrics.Ascent + metrics.Descent) / 2); // Baseline adjustment.
				killerTopLeft += new SKPoint(0, textFont.Size / 2); // Centralize.
				killerTopLeft += new SKPoint(0, cellSize / 24); // Manual adjustment.

				textFont.MeasureText(text, out var bounds, textPaint);
				bounds.Offset(killerTopLeft);
				bounds = new(bounds.Left + textOffsetX, bounds.Top + textOffsetY, bounds.Right + textOffsetX, bounds.Bottom + textOffsetY);

				// Fill text boundary.
				using var textBackgroundPaint = new SKPaint { Style = SKPaintStyle.Fill, Color = textBackgroundColor, IsAntialias = true };
				if (cornerRadius > 0)
				{
					var tempBounds = new SKRect(
						bounds.Left - textPaddingLeft,
						bounds.Top - textPaddingTop,
						bounds.Right + textPaddingRight,
						bounds.Bottom + textPaddingBottom
					);
					@this.DrawRoundRect(tempBounds, cornerRadius, cornerRadius, textBackgroundPaint);
				}
				else
				{
					@this.DrawRect(bounds, textBackgroundPaint);
				}

				killerTopLeft.X += textOffsetX;
				killerTopLeft.Y += textOffsetY;
				@this.DrawText(text, killerTopLeft, SKTextAlign.Left, textFont, textPaint);
			}
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
