namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents a cell battenburg (battenberg) mark item.
/// </summary>
public sealed record CellBattenburgMarkItem : CellMarkItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.Cell_Battenburg;

	/// <summary>
	/// Indicates the color 1.
	/// </summary>
	public required SerializableColor Color1 { get; init; }

	/// <summary>
	/// Indicates the color 2.
	/// </summary>
	public required SerializableColor Color2 { get; init; }

	/// <inheritdoc/>
	public override SerializableColor StrokeColor { get; init; }

	/// <inheritdoc/>
	public override Scale StrokeWidthScale { get; init; }

	/// <summary>
	/// Indicates uniform corner radius.
	/// </summary>
	public required Scale UniformCornerRadius { get; init; }


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var mapper = canvas.Templates[TemplateIndex].Mapper;
		canvas.BackingCanvas.DrawBattenburg(
			mapper.GetPoint(Cell, Alignment.Center),
			SizeScale,
			Color1,
			Color2,
			StrokeColor,
			StrokeWidthScale,
			[UniformCornerRadius, UniformCornerRadius, UniformCornerRadius, UniformCornerRadius],
			mapper
		);
	}
}
