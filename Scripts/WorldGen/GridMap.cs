using Godot;
using System;

public partial class GridMap : Godot.GridMap
{
	[Export] TileMap tileMap;

	Godot.Collections.Array<Vector2I> baseMapArray;
	Godot.Collections.Array<Vector2I> rocksMapArray;
	Godot.Collections.Array<Vector2I> forestsMapArray;
	Godot.Collections.Array<Vector2I> addonsMapArray;

	public override void _Ready()
	{
		initiateArrays();
		createGridMap();
	}

	private void initiateArrays()
	{
	baseMapArray = tileMap.GetUsedCells(0);
	rocksMapArray = tileMap.GetUsedCells(1);
	forestsMapArray = tileMap.GetUsedCells(2);
	addonsMapArray = tileMap.GetUsedCells(3);
	}

	private void createGridMap()
	{
		foreach(Vector2I item in baseMapArray)
		{
			this.SetCellItem(new Vector3I(item.X,0,item.Y), 0);
		}

		foreach(Vector2I item in rocksMapArray)
		{
			this.SetCellItem(new Vector3I(item.X,0,item.Y), 2);
		}

		foreach(Vector2I item in forestsMapArray)
		{
			this.SetCellItem(new Vector3I(item.X,0,item.Y), 1);
		}

		foreach(Vector2I item in addonsMapArray)
		{
			this.SetCellItem(new Vector3I(item.X,1,item.Y), 3);
		}
	}

}
