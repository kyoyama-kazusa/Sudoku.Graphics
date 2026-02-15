namespace Sudoku.Graphics.LineTemplates;

/// <summary>
/// Represents a default line template that doesn't contain any block (thick) lines (except borders).
/// </summary>
public sealed class DefaultLineTemplate : LineTemplate
{
	/// <summary>
	/// Indicates whether border lines should be drawn as thick lines or not.
	/// By default it's <see langword="true"/>.
	/// </summary>
	public bool DrawBorders { get; init; } = true;


	/// <inheritdoc/>
	public override void DrawLines(PointMapper mapper, SKCanvas canvas, CanvasDrawingOptions options)
	{
		drawBorderRectangle();
		drawGridLines();


		void drawBorderRectangle()
		{
			if (DrawBorders)
			{
				var path = new SKPath();
				path.AddRoundRect(
					new(
						SKRect.Create(
							mapper.Margin + mapper.CellSize * mapper.Vector.Left,
							mapper.Margin + mapper.CellSize * mapper.Vector.Up,
							mapper.GridSize.Width,
							mapper.GridSize.Height
						),
						options.GridBorderRoundedRectangleCornerRadius.Resolve(options).Measure(mapper.CellSize)
					)
				);
				using var borderPaint = new SKPaint
				{
					Style = SKPaintStyle.Stroke,
					Color = options.ThickLineColor.Resolve(options),
					StrokeWidth = (DrawBorders ? options.ThickLineWidth : options.ThickLineWidth)
						.Resolve(options)
						.Measure(mapper.CellSize),
					StrokeCap = SKStrokeCap.Round,
					IsAntialias = true
				};
				canvas.DrawPath(path, borderPaint);
			}
		}

		void drawGridLines()
		{
			using var thinLinePaint = new SKPaint
			{
				Style = SKPaintStyle.Stroke,
				Color = options.ThinLineColor.Resolve(options),
				StrokeWidth = options.ThinLineWidth.Resolve(options).Measure(mapper.CellSize),
				StrokeCap = SKStrokeCap.Round,
				IsAntialias = true
			};

			// Horizontal lines.
			for (var i = 1; i < mapper.RowsCount; i++)
			{
				var a = mapper.GetPoint(mapper.Vector.Up + i, (Absolute)mapper.Vector.Left, CellCornerType.TopLeft);
				var b = a + new SKPoint(mapper.ColumnsCount * mapper.CellSize, 0);
				canvas.DrawLine(a, b, thinLinePaint);
			}

			// Vertical lines.
			for (var i = 1; i < mapper.ColumnsCount; i++)
			{
				var a = mapper.GetPoint((Absolute)mapper.Vector.Up, mapper.Vector.Left + i, CellCornerType.TopLeft);
				var b = a + new SKPoint(0, mapper.RowsCount * mapper.CellSize);
				canvas.DrawLine(a, b, thinLinePaint);
			}
		}
	}
}
