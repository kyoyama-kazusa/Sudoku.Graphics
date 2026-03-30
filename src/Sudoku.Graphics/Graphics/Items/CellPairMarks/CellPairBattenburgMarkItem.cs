namespace Sudoku.Graphics.Items.CellPairMarks;

/// <summary>
/// Represents a cell pair battenburg mark item.
/// </summary>
public sealed record CellPairBattenburgMarkItem : CellPairMarkItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellPair_Battenburg;

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
	/// Indicates the scale of size of mark, related to cell size.
	/// </summary>
	public required Scale SizeScale { get; init; }

	/// <summary>
	/// Indicates uniform corner radius.
	/// </summary>
	public required Scale UniformCornerRadius { get; init; }


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
		=> canvas.BackingCanvas.DrawBattenburg(
			Cell1,
			Cell2,
			SizeScale,
			Color1,
			Color2,
			StrokeColor,
			StrokeWidthScale,
			[UniformCornerRadius, UniformCornerRadius, UniformCornerRadius, UniformCornerRadius],
			canvas.Templates[TemplateIndex].Mapper
		);
}
