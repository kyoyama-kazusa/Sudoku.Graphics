namespace Sudoku.Graphics;

public partial class SKCanvasDrawings
{
	extension(SKCanvas @this)
	{
		/// <summary>
		/// 在单元格 (x,y)（边长 cellSize）中心绘制一个 2x2 的 battenburg 图标。
		/// cornerRatios: 长度 4，顺序为 top-left, top-right, bottom-right, bottom-left（每个 0..1）。
		/// </summary>
		public void DrawBattenburgToCell(
			Absolute cell,
			Scale sizeScale,                // 0..1，相对于 cellSize
			SerializableColor colorA,                 // 左上 & 右下
			SerializableColor colorB,                 // 右上 & 左下
			SerializableColor lineColor,              // 十字线颜色
			Scale strokeWidthScale,                // 十字线粗细（像素）
			Scale[]? cornerRatios,      // 可为空（视为全 0），或长度 4（tl, tr, br, bl），每项 0..1
			PointMapper mapper
		)
		{
			cornerRatios ??= [0M, 0M, 0M, 0M];

			if (cornerRatios.Length != 4)
			{
				throw new ArgumentException("cornerRatios must be null or an array of length 4 (tl,tr,br,bl).");
			}

			var cellSize = mapper.CellSize;

			// 计算图标定位与大小
			var (x, y) = mapper.GetPoint(cell, Alignment.TopLeft);
			var iconSize = sizeScale.Measure(cellSize);
			var offset = (cellSize - iconSize) / 2;
			var iconLeft = x + offset;
			var iconTop = y + offset;
			var small = iconSize / 2; // 每个小格的边长

			// convert corner ratios (0..1) to pixel radii relative to 每个小格的边长
			// 用户描述 1 表示等于小格边长（我们按此映射）；Skia 会自动处理过大的半径。
			var cornerRadiiPx = (stackalloc float[4]);
			for (var i = 0; i < 4; i++)
			{
				cornerRadiiPx[i] = cornerRatios[i].Measure(small);
			}

			var strokeWidth = strokeWidthScale.Measure(cellSize);
			using var fillPaint = new SKPaint { Style = SKPaintStyle.Fill, IsAntialias = true };
			using var strokePaint = new SKPaint
			{
				Style = SKPaintStyle.Stroke,
				StrokeWidth = strokeWidth,
				IsAntialias = true,
				StrokeCap = SKStrokeCap.Butt,
				Color = lineColor
			};

			// 1) 绘制四个小格（按位置分别设置只有外侧角为圆角）
			// top-left (外侧角是 top-left)
			{
				var rect = new SKRect(iconLeft, iconTop, iconLeft + small, iconTop + small);
				var rr = new SKRoundRect();
				rr.SetRectRadii(rect, makeCornerRadii(cornerRadiiPx[0], 0f, 0f, 0f)); // tl
				fillPaint.Color = colorA;
				@this.DrawRoundRect(rr, fillPaint);
				@this.DrawRoundRect(rr, strokePaint);
			}

			// top-right (外侧角是 top-right)
			{
				var rect = new SKRect(iconLeft + small, iconTop, iconLeft + iconSize, iconTop + small);
				var rr = new SKRoundRect();
				rr.SetRectRadii(rect, makeCornerRadii(0f, cornerRadiiPx[1], 0f, 0f)); // tr
				fillPaint.Color = colorB;
				@this.DrawRoundRect(rr, fillPaint);
				@this.DrawRoundRect(rr, strokePaint);
			}

			// bottom-right (外侧角是 bottom-right)
			{
				var rect = new SKRect(iconLeft + small, iconTop + small, iconLeft + iconSize, iconTop + iconSize);
				var rr = new SKRoundRect();
				rr.SetRectRadii(rect, makeCornerRadii(0f, 0f, cornerRadiiPx[2], 0f)); // br
				fillPaint.Color = colorA;
				@this.DrawRoundRect(rr, fillPaint);
				@this.DrawRoundRect(rr, strokePaint);
			}

			// bottom-left (外侧角是 bottom-left)
			{
				var rect = new SKRect(iconLeft, iconTop + small, iconLeft + small, iconTop + iconSize);
				var rr = new SKRoundRect();
				rr.SetRectRadii(rect, makeCornerRadii(0f, 0f, 0f, cornerRadiiPx[3])); // bl
				fillPaint.Color = colorB;
				@this.DrawRoundRect(rr, fillPaint);
				@this.DrawRoundRect(rr, strokePaint);
			}

			// 2) 绘制十字格线（在填充上方）
			// 垂直线：x = iconLeft + small，y 从 iconTop 到 iconTop + iconSize
			// 水平线：y = iconTop + small，x 从 iconLeft 到 iconLeft + iconSize
			//var vx = iconLeft + small;
			//var hy = iconTop + small;
			//@this.DrawLine(vx, iconTop, vx, iconTop + iconSize, strokePaint);
			//@this.DrawLine(iconLeft, hy, iconLeft + iconSize, hy, strokePaint);


			static SKPoint[] makeCornerRadii(float tl, float tr, float br, float bl)
				=> [new(tl, tl), new(tr, tr), new(br, br), new(bl, bl)];
		}
	}
}
