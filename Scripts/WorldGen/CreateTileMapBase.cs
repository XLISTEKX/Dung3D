using Godot;
using System;
using System.Collections;

public partial class CreateTileMapBase : TileMap
{

	public FastNoiseLite noise = new();
	[Export] public int creationSize;
	[Export] public float creationThreshold;
	[Export] public float creationRadius;
	
	
	[Export] TileMap tileMap;
	//Vector2I baseAtlas = new(24,12);	//Coordinates of a brown tile in the tileset

	public static Godot.Collections.Array<Vector2I> mapArray;
	Godot.Collections.Array<Vector2I> mapArrayForests;
	Godot.Collections.Array<Vector2I> mapArrayRocks;

	Godot.Collections.Array<Vector2I> mapArrayAddons;


	public override void _Ready()
	{
		RandomNumberGenerator seed = new();
		//seed.Randomize();
		ApplyNoiseSettings(seed.RandiRange(0, 2048));
		mapArray = MapArray.CreateMapArrayCircular(noise, creationSize, creationThreshold, creationRadius);

		noise.Frequency = 0.07f;

		//seed.Randomize();
		noise.Seed=seed.RandiRange(0, 2048);
		mapArrayRocks = MapArray.CreateArrayOnTop(mapArray, noise, creationThreshold+0.3f);

		//seed.Randomize();
		noise.Seed=seed.RandiRange(0, 2048);
		mapArrayForests = MapArray.CreateArrayOnTop(mapArray, noise, creationThreshold+0.3f);

		noise.Frequency = 0.45f;

		//seed.Randomize();
		noise.Seed=seed.RandiRange(0, 2048);
		mapArrayAddons = MapArray.CreateArrayOnTop(mapArray, noise, creationThreshold+0.62f);

		CreateBase();
	}

	private void ApplyNoiseSettings(int seed)
	{
		noise.Seed = seed;
		noise.NoiseType = FastNoiseLite.NoiseTypeEnum.Simplex;
		noise.FractalOctaves = 1;
		noise.FractalGain = 0;
		noise.Frequency = 0.12f;
	}

/*dumbdumbdumbdumbdumb, no need for setcell when setcellsterrainconnect exists
	private void CreateBase() 
	{
		foreach (Vector2I item in mapArray)
		{
			SetCell(0, new(item.X, item.Y), 3, baseAtlas, 0);
		}
	}
*/
	private void CreateBase() 
	{
		tileMap.SetCellsTerrainConnect(0, mapArray, 0, 0, true);
		tileMap.SetCellsTerrainConnect(1, mapArrayRocks, 1, 1, true);
		tileMap.SetCellsTerrainConnect(2, mapArrayForests, 1, 0, true);
		tileMap.SetCellsTerrainConnect(3, mapArrayAddons, 2, 0, true);
	}
}
