namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents a cell moon phase mark item.
/// </summary>
public sealed class CellMoonPhaseMarkItem : CellMarkItem
{
	/// <summary>
	/// Indicates the moon phase.
	/// </summary>
	public required MoonPhase Phase { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellMark_MoonPhase;

	/// <inheritdoc/>
	protected override Type EqualityContract => typeof(CellMoonPhaseMarkItem);


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var template = canvas.Templates[TemplateIndex];
		var mapper = template.Mapper;
		canvas.BackingCanvas.DrawMoonToCell(
			Cell,
			Phase,
			StrokeWidthScale,
			StrokeColor,
			FillColor,
			SizeScale,
			mapper
		);
	}
}
