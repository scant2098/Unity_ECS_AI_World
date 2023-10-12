using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class MapGenerator2D : MonoBehaviour
{
    [Header("地图设置")]
    [Tooltip("地图的宽度.")]
    public int mapWidth = 100; // 地图宽度

    [Header("地图的长度.")]
    public int mapHeight = 100; // 地图高度

    [Header("Tilemap")]
    public Tilemap tilemap; // Tilemap组件，用于存储地图数据

    [Header("Tile设置")]
    public TileBase landTile; // 陆地Tile
    public TileBase waterTile; // 海洋Tile

    [Header("地形生成设置")]
    [Tooltip("Perlin噪声的比例，控制地形的波动性.")]
    public float noiseScale = 1.0f; // 噪声比例，控制地形的波动性

    [Header("将地形分为陆地和海洋的阈值.")]
    [Range(0.0f, 1.0f)]
    public float threshold = 0.5f; // 阈值，控制大陆和海洋之间的分界

    private void Start()
    {
        GenerateRandomMap();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateRandomMap();
        }
    }

    private void GenerateRandomMap()
    {
        tilemap.ClearAllTiles();
        StartCoroutine(StartLoad());
    }

    private IEnumerator StartLoad()
    {
        float randomNoiseScale = Random.Range(10f, 15f); // 随机的噪声比例
        float randomThreshold = Random.Range(0.3f, 0.4f); // 随机的阈值

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                float xCoord = (float)x / mapWidth * randomNoiseScale;
                float yCoord = (float)y / mapHeight * randomNoiseScale;
                float height = Mathf.PerlinNoise(xCoord, yCoord);

                TileBase tile = height > randomThreshold ? landTile : waterTile;

                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                tilemap.SetTile(tilePosition, tile);
            }

            yield return null;
        }
    }
}