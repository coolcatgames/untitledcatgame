using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;
using Unity.AI.Navigation;

public class TerrainGenerator : MonoBehaviour
{
    public int height1 = 1;
    public float height2 = 0.1f;

    public int width = 128;
    public int length = 128;

    public float scale1 = 10f;
    public float scale2 = 50f;

    public NavMeshSurface ground;

    private void Start()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
        ground.BuildNavMesh();
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;

        terrainData.size = new Vector3(width, height1, length);
        terrainData.SetHeights(0,0,GenerateHeights());
        return terrainData;
    }

    float[, ] GenerateHeights ()
    {
        float xBias = Random.Range(-65536.0f, 65536.0f);
        float yBias = Random.Range(-65536.0f, 65536.0f);

        float[,] heights = new float[width, length];
        for(int x = 0; x<width; x++)
        {
            for(int y = 0; y<length; y++)
            {
                heights[x, y] = CalculateHeight(x,y,xBias,yBias);
            }
        }

        return heights;
    }

    float CalculateHeight(int x, int y, float xBias, float yBias)
    {
        float xCoord1 = (float)x / width * scale1 + xBias;
        float yCoord1 = (float)y / length * scale1 + yBias;

        float xCoord2 = (float)x / width * scale2 + yBias;
        float yCoord2 = (float)y / length * scale2 + xBias;

        return Mathf.PerlinNoise(xCoord1, yCoord1) + Mathf.PerlinNoise(xCoord2, yCoord2)*height2;

    }
}
