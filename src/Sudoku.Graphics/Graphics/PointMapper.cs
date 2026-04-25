namespace Sudoku.Graphics;

/// <summary>
/// Represents a point mapper instance.
/// </summary>
[JsonConverter(typeof(Converter))]
public sealed record PointMapper : IEqualityOperators<PointMapper, PointMapper, bool>
{
	/// <summary>
	/// Indicates cell width and height of pixels. By design, cell width is equal to cell height,
	/// so this property doesn't return an instance of either type <see cref="SKSize"/> or <see cref="SKSizeI"/>.
	/// </summary>
	/// <seealso cref="SKSize"/>
	/// <seealso cref="SKSizeI"/>
	public required float CellSize { get; init; }

	/// <summary>
	/// Indicates margin (pixel size of empty spaces between the fact sudoku grid and borders of the picture).
	/// </summary>
	public required float Margin { get; init; }

	/// <inheritdoc cref="GridTemplateSize.RowsCount"/>
	public Absolute RowsCount => TemplateSize.RowsCount;

	/// <inheritdoc cref="GridTemplateSize.ColumnsCount"/>
	public Absolute ColumnsCount => TemplateSize.ColumnsCount;

	/// <inheritdoc cref="GridTemplateSize.AbsoluteRowsCount"/>
	public Absolute AbsoluteRowsCount => TemplateSize.AbsoluteRowsCount;

	/// <inheritdoc cref="GridTemplateSize.AbsoluteColumnsCount"/>
	public Absolute AbsoluteColumnsCount => TemplateSize.AbsoluteColumnsCount;

	/// <inheritdoc cref="GridTemplateSize.Vector"/>
	public Thickness<Relative> Vector => TemplateSize.Vector;

	/// <summary>
	/// Indicates size information of the target grid to be drawn.
	/// </summary>
	public required GridTemplateSize TemplateSize { get; init; }


	/// <inheritdoc cref="Deconstruct(out Absolute, out Absolute, out Absolute, out Absolute, out GridTemplateSize)"/>
	public void Deconstruct(out Absolute rowsCount, out Absolute columnsCount)
		=> (rowsCount, columnsCount) = (RowsCount, ColumnsCount);

	/// <inheritdoc cref="Deconstruct(out Absolute, out Absolute, out Absolute, out Absolute, out GridTemplateSize)"/>
	public void Deconstruct(
		out Absolute rowsCount,
		out Absolute columnsCount,
		out Absolute absoluteRowsCount,
		out Absolute absoluteColumnsCount
	) => ((rowsCount, columnsCount), absoluteRowsCount, absoluteColumnsCount) = (this, AbsoluteRowsCount, AbsoluteColumnsCount);

	/// <summary>
	/// Deconstructs the current instance into multiple values.
	/// </summary>
	/// <param name="rowsCount">The number of rows.</param>
	/// <param name="columnsCount">The number of columns.</param>
	/// <param name="absoluteRowsCount">The number of rows in absolute grid.</param>
	/// <param name="absoluteColumnsCount">The number of columns in absolute grid.</param>
	/// <param name="templateSize">The template size.</param>
	public void Deconstruct(
		out Absolute rowsCount,
		out Absolute columnsCount,
		out Absolute absoluteRowsCount,
		out Absolute absoluteColumnsCount,
		out GridTemplateSize templateSize
	) => ((rowsCount, columnsCount, absoluteRowsCount, absoluteColumnsCount), templateSize) = (this, TemplateSize);

	/// <inheritdoc/>
	public bool Equals([NotNullWhen(true)] PointMapper? other)
		=> other is not null && CellSize == other.CellSize && Margin == other.Margin && TemplateSize == other.TemplateSize;

	/// <inheritdoc/>
	public override int GetHashCode() => HashCode.Combine(CellSize, Margin, TemplateSize);

	/// <inheritdoc/>
	public override string ToString()
	{
		// Format: "<cell-size>,<margin>,<rows-count>,<columns-count>,<vector-left>,<vector-top>,<vector-right>,<vector-bottom>"
		var (left, top, right, bottom) = Vector;
		var l = left == 0 ? string.Empty : left.ToString();
		var t = top == 0 ? string.Empty : top.ToString();
		var r = right == 0 ? string.Empty : right.ToString();
		var b = bottom == 0 ? string.Empty : bottom.ToString();
		return $"{CellSize},{Margin},{RowsCount},{ColumnsCount},{l},{t},{r},{b}";
	}

	/// <summary>
	/// Returns the position (point) of the specified alignment type of the specified cell.
	/// </summary>
	/// <param name="absoluteCellIndex">Absolute cell index.</param>
	/// <param name="alignment">The alignment.</param>
	/// <returns>The point instance that represents the target position.</returns>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Throws when <paramref name="alignment"/> is not defined or <see cref="Alignment.None"/>.
	/// </exception>
	/// <seealso cref="Alignment.None"/>
	public SKPoint GetPoint(Absolute absoluteCellIndex, Alignment alignment)
	{
		var columnsCount = AbsoluteColumnsCount;
		return GetPoint(absoluteCellIndex / columnsCount, absoluteCellIndex % columnsCount, alignment);
	}

	/// <summary>
	/// Returns the position (point) of the specified alignment type of the specified cell.
	/// </summary>
	/// <param name="absoluteRowIndex">Absolute row index.</param>
	/// <param name="absoluteColumnIndex">Absolute column index.</param>
	/// <param name="alignment">The alignment type.</param>
	/// <returns>The point instance that represents the target position.</returns>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Throws when <paramref name="alignment"/> is not defined or <see cref="Alignment.None"/>.
	/// </exception>
	/// <seealso cref="Alignment.None"/>
	public SKPoint GetPoint(Absolute absoluteRowIndex, Absolute absoluteColumnIndex, Alignment alignment)
	{
		var topLeft = new SKPoint(CellSize * absoluteColumnIndex + Margin, CellSize * absoluteRowIndex + Margin);
		return alignment switch
		{
			Alignment.Center => topLeft + (CellSize / 2, CellSize / 2),
			Alignment.TopLeft => topLeft,
			Alignment.TopRight => topLeft + (CellSize, 0),
			Alignment.BottomLeft => topLeft + (0, CellSize),
			Alignment.BottomRight => topLeft + (CellSize, CellSize),
			_ => throw new ArgumentOutOfRangeException(nameof(alignment))
		};
	}

	/// <summary>
	/// Returns the position (point) of the specified alignment type of the specified cell or candidate.
	/// </summary>
	/// <typeparam name="TLocator">The type of locator (cell or candidate).</typeparam>
	/// <param name="locator">The locator object (cell or candidate).</param>
	/// <param name="alignment">The alignment.</param>
	/// <returns>The point instance that represents the target position.</returns>
	/// <exception cref="NotSupportedException">
	/// Throws when type <typeparamref name="TLocator"/> is not <see cref="Absolute"/>,
	/// <see cref="Relative"/> or <see cref="CandidatePosition"/>.
	/// </exception>
	public SKPoint GetPoint<TLocator>(TLocator locator, Alignment alignment) where TLocator : unmanaged, ILocator<TLocator>
		=> locator switch
		{
			Absolute cell => GetPoint(cell, alignment),
			Relative cell => GetPoint(cell.ToAbsolute(this), alignment),
			CandidatePosition candidate => GetPoint(candidate, alignment),
			_ => throw new NotSupportedException($"The specified type '{typeof(TLocator).Name}' is not supported - it must be of type '{nameof(Absolute)}', '{nameof(Relative)}' or '{nameof(CandidatePosition)}'.")
		};

	/// <summary>
	/// Returns the position (point) of the specified alignment type of the specified candidate (absolute).
	/// </summary>
	/// <param name="candidatePosition">Absolute candidate position.</param>
	/// <param name="alignment">The alignment.</param>
	/// <returns>The point instance that represents the target position.</returns>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Throws when <paramref name="alignment"/> is not defined or <see cref="Alignment.None"/>.
	/// </exception>
	/// <seealso cref="Alignment.None"/>
	public SKPoint GetPoint(CandidatePosition candidatePosition, Alignment alignment)
	{
		var (cell, subgridSize, innerIndex) = candidatePosition;
		var cellTopLeft = GetPoint(cell, Alignment.TopLeft);
		var candidateSize = CellSize / subgridSize;
		var candidateRowIndex = innerIndex / subgridSize;
		var candidateColumnIndex = innerIndex % subgridSize;
		var topLeft = cellTopLeft + (candidateColumnIndex * candidateSize, candidateRowIndex * candidateSize);
		return alignment switch
		{
			Alignment.Center => topLeft + (candidateSize / 2, candidateSize / 2),
			Alignment.TopLeft => topLeft,
			Alignment.TopRight => topLeft + (candidateSize, 0),
			Alignment.BottomLeft => topLeft + (0, candidateSize),
			Alignment.BottomRight => topLeft + (candidateSize, candidateSize),
			_ => throw new ArgumentOutOfRangeException(nameof(alignment))
		};
	}

	/// <inheritdoc cref="GetPointBetween(Absolute, Absolute)"/>
	public SKPoint GetPointBetween(Relative cell1, Relative cell2) => GetPointBetween(cell1.ToAbsolute(this), cell2.ToAbsolute(this));

	/// <inheritdoc cref="GetPointBetweenWithAdjacentRelation(Absolute, Absolute, out Direction8)"/>
	public SKPoint GetPointBetween(Relative cell1, Relative cell2, out Direction8 adjacentRelation)
		=> GetPointBetweenWithAdjacentRelation(cell1.ToAbsolute(this), cell2.ToAbsolute(this), out adjacentRelation);

	/// <summary>
	/// Gets a point that is the center point of two cells; this method doesn't require two cells are adjacent with each other.
	/// </summary>
	/// <param name="cell1">The cell 1.</param>
	/// <param name="cell2">The cell 2.</param>
	/// <returns>The center point of two adjacent cells.</returns>
	public SKPoint GetPointBetween(Absolute cell1, Absolute cell2)
	{
		var p1 = GetPoint(cell1, Alignment.Center);
		var p2 = GetPoint(cell2, Alignment.Center);
		return p1 == p2 ? p1 : new((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
	}

	/// <summary>
	/// Gets a point that is the center point of two <b>adjacent</b> cells.
	/// </summary>
	/// <param name="cell1">The cell 1.</param>
	/// <param name="cell2">The cell 2.</param>
	/// <param name="adjacentRelation">The adjacent direction between two cells.</param>
	/// <returns>The center point of two adjacent cells.</returns>
	/// <exception cref="ArgumentException">Throws when the specified pair of cells are not adjacent with each other.</exception>
	public SKPoint GetPointBetweenWithAdjacentRelation(Absolute cell1, Absolute cell2, out Direction8 adjacentRelation)
	{
		if (Absolute.GetAdjacentRelation(cell1, cell2, this) is not (var direction and not Direction8.None))
		{
			const string errorInfo = $"The specified pair of cells '{nameof(cell1)}' and '{nameof(cell2)}' are not adjacent with each other.";
			throw new ArgumentException(errorInfo);
		}

#pragma warning disable IDE0055
		adjacentRelation = direction;
		return GetPoint(cell1, Alignment.Center) + adjacentRelation switch
		{
			Direction8.Up			=> (            0,  CellSize / 2),
			Direction8.Down			=> (            0, -CellSize / 2),
			Direction8.Left			=> ( CellSize / 2,             0),
			Direction8.Right		=> (-CellSize / 2,             0),
			Direction8.LeftUp		=> ( CellSize / 2,  CellSize / 2),
			Direction8.RightUp		=> (-CellSize / 2,  CellSize / 2),
			Direction8.LeftDown		=> ( CellSize / 2, -CellSize / 2),
			Direction8.RightDown	=> (-CellSize / 2, -CellSize / 2),
			_ => throw new UnreachableException()
		};
#pragma warning restore IDE0055
	}

	/// <summary>
	/// Creates a new <see cref="PointMapper"/> instance via the specified offset, replacing with new value.
	/// </summary>
	/// <param name="vector">The direction vector as offset.</param>
	/// <returns>The result <see cref="PointMapper"/> instance.</returns>
	public PointMapper WithOffset(Thickness<Relative> vector) => this with { TemplateSize = TemplateSize with { Vector = vector } };

	/// <summary>
	/// Creates a new <see cref="PointMapper"/> instance via the specified offset, adding to original template size direction vector.
	/// </summary>
	/// <param name="vector">The direction vector as offset.</param>
	/// <returns>The result <see cref="PointMapper"/> instance.</returns>
	public PointMapper AddOffset(Thickness<Relative> vector)
		=> this with { TemplateSize = TemplateSize with { Vector = TemplateSize.Vector + vector } };

	private bool PrintMembers(StringBuilder builder)
	{
		builder.Append($"{nameof(CellSize)} = {CellSize:0.0###}, ");
		builder.Append($"{nameof(Margin)} = {Margin:0.0###}, ");
		builder.Append($"{nameof(TemplateSize)} = {TemplateSize}");
		return true;
	}


	/// <inheritdoc cref="IParsable{TSelf}.TryParse(string?, IFormatProvider?, out TSelf)"/>
	public static bool TryParse([NotNullWhen(true)] string? s, [NotNullWhen(true)] out PointMapper? result)
	{
		try
		{
			if (s is null)
			{
				goto ReturnFalse;
			}

			result = Parse(s);
			return true;
		}
		catch (FormatException)
		{
		}

	ReturnFalse:
		result = default;
		return false;
	}

	/// <inheritdoc cref="IParsable{TSelf}.Parse(string, IFormatProvider?)"/>
	public static PointMapper Parse(string s)
	{
		// Format: "<cell-size>,<margin>,<rows-count>,<columns-count>,<vector-left>,<vector-top>,<vector-right>,<vector-bottom>"
		var split = s.Split(',', StringSplitOptions.TrimEntries);
		if (split is not [
			var cellSizeString,
			var marginString,
			var rowsCountString,
			var columnsCountString,
			var leftString,
			var topString,
			var rightString,
			var bottomString
		])
		{
			throw new FormatException();
		}

		if (!float.TryParse(cellSizeString, out var cellSize)
			|| !float.TryParse(marginString, out var margin)
			|| !int.TryParse(rowsCountString, out var rowsCount)
			|| !int.TryParse(columnsCountString, out var columnsCount)
			|| c(leftString) is not { } vectorLeft
			|| c(topString) is not { } vectorTop
			|| c(rightString) is not { } vectorRight
			|| c(bottomString) is not { } vectorBottom)
		{
			throw new FormatException();
		}

		var vector = new Thickness<Relative>(vectorLeft, vectorTop, vectorRight, vectorBottom);
		return new()
		{
			CellSize = cellSize,
			Margin = margin,
			TemplateSize = new() { RowsCount = rowsCount, ColumnsCount = columnsCount, Vector = vector }
		};


		static int? c(string vectorValue)
			=> vectorValue switch
			{
				"" => 0,
				_ when int.TryParse(vectorValue, out var value) => value,
				_ => null
			};
	}
}

/// <summary>
/// Represents a JSON converter of this type.
/// </summary>
file sealed class Converter : JsonConverter<PointMapper>
{
	/// <inheritdoc/>
	public override PointMapper? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var str = reader.GetString();
		return str is null ? null : PointMapper.TryParse(str, out var r) ? r : throw new JsonException();
	}

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, PointMapper value, JsonSerializerOptions options)
		=> writer.WriteStringValue(value.ToString());
}
