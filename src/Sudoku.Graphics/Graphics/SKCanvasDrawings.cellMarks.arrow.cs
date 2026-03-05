namespace Sudoku.Graphics;

public partial class SKCanvasDrawings
{
	extension(SKCanvas @this)
	{
		/// <summary>
		/// Draws apex triangle to specified cell.
		/// </summary>
		/// <param name="cell">The cell.</param>
		/// <param name="cornerAlignment">The corner alignment.</param>
		/// <param name="paddingScale">The scale of padding, related to cell size.</param>
		/// <param name="lengthScale">The scale of length of triangle, related to cell size.</param>
		/// <param name="strokeColor">The stroke color.</param>
		/// <param name="strokeWidthScale">The stroke width scale, related to cell size.</param>
		/// <param name="fillColor">The fill color.</param>
		/// <param name="mapper">The mapper instance.</param>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Throws when <paramref name="cornerAlignment"/> is not defined.
		/// </exception>
		public void DrawApexTriangleToCell(
			Absolute cell,
			Alignment cornerAlignment,
			Scale paddingScale,
			Scale lengthScale,
			SerializableColor strokeColor,
			Scale strokeWidthScale,
			SerializableColor fillColor,
			PointMapper mapper
		)
		{
			var cellSize = mapper.CellSize;
			var offset = paddingScale.Measure(cellSize);
			var (x, y) = mapper.GetPoint(cell, Alignment.TopLeft);
			var (rx, ry) = cornerAlignment switch
			{
				Alignment.TopLeft => (x + offset, y + offset),
				Alignment.TopRight => (x + cellSize - offset, y + offset),
				Alignment.BottomLeft => (x + offset, y + cellSize - offset),
				Alignment.BottomRight => (x + cellSize - offset, y + cellSize - offset),
				_ => throw new ArgumentOutOfRangeException(nameof(cornerAlignment))
			};
			var len = lengthScale.Measure(cellSize);
			var (x1, y1, x2, y2) = cornerAlignment switch
			{
				Alignment.TopLeft => (rx + len, ry, rx, ry + len),
				Alignment.TopRight => (rx - len, ry, rx, ry + len),
				Alignment.BottomLeft => (rx + len, ry, rx, ry - len),
				Alignment.BottomRight => (rx - len, ry, rx, ry - len),
				_ => throw new ArgumentOutOfRangeException(nameof(cornerAlignment))
			};

			using var path = new SKPath();
			path.MoveTo(rx, ry);
			path.LineTo(x1, y1);
			path.LineTo(x2, y2);
			path.Close();

			using var fillPaint = new SKPaint { Style = SKPaintStyle.Fill, Color = fillColor, IsAntialias = true };
			@this.DrawPath(path, fillPaint);
			var strokeWidth = strokeWidthScale.Measure(cellSize);
			if (strokeWidth != 0 && strokeColor.Alpha != 0)
			{
				using var strokePaint = new SKPaint
				{
					Style = SKPaintStyle.Stroke,
					Color = strokeColor,
					StrokeWidth = strokeWidth,
					IsAntialias = true
				};
				@this.DrawPath(path, strokePaint);
			}
		}

		/// <summary>
		/// Draws an arrow symbol into the specified cell.
		/// </summary>
		/// <param name="cell">The cell.</param>
		/// <param name="direction">The direction.</param>
		/// <param name="triangleWidthScale">The triangle width scale, related to cell size.</param>
		/// <param name="triangleHeightScale">The triangle height scale, related to cell size.</param>
		/// <param name="shaftWidthScale">The shaft width scale, related to cell size.</param>
		/// <param name="shaftHeightScale">The shaft height scale, related to cell size.</param>
		/// <param name="strokeWidthScale">The stroke width scale, related to cell size.</param>
		/// <param name="strokeColor">The stroke coloor.</param>
		/// <param name="fillColor">The fill color.</param>
		/// <param name="mapper">The mapper instance.</param>
		public void DrawArrowToCell(
			Absolute cell,
			Direction8 direction,
			Scale triangleWidthScale,
			Scale triangleHeightScale,
			Scale shaftWidthScale,
			Scale shaftHeightScale,
			Scale strokeWidthScale,
			SerializableColor strokeColor,
			SerializableColor fillColor,
			PointMapper mapper
		)
		{
			var cellSize = mapper.CellSize;
			var (x, y) = mapper.GetPoint(cell, Alignment.TopLeft);
			var cellRect = new SKRect(x, y, x + cellSize, y + cellSize);
			var strokeWidth = strokeWidthScale.Measure(cellSize);
			using var fillPaint = new SKPaint { Style = SKPaintStyle.Fill, IsAntialias = true, Color = fillColor };
			using var strokePaint = new SKPaint
			{
				Style = SKPaintStyle.Stroke,
				IsAntialias = true,
				StrokeWidth = strokeWidth,
				StrokeJoin = SKStrokeJoin.Round,
				StrokeCap = SKStrokeCap.Round,
				Color = strokeColor
			};

			using var arrowPath = ArrowPainterHelper.CreateArrowPath(
				cellRect,
				triangleWidthScale,
				triangleHeightScale,
				shaftWidthScale,
				shaftHeightScale,
				strokeWidth,
				direction
			);
			@this.DrawPath(arrowPath, fillPaint);
			@this.DrawPath(arrowPath, strokePaint);
		}

		/// <summary>
		/// Draws an arrow triangle into the specifie cell.
		/// </summary>
		/// <param name="cell">The cell.</param>
		/// <param name="direction">The direction.</param>
		/// <param name="sizeScale">The scale of size, related to cell size.</param>
		/// <param name="baseScale">The scale of base line, related to cell size.</param>
		/// <param name="strokeWidthScale">The stroke width scale, related to cell size.</param>
		/// <param name="strokeColor">The stroke color.</param>
		/// <param name="fillColor">The fill color.</param>
		/// <param name="mapper">The mapper instance.</param>
		public void DrawArrowTriangleToCell(
			Absolute cell,
			Direction8 direction,
			Scale sizeScale,
			Scale baseScale,
			Scale strokeWidthScale,
			SerializableColor strokeColor,
			SerializableColor fillColor,
			PointMapper mapper
		)
		{
			var cellSize = mapper.CellSize;
			var (x, y) = mapper.GetPoint(cell, Alignment.TopLeft);
			var cellRect = new SKRect(x, y, x + cellSize, y + cellSize);
			var strokeWidth = strokeWidthScale.Measure(cellSize);
			using var fillPaint = new SKPaint { Style = SKPaintStyle.Fill, IsAntialias = true, Color = fillColor };
			using var strokePaint = new SKPaint
			{
				Style = SKPaintStyle.Stroke,
				IsAntialias = true,
				StrokeWidth = strokeWidth,
				StrokeJoin = SKStrokeJoin.Round,
				StrokeCap = SKStrokeCap.Round,
				Color = strokeColor
			};

			using var arrowPath = ArrowPainterHelper.CreateArrowTrianglePath(cellRect, sizeScale, strokeWidth, direction, baseScale);
			@this.DrawPath(arrowPath, fillPaint);
			@this.DrawPath(arrowPath, strokePaint);
		}
	}
}

/// <summary>
/// The helper type that draws for arrows.
/// </summary>
file static class ArrowPainterHelper
{
	/// <summary>
	/// Creates a path of arrow, pointing to the specified direction.
	/// </summary>
	/// <param name="cell">The cell rectangle.</param>
	/// <param name="sizeScale">The scale of size.</param>
	/// <param name="strokeWidth">The stroke width.</param>
	/// <param name="direction">The direction.</param>
	/// <param name="baseScale">The base scale.</param>
	/// <returns>
	/// A <see cref="SKPath"/> instance.
	/// Note: You must call <see cref="SKNativeObject.Dispose()"/> method manually
	/// or use <see langword="using"/> statements to release resources.
	/// </returns>
	/// <seealso cref="SKPath"/>
	/// <seealso cref="SKNativeObject.Dispose()"/>
	public static SKPath CreateArrowTrianglePath(SKRect cell, Scale sizeScale, float strokeWidth, Direction8 direction, Scale baseScale)
	{
		var cellSize = Math.Min(cell.Width, cell.Height);
		var halfStroke = strokeWidth / 2;
		var insetRect = new SKRect(cell.Left + halfStroke, cell.Top + halfStroke, cell.Right - halfStroke, cell.Bottom - halfStroke);
		var insetCellSize = Math.Min(insetRect.Width, insetRect.Height);
		var desiredLength = sizeScale.Measure(cellSize);
		var effectiveLength = Math.Min(desiredLength, insetCellSize);
		if (effectiveLength <= 0)
		{
			effectiveLength = 1F;
		}

		var cx = (insetRect.Left + insetRect.Right) / 2;
		var cy = (insetRect.Top + insetRect.Bottom) / 2;
		var halfLength = effectiveLength / 2;
		var baseWidth = baseScale.Measure(effectiveLength);
		var tip = new SKPoint(cx, cy - halfLength);
		var baseLeft = new SKPoint(cx - baseWidth / 2, cy + halfLength);
		var baseRight = new SKPoint(cx + baseWidth / 2, cy + halfLength);
		var angleDeg = direction.AngleDegrees;
		var rtTip = rotateAround(tip, new(cx, cy), angleDeg);
		var rtBL = rotateAround(baseLeft, new(cx, cy), angleDeg);
		var rtBR = rotateAround(baseRight, new(cx, cy), angleDeg);
		var path = new SKPath { FillType = SKPathFillType.EvenOdd };
		path.MoveTo(rtTip);
		path.LineTo(rtBL);
		path.LineTo(rtBR);
		path.Close();
		return path;


		static SKPoint rotateAround(SKPoint p, SKPoint center, float degreesClockwise)
		{
			var rad = degreesClockwise * MathF.PI / 180;
			var cosine = MathF.Cos(rad);
			var sine = MathF.Sin(rad);
			var dx = p.X - center.X;
			var dy = p.Y - center.Y;
			var rx = dx * cosine + dy * sine;
			var ry = -dx * sine + dy * cosine;
			return new(center.X + rx, center.Y + ry);
		}
	}

	/// <summary>
	/// Draws arrow path.
	/// </summary>
	/// <param name="cellRect">The cell rectangle.</param>
	/// <param name="triangleWidthScale">The triangle width scale, related to cell size.</param>
	/// <param name="triangleHeightScale">The triangle height scale, related to cell size.</param>
	/// <param name="shaftWidthScale">The shaft width scale, related to cell size.</param>
	/// <param name="shaftLengthScale">The shaft height scale, related to cell size.</param>
	/// <param name="strokeWidth">The stroke width.</param>
	/// <param name="direction">The direction.</param>
	/// <returns><inheritdoc cref="CreateArrowTrianglePath(SKRect, Scale, float, Direction8, Scale)" path="/returns"/></returns>
	/// <exception cref="InvalidOperationException">Throws when shaft width is greater than triangle width.</exception>
	public static SKPath CreateArrowPath(
		SKRect cellRect,
		Scale triangleWidthScale,
		Scale triangleHeightScale,
		Scale shaftWidthScale,
		Scale shaftLengthScale,
		float strokeWidth,
		Direction8 direction
	)
	{
		var cellSize = Math.Min(cellRect.Width, cellRect.Height);
		var halfStroke = strokeWidth / 2;
		var insetRect = new SKRect(cellRect.Left + halfStroke, cellRect.Top + halfStroke, cellRect.Right - halfStroke, cellRect.Bottom - halfStroke);
		var insetCellSize = Math.Min(insetRect.Width, insetRect.Height);
		if (insetCellSize <= 0)
		{
			insetCellSize = 1;
		}

		var triangleWidth = triangleWidthScale.Measure(cellSize);
		var triangleHeight = triangleHeightScale.Measure(cellSize);
		var shaftWidth = shaftWidthScale.Measure(cellSize);
		var shaftHeight = shaftLengthScale.Measure(cellSize);
		var totalLength = triangleHeight + shaftHeight;
		if (totalLength > insetCellSize)
		{
			var scale = insetCellSize / totalLength;
			triangleWidth *= scale;
			triangleHeight *= scale;
			shaftWidth *= scale;
			shaftHeight *= scale;
			totalLength = triangleHeight + shaftHeight;
		}

		triangleHeight = Math.Max(triangleHeight, 1F);
		triangleWidth = Math.Max(triangleWidth, 1F);
		shaftWidth = Math.Max(shaftWidth, .5F);

		var cx = (insetRect.Left + insetRect.Right) / 2;
		var cy = (insetRect.Top + insetRect.Bottom) / 2;
		var halfTotal = totalLength / 2;
		var tipY = -halfTotal;
		var baseY = tipY + triangleHeight;
		var shaftBottomY = baseY + shaftHeight;
		var halfTriangleWidth = triangleWidth / 2;
		var halfShaftWidth = shaftWidth / 2;
		if (halfShaftWidth > halfTriangleWidth)
		{
			throw new InvalidOperationException("Invalid size of shaft size - it is greater than triangle!");
		}

		var tipPoint = new SKPoint(0, tipY);
		var baseLeftPoint = new SKPoint(-halfTriangleWidth, baseY);
		var shaftBottomLeftPoint = new SKPoint(-halfShaftWidth, shaftBottomY);
		var shaftBottomRightPoint = new SKPoint(halfShaftWidth, shaftBottomY);
		var baseRightPoint = new SKPoint(halfTriangleWidth, baseY);
		var shaftTopLeftPoint = new SKPoint(-halfShaftWidth, baseY);
		var shaftTopRightPoint = new SKPoint(halfShaftWidth, baseY);
		var angleDegree = direction.AngleDegrees;

		var path = new SKPath { FillType = SKPathFillType.EvenOdd };
		path.MoveTo(RotateAndTranslate(tipPoint, angleDegree, cx, cy));
		path.LineTo(RotateAndTranslate(baseLeftPoint, angleDegree, cx, cy));
		path.LineTo(RotateAndTranslate(shaftTopLeftPoint, angleDegree, cx, cy));
		path.LineTo(RotateAndTranslate(shaftBottomLeftPoint, angleDegree, cx, cy));
		path.LineTo(RotateAndTranslate(shaftBottomRightPoint, angleDegree, cx, cy));
		path.LineTo(RotateAndTranslate(shaftTopRightPoint, angleDegree, cx, cy));
		path.LineTo(RotateAndTranslate(baseRightPoint, angleDegree, cx, cy));
		path.Close();
		return path;


		static SKPoint RotateAndTranslate(SKPoint p, float degreesClockwise, float tx, float ty)
		{
			var rad = degreesClockwise * MathF.PI / 180;
			var cosine = MathF.Cos(rad);
			var sine = MathF.Sin(rad);
			var rx = p.X * cosine + p.Y * sine;
			var ry = -p.X * sine + p.Y * cosine;
			return new(tx + rx, ty + ry);
		}
	}
}
