namespace Sudoku.ComponentModel.Items;

/// <summary>
/// Represents template line stroke item.
/// </summary>
public sealed class TemplateLineStrokeItem : Item, IItem_ColorProperty
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
	public override ItemType Type => ItemType.TemplateLineStroke;

	/// <inheritdoc/>
	protected override Type EqualityContract => typeof(TemplateLineStrokeItem);

	/// <inheritdoc/>
	SerializableColor IItem_ColorProperty.Color
	{
		get => TemplateIntersectionCellsColor;

		init => TemplateIntersectionCellsColor = value;
	}


	/// <inheritdoc/>
	public override bool Equals([NotNullWhen(true)] Item? other)
		=> other is TemplateLineStrokeItem comparer
		&& FillIntersectionCells == comparer.FillIntersectionCells
		&& TemplateIntersectionCellsColor == comparer.TemplateIntersectionCellsColor;

	/// <inheritdoc/>
	public override int GetHashCode() => HashCode.Combine(EqualityContract, FillIntersectionCells, TemplateIntersectionCellsColor);

	/// <inheritdoc/>
	protected override void PrintMembers(StringBuilder builder)
	{
		builder.Append($"{nameof(FillIntersectionCells)} = {FillIntersectionCells}, ");
		builder.Append($"{nameof(TemplateIntersectionCellsColor)} = {TemplateIntersectionCellsColor}");
	}

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
				if (templates[i] is not GridTemplate it)
				{
					// Non-individual templates are not supported to fill intersection cells.
					continue;
				}

				for (var j = i + 1; j < templates.Length; j++)
				{
					if (templates[j] is not GridTemplate jt)
					{
						// Non-individual templates are not supported to fill intersection cells.
						continue;
					}

					foreach (var cell in GridTemplate.GetIntersectionCellIndices(it, jt))
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
