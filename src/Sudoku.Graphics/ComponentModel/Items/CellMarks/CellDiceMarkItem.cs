namespace Sudoku.ComponentModel.Items.CellMarks;

/// <summary>
/// Represents cell dice mark item.
/// </summary>
public sealed class CellDiceMarkItem : CellMarkItem
{
	/// <summary>
	/// Indicates dice value.
	/// </summary>
	public required int DiceValue { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellMark_Dice;

	/// <inheritdoc/>
	protected override Type EqualityContract => typeof(CellDiceMarkItem);


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var template = canvas.Templates[TemplateIndex];
		var mapper = template.Mapper;
		canvas.BackingCanvas.DrawTetromino(
			Cell,
			StrokeWidthScale,
			SizeScale,
			DiceTable.Values[DiceValue],
			StrokeColor,
			FillColor,
			.2M,
			1M,
			mapper
		);
	}
}
