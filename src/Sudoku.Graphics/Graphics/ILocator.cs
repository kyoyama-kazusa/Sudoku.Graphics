namespace Sudoku.Graphics;

/// <summary>
/// Represents a locator object.
/// </summary>
/// <typeparam name="TSelf">The type implementing this interface.</typeparam>
public interface ILocator<TSelf> : IEquatable<TSelf> where TSelf : unmanaged, ILocator<TSelf>;
