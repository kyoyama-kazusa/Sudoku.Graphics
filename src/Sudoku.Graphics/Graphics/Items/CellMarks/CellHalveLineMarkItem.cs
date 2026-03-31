namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents cell halve line mark item.
/// </summary>
public sealed record CellHalveLineMarkItem : CellMarkItem, IItem_OrientationProperty<Orientation4>
{
	/// <inheritdoc/>
	public required Orientation4 Orientation { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.Cell_HalveLine;


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var template = canvas.Templates[TemplateIndex];
		var mapper = template.Mapper;
		var cellSize = mapper.CellSize;
		var lineContainingBoxSize = SizeScale.Measure(cellSize);
		var halfPadding = (cellSize - lineContainingBoxSize) / 2;
		var topLeft = mapper.GetPoint(Cell, Alignment.TopLeft) + (+halfPadding, +halfPadding);
		var topRight = mapper.GetPoint(Cell, Alignment.TopRight) + (-halfPadding, +halfPadding);
		var bottomLeft = mapper.GetPoint(Cell, Alignment.BottomLeft) + (+halfPadding, -halfPadding);
		var bottomRight = mapper.GetPoint(Cell, Alignment.BottomRight) + (-halfPadding, -halfPadding);
		var top = topLeft + (lineContainingBoxSize / 2, 0);
		var bottom = bottomLeft + (lineContainingBoxSize / 2, 0);
		var left = topLeft + (0, lineContainingBoxSize / 2);
		var right = topRight + (0, lineContainingBoxSize / 2);
		var (start, end) = Orientation switch
		{
			Orientation4.Horizontal => (left, right),
			Orientation4.Vertical => (top, bottom),
			Orientation4.Slash => (topRight, bottomLeft),
			Orientation4.Backslash => (topLeft, bottomRight),
			_ => throw new InvalidOperationException($"{nameof(Orientation)} is invalid or not defined.")
		};

		var strokeWidth = StrokeWidthScale.Measure(cellSize);
		if (strokeWidth != 0 && StrokeColor.Alpha != 0)
		{
			using var strokePaint = new SKPaint
			{
				Style = SKPaintStyle.Stroke,
				Color = StrokeColor,
				StrokeWidth = strokeWidth,
				IsAntialias = true
			};
			canvas.BackingCanvas.DrawLine(start, end, strokePaint);
		}
	}
}
