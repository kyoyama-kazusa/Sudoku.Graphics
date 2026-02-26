namespace Sudoku.Graphics;

public partial class SKCanvasDrawingExtensions
{
	extension(SKCanvas @this)
	{
		/// <summary>
		/// Try to draw a tetris piece (or not, specified as a <see cref="bool"/> matrix) into the specified cell.
		/// </summary>
		/// <param name="cell">The cell.</param>
		/// <param name="strokeWidthScale">The stroke width scale.</param>
		/// <param name="pieceMinicellSizeScale">
		/// The scale that is a value between 0 and 1,
		/// representing the ratio between each piece inner cell size and the whole cell size of <paramref name="cell"/>.
		/// </param>
		/// <param name="shapeMatrix">
		/// The shape matrix. Specify a <see cref="bool"/> matrix indicating which coordinates are filled and which are not.
		/// For example, the following pattern is for a Z piece:
		/// <code><![CDATA[
		/// new bool[,]
		/// {
		///     { true, true, false },
		///     { false, true, true }
		/// }
		/// ]]></code>
		/// </param>
		/// <param name="strokeColor">The stroke color.</param>
		/// <param name="fillColor">The fill color.</param>
		/// <param name="blockPadding">The block padding.</param>
		/// <param name="cornerRadius">The corner radius.</param>
		/// <param name="mapper">The mapper.</param>
		public void DrawTetromino(
			Absolute cell,
			Scale strokeWidthScale,
			Scale pieceMinicellSizeScale,
			bool[,] shapeMatrix,
			SerializableColor strokeColor,
			SerializableColor fillColor,
			Scale blockPadding,
			Scale cornerRadius,
			PointMapper mapper
		)
		{
			if (pieceMinicellSizeScale.IsNegative)
			{
				return;
			}

			var (x, y) = mapper.GetPoint(cell, Alignment.TopLeft);
			var shapeRowsCount = shapeMatrix.GetLength(0);
			var shapeColumnsCount = shapeMatrix.GetLength(1);
			var cellSize = mapper.CellSize;
			var pieceEachBlockSize = pieceMinicellSizeScale.Measure(cellSize);
			var totalWidth = shapeColumnsCount * pieceEachBlockSize;
			var totalHeight = shapeRowsCount * pieceEachBlockSize;

			// Aligned as center for the whole piece.
			var startX = x + (cellSize - totalWidth) / 2;
			var startY = y + (cellSize - totalHeight) / 2;
			using var strokePaint = new SKPaint
			{
				Style = SKPaintStyle.Stroke,
				StrokeWidth = Math.Max(1F, strokeWidthScale.Measure(cellSize)),
				Color = strokeColor,
				IsAntialias = true
			};
			using var fillPaint = new SKPaint { Style = SKPaintStyle.Fill, Color = fillColor, IsAntialias = true };

			// Calculate for pixels for padding between two minicells in a piece.
			var blockPaddingFactValue = blockPadding.Measure(pieceEachBlockSize);
			var cellInner = Math.Max(pieceEachBlockSize - 2F * blockPaddingFactValue, 0);
			for (var r = 0; r < shapeRowsCount; r++)
			{
				for (var c = 0; c < shapeColumnsCount; c++)
				{
					if (!shapeMatrix[r, c])
					{
						continue;
					}

					var rx = startX + c * pieceEachBlockSize + blockPaddingFactValue;
					var ry = startY + r * pieceEachBlockSize + blockPaddingFactValue;
					var rect = new SKRect(rx, ry, rx + cellInner, ry + cellInner);
					var maxCornerRadius = cellInner / 2;
					var cornerRadiusValue = Math.Clamp(cornerRadius.Measure(maxCornerRadius), 0, maxCornerRadius);
					@this.DrawRoundRect(rect, cornerRadiusValue, cornerRadiusValue, fillPaint);
					@this.DrawRoundRect(rect, cornerRadiusValue, cornerRadiusValue, strokePaint);
				}
			}
		}
	}
}
