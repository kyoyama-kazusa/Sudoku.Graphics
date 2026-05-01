namespace Sudoku.Graphics;

/// <summary>
/// Represents a type that stores basic information of a <see cref="Canvas"/> that can be used in serialization.
/// </summary>
public sealed class SerializableCanvasInfo
{
	/// <summary>
	/// Initializes a <see cref="SerializableCanvasInfo"/> instance.
	/// </summary>
	public SerializableCanvasInfo()
	{
	}

	/// <summary>
	/// Initializes a <see cref="SerializableCanvasInfo"/> instance via the specified template and items.
	/// </summary>
	/// <param name="template">The template.</param>
	/// <param name="items">The items.</param>
	public SerializableCanvasInfo(Template? template, ItemSet? items)
	{
		Template = template;
		Items = items;
	}


	/// <summary>
	/// Indicates templates.
	/// </summary>
	public Template? Template { get; set; }

	/// <summary>
	/// Indicates items.
	/// </summary>
	public ItemSet? Items { get; set; }


	/// <summary>
	/// Deconstructs the current instance into multiple values.
	/// </summary>
	/// <param name="template">The template.</param>
	/// <param name="items">The items.</param>
	public void Deconstruct(out Template? template, out ItemSet? items) => (template, items) = (Template, Items);
}
