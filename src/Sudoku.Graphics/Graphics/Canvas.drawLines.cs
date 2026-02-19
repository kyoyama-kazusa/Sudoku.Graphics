namespace Sudoku.Graphics;

public partial class Canvas
{
	/// <inheritdoc/>
	public partial void DrawLines(bool fillIntersection)
	{
		// Fill intersection cells if worth.
		if (fillIntersection)
		{
			fillIntersectionCells();
		}

		// Draw templates.
		foreach (var template in Templates)
		{
			template.DrawLines(BackingCanvas, Options);
		}


		void fillIntersectionCells()
		{
			using var fillPaint = new SKPaint { Style = SKPaintStyle.Fill, Color = Options.TemplateIntersectionColor.Resolve(Options) };

			// Collect intersection cells, grouped by template specified by its index.
			var intersectionCellsDictionary = new Dictionary<int /*TemplateIndex*/, HashSet<Absolute>>();
			for (var i = 0; i < Templates.Length - 1; i++)
			{
				if (Templates[i] is not IndividualGridTemplate it)
				{
					// Non-individual templates are not supported to fill intersection cells.
					continue;
				}

				for (var j = i + 1; j < Templates.Length; j++)
				{
					if (Templates[j] is not IndividualGridTemplate jt)
					{
						// Non-individual templates are not supported to fill intersection cells.
						continue;
					}

					foreach (var cell in IndividualGridTemplate.GetIntersectionCellIndices(it, jt))
					{
						if (!intersectionCellsDictionary.TryAdd(i, [cell]))
						{
							intersectionCellsDictionary[i].Add(cell);
						}
					}
				}
			}

			// Iterate on each template, to draw cells.
			foreach (var (templateIndex, cells) in intersectionCellsDictionary)
			{
				var template = Templates[templateIndex];
				foreach (var cell in cells.ToArray())
				{
					var topLeft = template.Mapper.GetPoint(cell, CellAlignment.TopLeft);
					var bottomRight = template.Mapper.GetPoint(cell, CellAlignment.BottomRight);
					var rect = SKRect.Create(topLeft, bottomRight);
					BackingCanvas.DrawRect(rect, fillPaint);
				}
			}
		}
	}
}
