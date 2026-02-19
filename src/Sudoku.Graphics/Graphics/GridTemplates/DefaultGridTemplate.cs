namespace Sudoku.Graphics.GridTemplates;

/// <summary>
/// Represents a default grid template that doesn't contain any block (thick) lines, except borders.
/// </summary>
/// <param name="mapper"><inheritdoc cref="GridTemplate(PointMapper)" path="/param[@name='mapper']"/></param>
[method: JsonConstructor]
public sealed class DefaultGridTemplate(PointMapper mapper) : IndividualGridTemplate(mapper)
{
	/// <summary>
	/// Indicates whether border lines should be drawn as thick lines or not.
	/// By default it's <see langword="true"/>.
	/// </summary>
	public bool DrawBordersAsThickLines { get; init; } = true;


	/// <inheritdoc/>
	protected override void GuardStatements(SKCanvas canvas, CanvasDrawingOptions options)
	{
	}

	/// <inheritdoc/>
	protected override void DrawBorderRectangle(SKCanvas canvas, CanvasDrawingOptions options)
	{
		var path = new SKPath();
		path.AddRoundRect(
			new(
				SKRect.Create(
					Mapper.Margin + Mapper.CellSize * Mapper.Vector.Left,
					Mapper.Margin + Mapper.CellSize * Mapper.Vector.Up,
					Mapper.GridDrawingSize.Width,
					Mapper.GridDrawingSize.Height
				),
				options.GridBorderRoundedRectangleCornerRadius.Resolve(options).Measure(Mapper.CellSize)
			)
		);

		using var borderPaint = DrawBordersAsThickLines ? CreateThickLinesPaint(options) : CreateThinLinesPaint(options);
		canvas.DrawPath(path, borderPaint);
	}

	/// <inheritdoc/>
	protected override void DrawGridLines(SKCanvas canvas, CanvasDrawingOptions options)
	{
		using var thinLinePaint = CreateThinLinesPaint(options);

		// Horizontal lines.
		for (var i = 1; i < Mapper.RowsCount; i++)
		{
			var a = Mapper.GetPoint(Mapper.Vector.Up + i, (Absolute)Mapper.Vector.Left, CellAlignment.TopLeft);
			var b = a + new SKPoint(Mapper.ColumnsCount * Mapper.CellSize, 0);
			canvas.DrawLine(a, b, thinLinePaint);
		}

		// Vertical lines.
		for (var i = 1; i < Mapper.ColumnsCount; i++)
		{
			var a = Mapper.GetPoint((Absolute)Mapper.Vector.Up, Mapper.Vector.Left + i, CellAlignment.TopLeft);
			var b = a + new SKPoint(0, Mapper.RowsCount * Mapper.CellSize);
			canvas.DrawLine(a, b, thinLinePaint);
		}
	}
}
