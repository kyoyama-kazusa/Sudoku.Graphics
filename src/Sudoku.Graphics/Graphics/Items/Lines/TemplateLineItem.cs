namespace Sudoku.Graphics.Items.Lines;

/// <summary>
/// Represents template line item.
/// </summary>
public sealed record TemplateLineItem : LineItem
{
	/// <summary>
	/// Indicates whether intersection cells should be filled with another color.
	/// By default it's <see langword="false"/>.
	/// </summary>
	public bool FillIntersectionCells { get; init; } = false;

	/// <summary>
	/// Indicates the color to be filled in intersection cells.
	/// This property must contain a valid color if <see cref="FillIntersectionCells"/> is <see langword="true"/>.
	/// </summary>
	/// <seealso cref="FillIntersectionCells"/>
	public SerializableColor TemplateIntersectionCellsColor { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.TemplateLine;


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var templates = canvas.Templates;

		// Fill intersection cells if worth.
		if (FillIntersectionCells)
		{
			fillIntersectionCells();
		}

		// Draw templates.
		foreach (var template in templates)
		{
			template.DrawLines(canvas.BackingCanvas);
		}


		void fillIntersectionCells()
		{
			using var fillPaint = new SKPaint { Style = SKPaintStyle.Fill, Color = TemplateIntersectionCellsColor };

			// Collect intersection cells, grouped by template specified by its index.
			var intersectionCellsDictionary = new Dictionary<int /*TemplateIndex*/, HashSet<Absolute>>();
			for (var i = 0; i < templates.Length - 1; i++)
			{
				if (templates[i] is not Template it)
				{
					// Non-individual templates are not supported to fill intersection cells.
					continue;
				}

				for (var j = i + 1; j < templates.Length; j++)
				{
					if (templates[j] is not Template jt)
					{
						// Non-individual templates are not supported to fill intersection cells.
						continue;
					}

					foreach (var cell in Template.GetIntersectionCellIndices(it, jt))
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
				var template = templates[templateIndex];
				foreach (var cell in cells.ToArray())
				{
					var topLeft = template.Mapper.GetPoint(cell, Alignment.TopLeft);
					var bottomRight = template.Mapper.GetPoint(cell, Alignment.BottomRight);
					var rect = SKRect.Create(topLeft, bottomRight);
					canvas.BackingCanvas.DrawRect(rect, fillPaint);
				}
			}
		}
	}
}
