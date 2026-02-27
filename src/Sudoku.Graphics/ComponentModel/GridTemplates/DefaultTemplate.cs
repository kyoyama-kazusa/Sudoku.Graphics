namespace Sudoku.ComponentModel.GridTemplates;

/// <summary>
/// Represents a default grid template that doesn't contain any block (thick) lines, except borders.
/// </summary>
public sealed class DefaultTemplate : Template, ITemplate_RoundedRectangleRelatedProperties
{
	/// <inheritdoc/>
	public bool IsBorderRoundedRectangle { get; init; } = true;

	/// <summary>
	/// Indicates whether border lines should be drawn as thick lines or not.
	/// By default it's <see langword="true"/>.
	/// </summary>
	public bool DrawBordersAsThickLines { get; init; } = true;

	/// <inheritdoc/>
	public Scale BorderCornerRadius { get; init; }


	/// <inheritdoc/>
	protected override void DrawBorderRectangle(SKCanvas canvas)
	{
		var path = new SKPath();
		path.AddRoundRect(new(Mapper.GridSize, IsBorderRoundedRectangle ? BorderCornerRadius.Measure(Mapper.CellSize) : 0));

		using var borderPaint = DrawBordersAsThickLines ? CreateThickLinesPaint() : CreateThinLinesPaint();
		canvas.DrawPath(path, borderPaint);
	}

	/// <inheritdoc/>
	protected override void DrawGridLines(SKCanvas canvas)
	{
		using var thinLinePaint = CreateThinLinesPaint();

		// Horizontal lines.
		for (var i = 1; i < Mapper.RowsCount; i++)
		{
			var a = Mapper.GetPoint(Mapper.Vector.Up + i, (Absolute)Mapper.Vector.Left, Alignment.TopLeft);
			var b = a + new SKPoint(Mapper.ColumnsCount * Mapper.CellSize, 0);
			canvas.DrawLine(a, b, thinLinePaint);
		}

		// Vertical lines.
		for (var i = 1; i < Mapper.ColumnsCount; i++)
		{
			var a = Mapper.GetPoint((Absolute)Mapper.Vector.Up, Mapper.Vector.Left + i, Alignment.TopLeft);
			var b = a + new SKPoint(0, Mapper.RowsCount * Mapper.CellSize);
			canvas.DrawLine(a, b, thinLinePaint);
		}
	}
}
