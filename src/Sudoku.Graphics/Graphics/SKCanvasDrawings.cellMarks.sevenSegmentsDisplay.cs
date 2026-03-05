namespace Sudoku.Graphics;

public partial class SKCanvasDrawings
{
	extension(SKCanvas @this)
	{
		/// <summary>
		/// Draws seven-segment display into the specified cell.
		/// </summary>
		/// <param name="cell">The cell.</param>
		/// <param name="value">The value.</param>
		/// <param name="showPhantomSegments">Indicates whether phantom segments will also be shown.</param>
		/// <param name="sizeScale">The scale of size, related to cell size.</param>
		/// <param name="strokeWidthScale">The scale of stroke width, related to cell size.</param>
		/// <param name="strokeColor">The stroke color.</param>
		/// <param name="fillColor">The fill color.</param>
		/// <param name="segmentRectWidthScale">The scale of width of every segment.</param>
		/// <param name="segmentRectHeightScale">The scale of height of every segment.</param>
		/// <param name="phantomStrokeWidthScale">The scale of phantom stroke width.</param>
		/// <param name="mapper">The mapper instance.</param>
		public void DrawSevenSegmentsDisplayToCell(
			Absolute cell,
			int value,
			bool showPhantomSegments,
			Scale sizeScale,
			Scale strokeWidthScale,
			SerializableColor strokeColor,
			SerializableColor fillColor,
			Scale segmentRectWidthScale,
			Scale segmentRectHeightScale,
			Scale phantomStrokeWidthScale,
			PointMapper mapper
		)
		{
			var cellSize = mapper.CellSize;
			var center = mapper.GetPoint(cell, Alignment.Center);
			var size = sizeScale.Measure(cellSize);
			var segmentRectWidth = segmentRectWidthScale.Measure(cellSize);
			var segmentRectHeight = segmentRectHeightScale.Measure(cellSize);
			var twoBoxesSize = size - segmentRectHeight;
			var boxSize = twoBoxesSize / 2;
			var gapSize = (boxSize - segmentRectWidth) / 2;
			var rectSize = segmentRectWidth - segmentRectHeight;
			var anchors = (SKPoint[])[
				new(center.X - boxSize / 2, center.Y - boxSize),
				new(center.X + boxSize / 2, center.Y - boxSize),
				new(center.X - boxSize / 2, center.Y),
				new(center.X + boxSize / 2, center.Y),
				new(center.X - boxSize / 2, center.Y + boxSize),
				new(center.X + boxSize / 2, center.Y + boxSize)
			];
			var points = (SKPoint[][])[
				[
					new(anchors[0].X + gapSize, anchors[0].Y),
					new(anchors[0].X + gapSize + segmentRectHeight / 2, anchors[0].Y - segmentRectHeight / 2),
					new(anchors[0].X + gapSize + segmentRectHeight / 2 + rectSize, anchors[0].Y - segmentRectHeight / 2),
					new(anchors[0].X + gapSize + segmentRectHeight + rectSize, anchors[0].Y),
					new(anchors[0].X + gapSize + segmentRectHeight / 2 + rectSize, anchors[0].Y + segmentRectHeight / 2),
					new(anchors[0].X + gapSize + segmentRectHeight / 2, anchors[0].Y + segmentRectHeight / 2)
				],
				[
					new(anchors[0].X, anchors[0].Y + gapSize),
					new(anchors[0].X + segmentRectHeight / 2, anchors[0].Y + gapSize + segmentRectHeight / 2),
					new(anchors[0].X + segmentRectHeight / 2, anchors[0].Y + gapSize + segmentRectHeight / 2 + rectSize),
					new(anchors[0].X, anchors[0].Y + gapSize + segmentRectHeight + rectSize),
					new(anchors[0].X - segmentRectHeight / 2, anchors[0].Y + gapSize + segmentRectHeight / 2 + rectSize),
					new(anchors[0].X - segmentRectHeight / 2, anchors[0].Y + gapSize + segmentRectHeight / 2)
				],
				[
					new(anchors[1].X, anchors[1].Y + gapSize),
					new(anchors[1].X + segmentRectHeight / 2, anchors[1].Y + gapSize + segmentRectHeight / 2),
					new(anchors[1].X + segmentRectHeight / 2, anchors[1].Y + gapSize + segmentRectHeight / 2 + rectSize),
					new(anchors[1].X, anchors[1].Y + gapSize + segmentRectHeight + rectSize),
					new(anchors[1].X - segmentRectHeight / 2, anchors[1].Y + gapSize + segmentRectHeight / 2 + rectSize),
					new(anchors[1].X - segmentRectHeight / 2, anchors[1].Y + gapSize + segmentRectHeight / 2)
				],
				[
					new(anchors[2].X + gapSize, anchors[2].Y),
					new(anchors[2].X + gapSize + segmentRectHeight / 2, anchors[2].Y - segmentRectHeight / 2),
					new(anchors[2].X + gapSize + segmentRectHeight / 2 + rectSize, anchors[2].Y - segmentRectHeight / 2),
					new(anchors[2].X + gapSize + segmentRectHeight + rectSize, anchors[2].Y),
					new(anchors[2].X + gapSize + segmentRectHeight / 2 + rectSize, anchors[2].Y + segmentRectHeight / 2),
					new(anchors[2].X + gapSize + segmentRectHeight / 2, anchors[2].Y + segmentRectHeight / 2)
				],
				[
					new(anchors[2].X, anchors[2].Y + gapSize),
					new(anchors[2].X + segmentRectHeight / 2, anchors[2].Y + gapSize + segmentRectHeight / 2),
					new(anchors[2].X + segmentRectHeight / 2, anchors[2].Y + gapSize + segmentRectHeight / 2 + rectSize),
					new(anchors[2].X, anchors[2].Y + gapSize + segmentRectHeight + rectSize),
					new(anchors[2].X - segmentRectHeight / 2, anchors[2].Y + gapSize + segmentRectHeight / 2 + rectSize),
					new(anchors[2].X - segmentRectHeight / 2, anchors[2].Y + gapSize + segmentRectHeight / 2)
				],
				[
					new(anchors[3].X, anchors[3].Y + gapSize),
					new(anchors[3].X + segmentRectHeight / 2, anchors[3].Y + gapSize + segmentRectHeight / 2),
					new(anchors[3].X + segmentRectHeight / 2, anchors[3].Y + gapSize + segmentRectHeight / 2 + rectSize),
					new(anchors[3].X, anchors[3].Y + gapSize + segmentRectHeight + rectSize),
					new(anchors[3].X - segmentRectHeight / 2, anchors[3].Y + gapSize + segmentRectHeight / 2 + rectSize),
					new(anchors[3].X - segmentRectHeight / 2, anchors[3].Y + gapSize + segmentRectHeight / 2)
				],
				[
					new(anchors[4].X + gapSize, anchors[4].Y),
					new(anchors[4].X + gapSize + segmentRectHeight / 2, anchors[4].Y - segmentRectHeight / 2),
					new(anchors[4].X + gapSize + segmentRectHeight / 2 + rectSize, anchors[4].Y - segmentRectHeight / 2),
					new(anchors[4].X + gapSize + segmentRectHeight + rectSize, anchors[4].Y),
					new(anchors[4].X + gapSize + segmentRectHeight / 2 + rectSize, anchors[4].Y + segmentRectHeight / 2),
					new(anchors[4].X + gapSize + segmentRectHeight / 2, anchors[4].Y + segmentRectHeight / 2)
				]
			];

			using var strokePaint = new SKPaint
			{
				Style = SKPaintStyle.Stroke,
				IsAntialias = true,
				Color = strokeColor,
				StrokeWidth = strokeWidthScale.Measure(cellSize)
			};
			using var phantomStrokePaint = new SKPaint
			{
				Style = SKPaintStyle.Stroke,
				IsAntialias = true,
				Color = strokeColor,
				StrokeWidth = phantomStrokeWidthScale.Measure(cellSize)
			};
			using var fillPaint = new SKPaint { Style = SKPaintStyle.Fill, IsAntialias = true, Color = fillColor };

			var segmentsLightup = LedStatesTable.Value[/*digit*/value][/*style*/0];
			for (var i = 0; i < segmentsLightup.Length; i++)
			{
				using var path = new SKPath();
				path.MoveTo(points[i][0]);
				for (var j = 1; j < 6; j++)
				{
					path.LineTo(points[i][j]);
				}
				path.Close();

				if (segmentsLightup[i])
				{
					@this.DrawPath(path, fillPaint);
					@this.DrawPath(path, strokePaint);
				}
				else if (showPhantomSegments)
				{
					@this.DrawPath(path, phantomStrokePaint);
				}
			}
		}
	}
}
