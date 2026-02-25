namespace Sudoku.ComponentModel.Items.CellMarks;

/// <summary>
/// Represents cell text mark item.
/// </summary>
public abstract class CellTextMarkItem : CellMarkItem
{
	/// <summary>
	/// Indicates the printing text.
	/// </summary>
	protected abstract string PrintingText { get; }


	/// <inheritdoc/>
	protected internal sealed override void DrawTo(Canvas canvas)
	{
		var template = canvas.Templates[TemplateIndex];
		var mapper = template.Mapper;
		using var typeface = SKTypeface.FromFamilyName(
			TextFontName,
			((IItem_FontRelatedProperties)this).FontWeight,
			((IItem_FontRelatedProperties)this).FontWidth,
			((IItem_FontRelatedProperties)this).FontSlant
		);
		var factSize = SizeScale.Measure(mapper.CellSize);
		using var textFont = new SKFont(typeface, factSize) { Subpixel = true };
		using var textStrokePaint = new SKPaint
		{
			Style = SKPaintStyle.Stroke,
			Color = StrokeColor,
			IsAntialias = true,
			StrokeWidth = StrokeWidthScale.Measure(factSize),
			StrokeJoin = SKStrokeJoin.Round
		};
		using var textFillPaint = new SKPaint { Style = SKPaintStyle.Fill, Color = FillColor, IsAntialias = true };
		var textMetrics = textFont.Metrics;
		var targetPoint = mapper.GetPoint(Cell, Alignment.Center)
			+ new SKPoint(0, (textMetrics.Ascent + textMetrics.Descent) / 2) // Baseline adjustment
			+ new SKPoint(0, textFont.Size / 2) // Centeralize
			+ new SKPoint(0, mapper.CellSize / 6); // Manual adjustment
		canvas.BackingCanvas.DrawText(PrintingText, targetPoint, SKTextAlign.Center, textFont, textStrokePaint);
		canvas.BackingCanvas.DrawText(PrintingText, targetPoint, SKTextAlign.Center, textFont, textFillPaint);
	}
}
