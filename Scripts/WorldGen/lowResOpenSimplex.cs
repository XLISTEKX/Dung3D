using Godot;
using System;

public partial class lowResOpenSimplex : TileMap
{

	public FastNoiseLite noise = new();
	[Export] public int creationSize;
	[Export] public float creationThreshold;
	[Export] public float creationRadius;
	
	
	[Export] TileMap tileMap;
	Vector2I baseAtlas = new(24,12);	//Coordinates of a brown tile in the tileset

	Godot.Collections.Array<Vector2I> mapArray;

	public override void _Ready()
	{
		ApplyNoiseSettings();
		CreateBase();
		UpdateAutoTile();
	}

	private void ApplyNoiseSettings()
	{
		RandomNumberGenerator seed = new();
		seed.Randomize();
		noise.Seed = seed.RandiRange(0, 42170);
		noise.NoiseType = FastNoiseLite.NoiseTypeEnum.Simplex;
		noise.FractalOctaves = 1;
		noise.FractalGain = 0;
		noise.Frequency = 0.12f;
	}

	private void CreateBase() 
	{
		foreach (Vector2I item in MapArray.CreateMapArrayCircular(noise, creationSize, creationThreshold, creationRadius))
		{
			SetCell(0, new(item.X, item.Y), 3, baseAtlas, 0);
		}
	}

	private void UpdateAutoTile() 
	{
		tileMap.SetCellsTerrainConnect(0, MapArray.CreateMapArrayCircular(noise, creationSize, creationThreshold, creationRadius), 0, 0, true);
	}
}
