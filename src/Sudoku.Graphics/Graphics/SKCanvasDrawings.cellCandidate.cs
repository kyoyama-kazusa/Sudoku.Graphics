namespace Sudoku.Graphics;

public partial class SKCanvasDrawings
{
	/// <param name="this">The current instance.</param>
	extension(SKCanvas @this)
	{
		/// <summary>
		/// Draws the specified text into the target cell.
		/// </summary>
		/// <inheritdoc cref="extension(SKCanvas).DrawOutlinedTextToCell"/>
		public void DrawTextToCell(
			string text,
			Absolute cell,
			string fontName,
			Scale fontScale,
			SKFontStyleWeight fontWeight,
			SKFontStyleWidth fontWidth,
			SKFontStyleSlant fontSlant,
			SerializableColor fillColor,
			float rotationDegree,
			Direction8 alignedDirection,
			PointMapper mapper
		) => @this.DrawOutlinedTextToCell(
			text,
			cell,
			fontName,
			fontScale,
			0M,
			fontWeight,
			fontWidth,
			fontSlant,
			SKColors.Transparent,
			fillColor,
			rotationDegree,
			alignedDirection,
			mapper
		);

		/// <summary>
		/// Draws the specified text into the target candidate.
		/// </summary>
		/// <inheritdoc cref="extension(SKCanvas).DrawOutlinedTextToCandidate"/>
		public void DrawTextToCandidate(
			string text,
			CandidatePosition candidatePosition,
			string fontName,
			Scale fontScale,
			SKFontStyleWeight fontWeight,
			SKFontStyleWidth fontWidth,
			SKFontStyleSlant fontSlant,
			SerializableColor fillColor,
			PointMapper mapper
		) => @this.DrawOutlinedTextToCandidate(
			text,
			candidatePosition,
			fontName,
			fontScale,
			0M,
			fontWeight,
			fontWidth,
			fontSlant,
			SKColors.Transparent,
			fillColor,
			mapper
		);

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

			using var typeface = SKTypeface.FromFamilyName(fontName, fontWeight, fontWidth, fontSlant);
			var factSize = fontScale.Measure(mapper.CellSize);
			using var textFont = new SKFont(typeface, factSize) { Subpixel = true };

			var center = mapper.GetPoint(cell, Alignment.Center);
			var targetPoint = center;
			if (alignedDirection != Direction8.None)
			{
				if (!alignedDirection.IsDiagonal)
				{
					throw new InvalidOperationException($"The specified direction '{alignedDirection}' is not supported.");
				}

				var quarterCellSize = mapper.CellSize / 4;
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

			// Baseline adjustment
			var textMetrics = textFont.Metrics;
			targetPoint += new SKPoint(0, (textMetrics.Ascent + textMetrics.Descent) / 2);

			// Centeralize
			targetPoint += new SKPoint(0, textFont.Size / 2);

			// Manual adjustment
			targetPoint += new SKPoint(0, mapper.CellSize / 8);

			var outlineStrokeWidth = outlineThicknessScale.Measure(factSize);
			if (outlineStrokeWidth != 0 && outlineColor.Alpha != 0)
			{
				using var textStrokePaint = new SKPaint
				{
					Style = SKPaintStyle.Stroke,
					Color = outlineColor,
					IsAntialias = true,
					StrokeWidth = outlineStrokeWidth,
					StrokeJoin = SKStrokeJoin.Round
				};

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
			var targetPoint = mapper.GetPoint(candidatePosition, Alignment.Center)
				+ new SKPoint(0, (textMetrics.Ascent + textMetrics.Descent) / 2) // Baseline adjustment
				+ new SKPoint(0, textFont.Size / 2) // Centeralize
				+ new SKPoint(0, candidateSize / 4); // Manual adjustment
			@this.DrawText(text, targetPoint, SKTextAlign.Center, textFont, textStrokePaint);
			@this.DrawText(text, targetPoint, SKTextAlign.Center, textFont, textFillPaint);
		}
	}
}
