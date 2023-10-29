using Godot;
using System;

public partial class lowResOpenSimplex : TileMap
{

	public FastNoiseLite noise = new();
	[Export] public int width;
	[Export] public int height;
	[Export] public float terrainCap;
	
	[Export] TileMap tileMap;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		noise.NoiseType = FastNoiseLite.NoiseTypeEnum.Simplex;
		CreateBase();
	}

	public void GenerateWorld()
	{
		//RandomNumberGenerator seed = new();
		//seed.Randomize();
		//noise.Seed = seed.RandiRange(0, 42170);

		noise.FractalOctaves = 1;
		noise.FractalGain = 0;
		noise.
	}

	private void CreateBase() {
		float noiseVal;
		Godot.Collections.Array<Vector2I> vectorArr = new Godot.Collections.Array<Vector2I>();
		for(int i = 0; i < width; i++)
		{
			for(int j = 0; j < height; j++)
			{
				noiseVal = noise.GetNoise2D(i, j);
				if(noiseVal < terrainCap)
				{
					SetCell(0, new(i, j), 0, new(24,12));
					vectorArr.Add(new(i, j));
				}
			}
		}
		tileMap.SetCellsTerrainConnect(0, vectorArr, 0, 0, true);
	}
}
