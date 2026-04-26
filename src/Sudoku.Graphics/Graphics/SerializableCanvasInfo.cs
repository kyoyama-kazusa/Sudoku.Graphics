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
	/// Initializes a <see cref="SerializableCanvasInfo"/> instance via the specified templates and items.
	/// </summary>
	/// <param name="templates">The templates.</param>
	/// <param name="items">The items.</param>
	public SerializableCanvasInfo(Template[]? templates, ItemSet? items)
	{
		Templates = templates;
		Items = items;
	}


	/// <summary>
	/// Indicates templates.
	/// </summary>
	public Template[]? Templates { get; set; }

	/// <summary>
	/// Indicates items.
	/// </summary>
	public ItemSet? Items { get; set; }


	/// <summary>
	/// Deconstructs the current instance into multiple values.
	/// </summary>
	/// <param name="templates">The templates.</param>
	/// <param name="items">The items.</param>
	public void Deconstruct(out Template[]? templates, out ItemSet? items) => (templates, items) = (Templates, Items);
}
