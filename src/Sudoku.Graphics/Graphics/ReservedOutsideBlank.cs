using System.Numerics;

namespace Sudoku.Graphics;

/// <summary>
/// Represents an encapsulated type that describes the number of cell units reserved to be empty
/// in drawing on the border of grid in a canvas.
/// </summary>
/// <param name="Up">
/// Indicates the number of cell units upside the canvas that will be reserved as blank in drawing.
/// </param>
/// <param name="Down">
/// Indicates the number of cell units downside the canvas that will be reserved as blank in drawing.
/// </param>
/// <param name="Left">
/// Indicates the number of cell units leftside the canvas that will be reserved as blank in drawing.
/// </param>
/// <param name="Right">
/// Indicates the number of cell units rightside the canvas that will be reserved as blank in drawing.
/// </param>
public readonly record struct ReservedOutsideBlank(int Up, int Down, int Left, int Right) :
	IEqualityOperators<ReservedOutsideBlank, ReservedOutsideBlank, bool>;
