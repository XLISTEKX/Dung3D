using Godot;
using System;

public partial class lowResOpenSimplex : TileMap
{

	public FastNoiseLite noise = new();
	[Export] public int size;
	[Export] public float creationThreshold;
	
	
	[Export] TileMap tileMap;
	Vector2I baseAtlas = new(24,12);

	public override void _Ready()
	{
		ApplyNoiseSettings();
		CreateBase();
	}

	private void ApplyNoiseSettings()
	{
		//RandomNumberGenerator seed = new();
		//seed.Randomize();
		//noise.Seed = seed.RandiRange(0, 42170);
		noise.NoiseType = FastNoiseLite.NoiseTypeEnum.Simplex;
		noise.FractalOctaves = 1;
		noise.FractalGain = 0;
		noise.Frequency = 0.12f;
	}

	private void CreateBase() {
		int i = 0;
		foreach (Vector2I item in MapArray.CreateMapArray(noise, size, creationThreshold))
		{
			SetCell(0, new(item.X, item.Y), 3, baseAtlas, 0);
		}
	}
}
