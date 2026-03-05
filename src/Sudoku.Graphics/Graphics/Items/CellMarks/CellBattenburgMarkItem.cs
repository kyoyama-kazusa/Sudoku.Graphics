namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents a cell battenburg (battenberg) mark item.
/// </summary>
public sealed class CellBattenburgMarkItem : CellMarkItem
{
	/// <summary>
	/// Indicates the color 1.
	/// </summary>
	public required SerializableColor Color1 { get; init; }

	/// <summary>
	/// Indicates the color 2.
	/// </summary>
	public required SerializableColor Color2 { get; init; }

	/// <summary>
	/// Indicates uniform corner radius.
	/// </summary>
	public required Scale UniformCornerRadius { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellMark_Battenburg;

	/// <inheritdoc/>
	protected override Type EqualityContract => typeof(CellBattenburgMarkItem);


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var template = canvas.Templates[TemplateIndex];
		var mapper = template.Mapper;
		canvas.BackingCanvas.DrawBattenburgToCell(
			Cell,
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
