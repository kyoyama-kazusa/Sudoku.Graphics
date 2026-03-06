namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Provides cell apex corner triangle mark item.
/// </summary>
public sealed record CellApexCornerTriangleMarkItem : CellMarkItem
{
	/// <summary>
	/// Indicates corner alignment. The value cannot be <see cref="Alignment.Center"/> due to design.
	/// </summary>
	/// <seealso cref="Alignment.Center"/>
	public required Alignment CornerAlignment { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellMark_ApexCornerTriangle;

	/// <summary>
	/// Indicates padding scale (distance to border of cells).
	/// </summary>
	public required Scale PaddingScale { get; init; }


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var template = canvas.Templates[TemplateIndex];
		var mapper = template.Mapper;
		canvas.BackingCanvas.DrawApexTriangleToCell(
			Cell,
			CornerAlignment,
			PaddingScale,
			SizeScale,
			StrokeColor,
			StrokeWidthScale,
			FillColor,
			mapper
		);
	}
}
