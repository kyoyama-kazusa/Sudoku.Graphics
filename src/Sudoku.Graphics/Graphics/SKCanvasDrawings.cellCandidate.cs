namespace Sudoku.Graphics;

public partial class SKCanvasDrawings
{
	extension(SKCanvas @this)
	{
		/// <summary>
		/// Draws the specified text into the target cell, with outlined.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="cell">The cell.</param>
		/// <param name="fontName">The font name.</param>
		/// <param name="fontScale">The font scale, relative to cell size.</param>
		/// <param name="outlineThicknessScale">
		/// The outline width scale, relative to text fact size calculated from <paramref name="fontScale"/>.
		/// </param>
		/// <param name="fontWeight">The font weight.</param>
		/// <param name="fontWidth">The font width.</param>
		/// <param name="fontSlant">The font slant.</param>
		/// <param name="outlineColor">The outline color.</param>
		/// <param name="fillColor">The fill color of text.</param>
		/// <param name="rotationDegree">The rotation degrees, in angle.</param>
		/// <param name="alignedDirection">The aligned direction.</param>
		/// <param name="mapper">The mapper.</param>
		/// <exception cref="InvalidOperationException">
		/// Throws when <paramref name="alignedDirection"/> is not diagonally aligned.
		/// </exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Throws when <paramref name="alignedDirection"/> is not defined.
		/// </exception>
		public void DrawOutlinedTextToCell(
			string text,
			Absolute cell,
			string fontName,
			Scale fontScale,
			Scale outlineThicknessScale,
			SKFontStyleWeight fontWeight,
			SKFontStyleWidth fontWidth,
			SKFontStyleSlant fontSlant,
			SerializableColor outlineColor,
			SerializableColor fillColor,
			float rotationDegree,
			Direction8 alignedDirection,
			PointMapper mapper
		)
		{
			if (!Enum.IsDefined(alignedDirection))
			{
				throw new ArgumentOutOfRangeException(nameof(alignedDirection));
			}

			var cellSize = mapper.CellSize;
			using var typeface = SKTypeface.FromFamilyName(fontName, fontWeight, fontWidth, fontSlant);
			var factSize = fontScale.Measure(cellSize);
			using var textFont = new SKFont(typeface, factSize) { Subpixel = true };

			var center = mapper.GetPoint(cell, Alignment.Center);
			var targetPoint = center;
			if (alignedDirection != Direction8.None)
			{
				if (!alignedDirection.IsDiagonal)
				{
					throw new InvalidOperationException($"The specified direction '{alignedDirection}' is not supported.");
				}

				var quarterCellSize = cellSize / 4;
				targetPoint = alignedDirection switch
				{
					Direction8.LeftUp => targetPoint + new SKPoint(-quarterCellSize, -quarterCellSize),
					Direction8.RightUp => targetPoint + new SKPoint(+quarterCellSize, -quarterCellSize),
					Direction8.LeftDown => targetPoint + new SKPoint(-quarterCellSize, +quarterCellSize),
					Direction8.RightDown => targetPoint + new SKPoint(+quarterCellSize, +quarterCellSize),
					_ => throw new ArgumentOutOfRangeException(nameof(alignedDirection))
				};
			}

			if (rotationDegree != 0)
			{
				@this.Save();
				@this.RotateDegrees(rotationDegree, targetPoint.X, targetPoint.Y);
			}

			targetPoint = targetPoint.AlignYAsBaseline(textFont);

			var outlineStrokeWidth = outlineThicknessScale.Measure(factSize);
			using var textStrokePaint = new SKPaint
			{
				Style = SKPaintStyle.Stroke,
				Color = outlineColor,
				IsAntialias = true,
				StrokeWidth = outlineStrokeWidth,
				StrokeJoin = SKStrokeJoin.Round
			};

			// Set scale X of font.
			textFont.SetScaleX(textFont.MeasureText(text, textStrokePaint), cellSize);

			if (outlineStrokeWidth != 0 && outlineColor.Alpha != 0)
			{
				@this.DrawText(text, targetPoint, SKTextAlign.Center, textFont, textStrokePaint);
			}

			if (fillColor.Alpha != 0)
			{
				using var textFillPaint = new SKPaint { Style = SKPaintStyle.Fill, Color = fillColor, IsAntialias = true };
				@this.DrawText(text, targetPoint, SKTextAlign.Center, textFont, textFillPaint);
			}

			if (rotationDegree != 0)
			{
				@this.Restore();
			}
		}

		/// <summary>
		/// Draws the specified text into the target candidate, with outlined.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="candidatePosition">The candidate position.</param>
		/// <param name="fontName">The font name.</param>
		/// <param name="fontScale">The font scale, relative to cell size.</param>
		/// <param name="outlineThicknessScale">
		/// The outline width scale, relative to text fact size calculated from <paramref name="fontScale"/>.
		/// </param>
		/// <param name="fontWeight">The font weight.</param>
		/// <param name="fontWidth">The font width.</param>
		/// <param name="fontSlant">The font slant.</param>
		/// <param name="outlineColor">The outline color.</param>
		/// <param name="fillColor">The fill color of text.</param>
		/// <param name="mapper">The mapper.</param>
		public void DrawOutlinedTextToCandidate(
			string text,
			CandidatePosition candidatePosition,
			string fontName,
			Scale fontScale,
			Scale outlineThicknessScale,
			SKFontStyleWeight fontWeight,
			SKFontStyleWidth fontWidth,
			SKFontStyleSlant fontSlant,
			SerializableColor outlineColor,
			SerializableColor fillColor,
			PointMapper mapper
		)
		{
			var (_, subgridSize, _) = candidatePosition;
			using var typeface = SKTypeface.FromFamilyName(fontName, fontWeight, fontWidth, fontSlant);
			var factSize = fontScale.Measure(mapper.CellSize) / subgridSize;
			var candidateSize = mapper.CellSize / subgridSize;
			using var textFont = new SKFont(typeface, factSize) { Subpixel = true };
			using var textStrokePaint = new SKPaint
			{
				Style = SKPaintStyle.Stroke,
				Color = outlineColor,
				IsAntialias = true,
				StrokeWidth = outlineThicknessScale.Measure(factSize),
				StrokeJoin = SKStrokeJoin.Round
			};
			using var textFillPaint = new SKPaint { Style = SKPaintStyle.Fill, Color = fillColor, IsAntialias = true };
			var textMetrics = textFont.Metrics;
			var targetPoint = mapper.GetPoint(candidatePosition, Alignment.Center).AlignYAsBaseline(textFont);

			// Set scale X of font.
			textFont.SetScaleX(textFont.MeasureText(text, textStrokePaint), candidateSize);

			@this.DrawText(text, targetPoint, SKTextAlign.Center, textFont, textStrokePaint);
			@this.DrawText(text, targetPoint, SKTextAlign.Center, textFont, textFillPaint);
		}
	}
}
