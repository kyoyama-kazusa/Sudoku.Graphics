namespace Sudoku.Graphics.UI.Services;

public interface IImageSaveService
{
	void Save(ISaveFileDialogService saveFileDialogService, WriteableBitmap? image);
}
