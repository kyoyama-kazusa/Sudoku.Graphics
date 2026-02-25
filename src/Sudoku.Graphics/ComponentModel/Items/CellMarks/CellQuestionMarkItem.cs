namespace Sudoku.ComponentModel.Items.CellMarks;

/// <summary>
/// Represents cell question mark item.
/// </summary>
public sealed class CellQuestionMarkItem : CellMarkItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellMark_Question;

	/// <inheritdoc/>
	protected override Type EqualityContract => typeof(CellQuestionMarkItem);


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		const string text = "?";

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
		var offset = textFont.MeasureText(text, textStrokePaint);
		var targetPoint = mapper.GetPoint(Cell, Alignment.Center)
			+ new SKPoint(0, offset / (2 * text.Length)) // Offset adjustment
			+ new SKPoint(0, mapper.CellSize / 12); // Manual adjustment
		canvas.BackingCanvas.DrawText(text, targetPoint, SKTextAlign.Center, textFont, textStrokePaint);
		canvas.BackingCanvas.DrawText(text, targetPoint, SKTextAlign.Center, textFont, textFillPaint);
	}
}
