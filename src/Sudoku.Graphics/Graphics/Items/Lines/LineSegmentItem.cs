namespace Sudoku.Graphics.Items.Lines;

/// <summary>
/// Represents a line segment item.
/// </summary>
/// <param name="_isThick">Indicates whether the item is thick line.</param>
public abstract record LineSegmentItem(bool _isThick) : LineItem, IItem_CellPairProperty
{
	/// <summary>
	/// Indicates whether the item is thick.
	/// </summary>
	private readonly bool _isThick = _isThick;


	/// <inheritdoc/>
	public sealed override ItemType Type => _isThick ? ItemType.LineSegment_Thick : ItemType.LineSegment_Thin;

	/// <inheritdoc/>
	public required abstract Absolute Cell1 { get; init; }

	/// <inheritdoc/>
	public required abstract Absolute Cell2 { get; init; }

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
		var (cell1, cell2) = (Cell1, Cell2);
		if (cell1 > cell2)
		{
			(cell1, cell2) = (cell2, cell1);
		}

		var backingCanvas = canvas.BackingCanvas;
		var mapper = canvas.Mapper;
		if (Absolute.GetAdjacentRelation(cell1, cell2, mapper) is not ({ IsDiagonal: false } relation and not Direction8.None))
		{
			return;
		}

		var direction = relation.AsDirection4();
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
			mapper.GetPoint(cell2, Alignment.TopLeft),
			mapper.GetPoint(cell2, Alignment.TopRight),
			mapper.GetPoint(cell2, Alignment.BottomLeft),
			mapper.GetPoint(cell2, Alignment.BottomRight),
			direction,
			lineStrokePaint
		);


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void drawLine(SKPoint topLeft, SKPoint topRight, SKPoint bottomLeft, SKPoint bottomRight, Direction4 direction, SKPaint paint)
		{
			if (direction == Direction4.Up)
			{
				backingCanvas.DrawLine(topLeft, topRight, paint);
			}
			if (direction == Direction4.Down)
			{
				backingCanvas.DrawLine(bottomLeft, bottomRight, paint);
			}
			if (direction == Direction4.Left)
			{
				backingCanvas.DrawLine(topLeft, bottomLeft, paint);
			}
			if (direction == Direction4.Right)
			{
				backingCanvas.DrawLine(topRight, bottomRight, paint);
			}
		}
	}
}
