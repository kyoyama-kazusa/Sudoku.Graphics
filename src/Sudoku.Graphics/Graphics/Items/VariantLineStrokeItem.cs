namespace Sudoku.Graphics.Items;

/// <summary>
/// Represents variant line stroke item.
/// </summary>
public sealed record VariantLineStrokeItem : Item, IItem_ColorProperty
{
	/// <summary>
	/// Indicates template index.
	/// </summary>
	public required int TemplateIndex { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.VariantLineStroke;

	/// <summary>
	/// Indicates start cell alignment.
	/// </summary>
	public required Alignment StartCellAlignment { get; init; }

	/// <summary>
	/// Indicates interim cell alignment.
	/// </summary>
	public required Alignment InterimCellAlignment { get; init; }

	/// <summary>
	/// Indicates the start cell.
	/// </summary>
	public required Absolute StartCell { get; init; }

	/// <summary>
	/// Indicates the interim cell.
	/// </summary>
	public required Absolute InterimCell { get; init; }

	/// <inheritdoc/>
	public required SerializableColor Color { get; init; }

	/// <summary>
	/// Indicates stroke width scale.
	/// </summary>
	public required Scale StrokeWidthScale { get; init; }

	/// <summary>
	/// Indicates line dash sequence.
	/// </summary>
	public LineDashSequence LineDashSequence { get; init; } = [];


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var mapper = canvas.Templates[TemplateIndex].Mapper;
		var (start, end) = GridLineExtender.ComputeExtendedLine(
			mapper.GetPoint(StartCell, Alignment.TopLeft),
			StartCellAlignment,
			mapper.GetPoint(InterimCell, Alignment.TopLeft),
			InterimCellAlignment,
			mapper.CellSize,
			mapper.RowsCount,
			mapper.ColumnsCount,
			mapper.Margin,
			mapper.Margin
		);
		using var strokePaint = new SKPaint
		{
			Style = SKPaintStyle.Stroke,
			Color = Color,
			StrokeWidth = StrokeWidthScale.Measure(mapper.CellSize),
			StrokeCap = SKStrokeCap.Round,
			StrokeJoin = SKStrokeJoin.Round,
			IsAntialias = true,
			PathEffect = LineDashSequence.IsEmpty ? null : LineDashSequence
		};
		canvas.BackingCanvas.DrawLine(start, end, strokePaint);
	}
}

/// <summary>
/// Represents a grid line extender.
/// </summary>
file static class GridLineExtender
{
	/// <summary>
	/// Represents epsilon.
	/// </summary>
	private const float Epsilon = 1E-9F;


	/// <summary>
	/// Gives start and anchor (interim) points, to calculate the target point intersected with grid border.
	/// </summary>
	/// <param name="start">The start point.</param>
	/// <param name="anchor">The anchor point.</param>
	/// <param name="left">The left value of grid.</param>
	/// <param name="top">The top value of grid.</param>
	/// <param name="right">The right value of grid.</param>
	/// <param name="bottom">The bottom value of grid.</param>
	/// <returns>The result point.</returns>
	public static SKPoint ExtendRayToRectangleBoundary(SKPoint start, SKPoint anchor, float left, float top, float right, float bottom)
	{
		#region Comment
		// Algorithm overview:
		// 
		// 1. First compute the exact coordinates of the start point S and the anchor point A.
		//    These are determined by the cell's top-left position, the cellSize, and the specified corner enum.
		//
		// 2. Construct a direction vector d = A - S.
		//    This vector represents the direction of the line that starts at S and points toward A.
		//    If d = (0,0), the line is degenerate and cannot be extended.
		//
		// 3. Represent the line as a parametric ray:
		// 	  P(t) = S + t * d
		//    where t > 0 means moving from the start point in the direction of A.
		//
		// 4. Compute intersections between this ray and the four boundaries of the table rectangle:
		// 	  x = left
		// 	  x = right
		// 	  y = top
		// 	  y = bottom
		// 
		//    For each boundary, solve the corresponding parameter t and check
		//    whether the resulting intersection point lies within the valid range of that edge segment.
		//
		// 5. Among all valid intersection parameters (t > 0), choose the smallest one.
		//    This represents the first point where the ray exits the table rectangle.
		//
		// 6. Compute the final endpoint E = P(t).
		//    The line segment to draw is therefore from S to E.
		#endregion

		var sx = start.X;
		var sy = start.Y;
		var dx = anchor.X - start.X;
		var dy = anchor.Y - start.Y;

		// Check whether there is no direction to extend.
		if (Math.Abs(dx) < Epsilon && Math.Abs(dy) < Epsilon)
		{
			return start;
		}

		var possibleTs = new List<float>();

		// Find intersection point X value.
		if (Math.Abs(dx) > Epsilon)
		{
			var tLeft = (left - sx) / dx;
			if (tLeft > 1E-3F) // Only positive value.
			{
				var yAtT = sy + tLeft * dy;
				if (yAtT + Epsilon >= top && yAtT - Epsilon <= bottom)
				{
					possibleTs.Add(tLeft);
				}
			}

			var tRight = (right - sx) / dx;
			if (tRight > 1E-3F)
			{
				var yAtT = sy + tRight * dy;
				if (yAtT + Epsilon >= top && yAtT - Epsilon <= bottom)
				{
					possibleTs.Add(tRight);
				}
			}
		}

		// Find intersection point Y value.
		if (Math.Abs(dy) > Epsilon)
		{
			var tTop = (top - sy) / dy;
			if (tTop > 1E-3F)
			{
				var xAtT = sx + tTop * dx;
				if (xAtT + Epsilon >= left && xAtT - Epsilon <= right)
				{
					possibleTs.Add(tTop);
				}
			}

			var tBottom = (bottom - sy) / dy;
			if (tBottom > 1E-3F)
			{
				var xAtT = sx + tBottom * dx;
				if (xAtT + Epsilon >= left && xAtT - Epsilon <= right)
				{
					possibleTs.Add(tBottom);
				}
			}
		}

		if (possibleTs.Count == 0)
		{
			// Fallback (but thought to be unnecessary).
			return anchor;
		}

		var tMin = float.PositiveInfinity;
		foreach (var t in possibleTs)
		{
			if (t > 0 && t <= tMin)
			{
				tMin = t;
			}
		}

		return new(sx + tMin * dx, sy + tMin * dy);
	}

	/// <summary>
	/// Calculates start and end point, with line extended.
	/// </summary>
	/// <param name="start">Represents point of start cell.</param>
	/// <param name="startAlignment">The start cell alignment.</param>
	/// <param name="interim">Represents point of interim cell.</param>
	/// <param name="interimCorner">The interim cell alignment.</param>
	/// <param name="cellSize">The cell size.</param>
	/// <param name="rowsCount">The number of rows.</param>
	/// <param name="columnsCount">The number of columns.</param>
	/// <param name="marginLeft">The margin X value.</param>
	/// <param name="marginTop">The margin Y value.</param>
	/// <returns>The pair of points.</returns>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Throws when either variable <paramref name="startAlignment"/> or <paramref name="interimCorner"/> is not defined.
	/// </exception>
	public static (SKPoint Start, SKPoint End) ComputeExtendedLine(
		SKPoint start,
		Alignment startAlignment,
		SKPoint interim,
		Alignment interimCorner,
		float cellSize,
		Absolute rowsCount,
		Absolute columnsCount,
		float marginLeft,
		float marginTop
	)
	{
		var tableRight = marginLeft + columnsCount * cellSize;
		var tableBottom = marginTop + rowsCount * cellSize;
		var ((startCellLeft, startCellTop), (interimCellLeft, interimCellTop)) = (start, interim);
		var startPoint = getPoint(startCellLeft, startCellTop, cellSize, startAlignment);
		var interimPoint = getPoint(interimCellLeft, interimCellTop, cellSize, interimCorner);
		var endPoint = ExtendRayToRectangleBoundary(startPoint, interimPoint, marginLeft, marginTop, tableRight, tableBottom);
		return (startPoint, endPoint);


		static SKPoint getPoint(float cellLeft, float cellTop, float cellSize, Alignment alignment)
			=> alignment switch
			{
				Alignment.TopLeft => new(cellLeft, cellTop),
				Alignment.TopRight => new(cellLeft + cellSize, cellTop),
				Alignment.BottomLeft => new(cellLeft, cellTop + cellSize),
				Alignment.BottomRight => new(cellLeft + cellSize, cellTop + cellSize),
				_ => throw new ArgumentOutOfRangeException(nameof(alignment)),
			};
	}
}
