namespace Sudoku.Graphics.Items.CellPairTextMarks;

/// <summary>
/// Represents a cell pair text mark item.
/// </summary>
public abstract record CellPairTextMarkItem : Item, IItem_FontRelatedProperties, IItem_MarkRelatedProperties, IItem_TemplateIndexProperty
{
	/// <summary>
	/// Indicates the padding top of the boundary of text drawn.
	/// </summary>
	public float PaddingTop { get; init; } = 0;

	/// <summary>
	/// Indicates the padding bottom of the boundary of text drawn.
	/// </summary>
	public float PaddingBottom { get; init; } = 0;

	/// <summary>
	/// Indicates the padding left of the boundary of text drawn.
	/// </summary>
	public float PaddingLeft { get; init; } = 0;

	/// <summary>
	/// Indicates the padding right of the boundary of text drawn.
	/// </summary>
	public float PaddingRight { get; init; } = 0;

	/// <summary>
	/// The X value of offset to the text to be drawn.
	/// </summary>
	public float OffsetX { get; init; } = 0;

	/// <summary>
	/// The Y value of offset to the text to be drawn.
	/// </summary>
	public float OffsetY { get; init; } = 0;

	/// <inheritdoc/>
	public required int TemplateIndex { get; init; }

	/// <inheritdoc/>
	public SKFontStyleWeight FontWeight { get; init; } = SKFontStyleWeight.Normal;

	/// <inheritdoc/>
	public SKFontStyleWidth FontWidth { get; init; } = SKFontStyleWidth.Normal;

	/// <inheritdoc/>
	public SKFontStyleSlant FontSlant { get; init; } = SKFontStyleSlant.Upright;

	/// <summary>
	/// Indicates the cell 1.
	/// </summary>
	public required Absolute Cell1 { get; init; }

	/// <summary>
	/// Indicates the cell 2.
	/// </summary>
	public required Absolute Cell2 { get; init; }

	/// <inheritdoc/>
	public required Scale FontSizeScale { get; init; }

	/// <summary>
	/// Indicates stroke width scale.
	/// </summary>
	public Scale StrokeWidthScale { get; init; }

	/// <summary>
	/// Indicates font color.
	/// </summary>
	public SerializableColor FontColor { get; init; }

	/// <inheritdoc/>
	public SerializableColor StrokeColor { get; init; }

	/// <inheritdoc/>
	public required SerializableColor FillColor { get; init; }

	/// <inheritdoc/>
	public required string FontName { get; init; }

	/// <summary>
	/// Indicates the printing text.
	/// </summary>
	protected abstract string PrintingText { get; }

	/// <inheritdoc/>
	Scale IItem_MarkRelatedProperties.SizeScale { get => FontSizeScale; init => FontSizeScale = value; }


	/// <inheritdoc/>
	protected internal sealed override void DrawTo(Canvas canvas)
	{
		var mapper = canvas.Templates[TemplateIndex].Mapper;
		var cellSize = mapper.CellSize;
		var center = mapper.GetPointBetweenWithAdjacentRelation(Cell1, Cell2, out _);

		using var typeface = SKTypeface.FromFamilyName(FontName, FontWeight, FontWidth, FontSlant);
		var fontSize = FontSizeScale.Measure(cellSize);
		using var font = new SKFont(typeface, fontSize) { Subpixel = true };
		using var textStrokePaint = FontColor.Alpha == 0 ? null : new SKPaint
		{
			IsAntialias = true,
			Style = SKPaintStyle.Fill,
			Color = FontColor,
			StrokeWidth = fontSize,
			StrokeCap = SKStrokeCap.Round,
			StrokeJoin = SKStrokeJoin.Round
		};
		using var coverStrokePaint = StrokeColor.Alpha == 0 ? null : new SKPaint
		{
			IsAntialias = true,
			Style = SKPaintStyle.Stroke,
			Color = StrokeColor,
			StrokeWidth = StrokeWidthScale.Measure(cellSize),
			StrokeCap = SKStrokeCap.Round,
			StrokeJoin = SKStrokeJoin.Round
		};
		using var coverFillPaint = FillColor.Alpha == 0 ? null : new SKPaint
		{
			IsAntialias = true,
			Style = SKPaintStyle.Fill,
			Color = FillColor
		};

		var textMetrics = font.Metrics;
		canvas.BackingCanvas.DrawTextWithCover(
			center
				+ new SKPoint(0, (textMetrics.Ascent + textMetrics.Descent) / 2) // Baseline adjustment.
				+ new SKPoint(0, font.Size / 2), // Centeralize.
			PrintingText,
			SKTextAlign.Center,
			CoverStyle.Rectangle,
			font,
			textStrokePaint,
			coverStrokePaint,
			coverFillPaint,
			PaddingTop,
			PaddingBottom,
			PaddingLeft,
			PaddingRight,
			new(OffsetX, OffsetY)
		);
	}
}
