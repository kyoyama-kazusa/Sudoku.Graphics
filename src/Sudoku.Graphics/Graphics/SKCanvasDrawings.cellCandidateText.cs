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
					Direction8.LeftUp => targetPoint + (-quarterCellSize, -quarterCellSize),
					Direction8.RightUp => targetPoint + (+quarterCellSize, -quarterCellSize),
					Direction8.LeftDown => targetPoint + (-quarterCellSize, +quarterCellSize),
					Direction8.RightDown => targetPoint + (+quarterCellSize, +quarterCellSize),
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

		/// <summary>
		/// Draws the specified text with background cover.
		/// </summary>
		/// <param name="point">The point.</param>
		/// <param name="text">The text.</param>
		/// <param name="textAlign">The text align.</param>
		/// <param name="coverStyle">The cover style.</param>
		/// <param name="font">The font.</param>
		/// <param name="textPaint">The paint.</param>
		/// <param name="coverStrokePaint">
		/// The stroke paint of cover background. The value can be <see langword="null"/> if you don't want to draw stroke.
		/// </param>
		/// <param name="coverFillPaint">The fill paint of cover background.</param>
		/// <param name="padding">The padding of the boundary of text drawn.</param>
		/// <param name="offset">The offset to the text to be drawn.</param>
		/// <exception cref="ArgumentOutOfRangeException">Throws when <paramref name="textAlign"/> is not defined.</exception>
		public void DrawTextWithCover(
			SKPoint point,
			string text,
			SKTextAlign textAlign,
			CoverStyle coverStyle,
			SKFont font,
			SKPaint? textPaint,
			SKPaint? coverStrokePaint,
			SKPaint? coverFillPaint,
			Thickness<float> padding,
			SKPoint offset
		)
		{
			if (string.IsNullOrWhiteSpace(text) || coverStyle == CoverStyle.None || !Enum.IsDefined(coverStyle)
				|| !Enum.IsDefined(textAlign)
				|| textPaint is null)
			{
				// Nothing to draw.
				return;
			}

			var drawPoint = point + offset;
			var textWidth = font.MeasureText(text, textPaint);
			font.MeasureText(text, out var bounds, textPaint);
			var alignedX = textAlign switch
			{
				SKTextAlign.Left => drawPoint.X,
				SKTextAlign.Center => drawPoint.X - textWidth / 2,
				SKTextAlign.Right => drawPoint.X - textWidth,
				_ => throw new ArgumentOutOfRangeException(nameof(textAlign))
			};
			bounds.Offset(alignedX, drawPoint.Y);
			var coverBounds = new SKRect(
				bounds.Left - padding.Left,
				bounds.Top - padding.Top,
				bounds.Right + padding.Right,
				bounds.Bottom + padding.Bottom
			);

			switch (coverStyle)
			{
				case CoverStyle.Rectangle or CoverStyle.Square:
				{
					if (coverStyle == CoverStyle.Square)
					{
						makeBoundsSquareOrCircle(ref coverBounds, textAlign);
					}
					if (coverStrokePaint is not null)
					{
						@this.DrawRect(coverBounds, coverStrokePaint);
					}
					if (coverFillPaint is not null)
					{
						@this.DrawRect(coverBounds, coverFillPaint);
					}
					break;
				}
				case CoverStyle.Oval or CoverStyle.Circle:
				{
					if (coverStyle == CoverStyle.Circle)
					{
						makeBoundsSquareOrCircle(ref coverBounds, textAlign);
					}
					if (coverStrokePaint is not null)
					{
						@this.DrawOval(coverBounds, coverStrokePaint);
					}
					if (coverFillPaint is not null)
					{
						@this.DrawOval(coverBounds, coverFillPaint);
					}
					break;
				}
				default:
				{
					throw new UnreachableException();
				}
			}

			@this.DrawText(text, drawPoint, textAlign, font, textPaint);


			static void makeBoundsSquareOrCircle(ref SKRect coverBounds, SKTextAlign textAlign)
			{
				var (left, top, right, bottom, width, height) = coverBounds;
				coverBounds = (width - height) switch
				{
					var coverBoundDelta and > 0 => coverBounds with
					{
						Top = top - coverBoundDelta / 2,
						Bottom = bottom + coverBoundDelta / 2
					},
					var coverBoundDelta and < 0 => textAlign switch
					{
						SKTextAlign.Left => coverBounds with { Right = right + -coverBoundDelta },
						SKTextAlign.Center => coverBounds with { Left = left - -coverBoundDelta / 2, Right = right + -coverBoundDelta / 2 },
						_ => coverBounds with { Left = left - -coverBoundDelta }
					},
					_ => coverBounds
				};
			}
		}
	}
}
