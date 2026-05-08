namespace Sudoku.Graphics.Items.CellPairTextMarks;

/// <summary>
/// Represents a cell pair text mark item.
/// </summary>
public abstract record CellPairTextMarkItem : Item, IItem_FontRelatedProperties, IItem_MarkRelatedProperties, IItem_CellPairProperty
{
	/// <inheritdoc/>
	public required string FontName { get; init; }

	/// <inheritdoc/>
	public SKFontStyleWeight FontWeight { get; init; } = SKFontStyleWeight.Normal;

	/// <inheritdoc/>
	public SKFontStyleWidth FontWidth { get; init; } = SKFontStyleWidth.Normal;

	/// <inheritdoc/>
	public SKFontStyleSlant FontSlant { get; init; } = SKFontStyleSlant.Upright;

	/// <inheritdoc/>
	public required Absolute Cell1 { get; init; }

	/// <inheritdoc/>
	public required Absolute Cell2 { get; init; }

	/// <summary>
	/// Indicates padding of the boundary of text drawn.
	/// </summary>
	public Thickness<float> Padding { get; init; } = new(0);

	/// <summary>
	/// Indiactes the offset to the text to be drawn.
	/// </summary>
	public SKPoint Offset { get; init; } = new(0, 0);

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

	/// <summary>
	/// Represents rotation degrees table.
	/// </summary>
	public IDictionary<Direction8, float> RotationDegreesLookup { get; } = new Dictionary<Direction8, float>();

	/// <summary>
	/// Indicates the printing text.
	/// </summary>
	protected abstract string PrintingText { get; }

	/// <inheritdoc/>
	Scale IItem_MarkRelatedProperties.SizeScale { get => FontSizeScale; init => FontSizeScale = value; }


	/// <inheritdoc/>
	protected internal sealed override void DrawTo(Canvas canvas)
	{
		var mapper = canvas.Mapper;
		var cellSize = mapper.CellSize;
		var center = mapper.GetPointBetweenWithAdjacentRelation(Cell1, Cell2, out var adjacentRelation);
		var rotationDegree = RotationDegreesLookup.TryGetValue(adjacentRelation, out var d) ? d : 0;

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

		canvas.BackingCanvas.DrawTextWithCover(
			center.AlignYAsBaseline(font),
			PrintingText,
			SKTextAlign.Center,
			CoverStyle.Rectangle,
			font,
			textStrokePaint,
			coverStrokePaint,
			coverFillPaint,
			Padding,
			Offset,
			rotationDegree
		);
	}
}
