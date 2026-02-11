namespace Sudoku.Graphics.BlockLineTemplates;

/// <summary>
/// Represents a default (rectangular) block line template.
/// </summary>
/// <param name="rowBlockSize"><inheritdoc cref="RowBlockSize" path="/summary"/></param>
/// <param name="columnBlockSize"><inheritdoc cref="ColumnBlockSize" path="/summary"/></param>
public sealed class RectangularBlockLineTemplate(int rowBlockSize, int columnBlockSize) : BlockLineTemplate
{
	/// <summary>
	/// Initializes a <see cref="RectangularBlockLineTemplate"/> instance via the specified size as uniformed value.
	/// </summary>
	/// <param name="uniformSize">The uniformed value.</param>
	public RectangularBlockLineTemplate(int uniformSize) : this(uniformSize, uniformSize)
	{
	}


	/// <summary>
	/// Indicates the number of rows in a rectangular block.
	/// </summary>
	public int RowBlockSize { get; } = rowBlockSize;

	/// <summary>
	/// Indicates the number of columns in a rectangular block.
	/// </summary>
	public int ColumnBlockSize { get; } = columnBlockSize;


	/// <inheritdoc/>
	public override void DrawLines(PointMapper mapper, SKCanvas canvas, CanvasDrawingOptions options)
	{
		ArgumentException.Assert(mapper.RowsCount % RowBlockSize == 0);
		ArgumentException.Assert(mapper.ColumnsCount % ColumnBlockSize == 0);

		drawBorderRectangle();
		drawGridLines();


		void drawBorderRectangle()
		{
			var path = new SKPath();
			path.AddRoundRect(
				new(
					SKRectangle.Create(
						mapper.Margin + mapper.CellSize * mapper.Vector.Left,
						mapper.Margin + mapper.CellSize * mapper.Vector.Up,
						mapper.GridSizeRectangle.Width,
						mapper.GridSizeRectangle.Height
					),
					options.GridBorderRoundedRectangleCornerRadius.Measure(mapper.CellSize)
				)
			);
			using var borderPaint = new SKPaint
			{
				Style = SKPaintStyle.Stroke,
				Color = options.ThickLineColor,
				StrokeWidth = options.ThickLineWidth.Measure(mapper.CellSize),
				StrokeCap = SKStrokeCap.Round,
				IsAntialias = true
			};
			canvas.DrawPath(path, borderPaint);
		}

		void drawGridLines()
		{
			using var thickLinePaint = new SKPaint
			{
				Style = SKPaintStyle.Stroke,
				Color = options.ThickLineColor,
				StrokeWidth = options.ThickLineWidth.Measure(mapper.CellSize),
				StrokeCap = SKStrokeCap.Round,
				IsAntialias = true
			};
			using var thinLinePaint = new SKPaint
			{
				Style = SKPaintStyle.Stroke,
				Color = options.ThinLineColor,
				StrokeWidth = options.ThinLineWidth.Measure(mapper.CellSize),
				StrokeCap = SKStrokeCap.Round,
				IsAntialias = true
			};

			// Horizontal lines.
			for (var i = 1; i < mapper.RowsCount; i++)
			{
				var a = mapper.GetPoint(mapper.Vector.Up + i, mapper.Vector.Left, CellCornerType.TopLeft);
				var b = a + new SKPoint(mapper.ColumnsCount * mapper.CellSize, 0);
				canvas.DrawLine(a, b, i % RowBlockSize == 0 ? thickLinePaint : thinLinePaint);
			}

			// Vertical lines.
			for (var i = 1; i < mapper.ColumnsCount; i++)
			{
				var a = mapper.GetPoint(mapper.Vector.Up, mapper.Vector.Left + i, CellCornerType.TopLeft);
				var b = a + new SKPoint(0, mapper.RowsCount * mapper.CellSize);
				canvas.DrawLine(a, b, i % ColumnBlockSize == 0 ? thickLinePaint : thinLinePaint);
			}
		}
	}
}
