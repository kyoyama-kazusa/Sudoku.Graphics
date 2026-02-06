namespace Sudoku.Concepts;

/// <summary>
/// Represents a cell coordinate. The cell will be designed inside a grid. If for a puzzle type that will display outside labels,
/// the value can be negative or greater than logical count of rows and columns.
/// </summary>
/// <param name="Row">Indicates the row index.</param>
/// <param name="Column">Indicates the column index.</param>
public readonly record struct Cell(int Row, int Column) : IEqualityOperators<Cell, Cell, bool>;
