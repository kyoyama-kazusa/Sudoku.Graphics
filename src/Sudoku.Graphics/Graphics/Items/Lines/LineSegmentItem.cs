namespace Sudoku.Graphics.Items.Lines;

/// <summary>
/// Represents a line segment item.
/// </summary>
/// <param name="_isThick">Indicates whether the item is thick line.</param>
public abstract record LineSegmentItem(bool _isThick) : LineItem, IItem_CellProperty, IItem_DirectionProperty<Direction4>
{
	/// <summary>
	/// Indicates whether the item is thick.
	/// </summary>
	private readonly bool _isThick = _isThick;


	/// <inheritdoc/>
	public sealed override ItemType Type => _isThick ? ItemType.LineSegment_Thick : ItemType.LineSegment_Thin;

	/// <inheritdoc/>
	public required abstract Direction4 Direction { get; init; }

	/// <inheritdoc/>
	public required abstract Absolute Cell { get; init; }

	/// <summary>
	/// Indicates the line width scale.
	/// </summary>
	public required abstract Scale LineWidthScale { get; init; }

	/// <summary>
	/// Indicates the line color.
	/// </summary>
	public required abstract SerializableColor LineColor { get; init; }

	/// <summary>
	/// Indicates the line dash sequence.
	/// </summary>
	public required abstract LineDashSequence LineDashSequence { get; init; }


	/// <inheritdoc/>
	protected internal sealed override void DrawTo(Canvas canvas)
	{
		var backingCanvas = canvas.BackingCanvas;
		var mapper = canvas.Mapper;
		using var lineStrokePaint = new SKPaint
		{
			Style = SKPaintStyle.Stroke,
			Color = LineColor,
			StrokeWidth = LineWidthScale.Measure(mapper.CellSize),
			StrokeCap = SKStrokeCap.Round,
			StrokeJoin = SKStrokeJoin.Round,
			IsAntialias = true,
			PathEffect = LineDashSequence.IsEmpty ? null : LineDashSequence
		};
		drawLine(
			mapper.GetPoint(Cell, Alignment.TopLeft),
			mapper.GetPoint(Cell, Alignment.TopRight),
			mapper.GetPoint(Cell, Alignment.BottomLeft),
			mapper.GetPoint(Cell, Alignment.BottomRight),
			lineStrokePaint
		);


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void drawLine(SKPoint topLeft, SKPoint topRight, SKPoint bottomLeft, SKPoint bottomRight, SKPaint paint)
		{
			if (Direction == Direction4.Up)
			{
				backingCanvas.DrawLine(topLeft, topRight, paint);
			}
			if (Direction == Direction4.Down)
			{
				backingCanvas.DrawLine(bottomLeft, bottomRight, paint);
			}
			if (Direction == Direction4.Left)
			{
				backingCanvas.DrawLine(topLeft, bottomLeft, paint);
			}
			if (Direction == Direction4.Right)
			{
				backingCanvas.DrawLine(topRight, bottomRight, paint);
			}
		}
	}
}
