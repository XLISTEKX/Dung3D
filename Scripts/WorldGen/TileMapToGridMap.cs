using Godot;
using System;

public partial class TileMapToGridMap : GridMap
{
	[Export] TileMap tileMap;
	
	public override void _Ready()
	{
		int layersCount = tileMap.GetLayersCount();

		Godot.Collections.Array<Vector2I>[] perLayer = new Godot.Collections.Array<Vector2I>[layersCount];
		
		for(int i = 0; i < layersCount; i++)
		{
			perLayer[i] = tileMap.GetUsedCells(i);
		}

		TileMapTo3d(perLayer, layersCount);
	}

	private void TileMapTo3d(Godot.Collections.Array<Vector2I>[] arrayOfArrays, int layers)
	{
		for(int i = 0; i < layers; i++)
		{
			foreach(Vector2I item in arrayOfArrays[i])
			{
				SetCellItem(new Vector3I(item.X,0,item.Y), i);
			}
		}
		SetCellItem(new Vector3I(0,0,0), 0);
	}
}
