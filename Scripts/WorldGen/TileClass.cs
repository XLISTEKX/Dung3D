using Godot;
using System;
using System.Collections;

public class TileClass
{
    int posX;
    int posZ;
    float vegetation;
    float temperature;
    int islandId;

    public TileClass(int posX, int posZ, float vegetation, float temperature)
    {
        this.posX = posX; this.posZ = posZ; this.vegetation = vegetation; this.temperature = temperature;
    }

    public int GetPosX()
    {
        return posX;
    }

    public int GetPosZ()
    {
        return posZ;
    }
}