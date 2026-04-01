namespace Sudoku.Graphics;

public partial class SKCanvasDrawings
{
	extension(SKCanvas @this)
	{
		/// <summary>
		/// Draw arrow caps.
		/// </summary>
		/// <param name="penultimatePoint">The penuleimate point (i.e. <c>points[^2]</c>).</param>
		/// <param name="endPoint">The end point.</param>
		/// <param name="arrowCapLength">The arrow length.</param>
		/// <param name="arrowCapHalfAngleDegrees">The arrow half angle degrees.</param>
		/// <param name="paint">The stroke paint.</param>
		public void DrawArrowCaps(
			SKPoint penultimatePoint,
			SKPoint endPoint,
			float arrowCapLength,
			float arrowCapHalfAngleDegrees,
			SKPaint paint
		)
		{
			var dir = normalize(new(endPoint.X - penultimatePoint.X, endPoint.Y - penultimatePoint.Y));
			if (float.IsNaN(dir.X) || float.IsNaN(dir.Y))
			{
				return;
			}

			var angle = arrowCapHalfAngleDegrees * MathF.PI / 180;

			// In reversed direction, drawing arrow caps.
			var back = new SKPoint(-dir.X, -dir.Y);
			var leftDir = rotate(back, angle);
			var rightDir = rotate(back, -angle);
			var left = new SKPoint(endPoint.X + leftDir.X * arrowCapLength, endPoint.Y + leftDir.Y * arrowCapLength);
			var right = new SKPoint(endPoint.X + rightDir.X * arrowCapLength, endPoint.Y + rightDir.Y * arrowCapLength);
			using var arrowPath = new SKPath();
			arrowPath.MoveTo(endPoint);
			arrowPath.LineTo(left);
			arrowPath.MoveTo(endPoint);
			arrowPath.LineTo(right);
			@this.DrawPath(arrowPath, paint);


			static SKPoint normalize(SKPoint v)
			{
				var len = MathF.Sqrt(v.X * v.X + v.Y * v.Y);
				return len < 1E-6F ? new(float.NaN, float.NaN) : new(v.X / len, v.Y / len);
			}

			static SKPoint rotate(SKPoint v, float radians)
			{
				var cosine = MathF.Cos(radians);
				var sine = MathF.Sin(radians);
				return new(v.X * cosine - v.Y * sine, v.X * sine + v.Y * cosine);
			}
		}
	}
}
