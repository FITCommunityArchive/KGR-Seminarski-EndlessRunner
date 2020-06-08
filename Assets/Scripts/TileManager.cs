using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TileManager : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private GameObject[] tilePrefabs;
#pragma warning restore 0649

    private Transform playerTransform;
    private float spawnZ = 0.0f;
    private float spawnX = 0.0f;
    private float tileLenght = 87.6f;
    private float tileXPosition = 25.25f;
    private int amnTilesOnScreen = 2;
    private int lastPrefabIndex = 0;
    private float safeZone = 95.0f;
    private List<GameObject> activeTiles;
    private void Start()
    {
        activeTiles = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        for(int i=0;i<amnTilesOnScreen;i++)
                SpawnTile();
    }

    void Update()
    {
        if (playerTransform.position.z - safeZone> spawnZ - amnTilesOnScreen * tileLenght)
        {
            SpawnTile();
            DeleteTile();
        }
    }
    private void SpawnTile(int prefabIndex = -1)
    {
        GameObject go;
        go = Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = new Vector3(spawnX,0,spawnZ);
        spawnZ += tileLenght;
        spawnX += tileXPosition;
        activeTiles.Add(go);
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
    private int RandomPrefabIndex()
    {
        if (tilePrefabs.Length <= 1)
            return 0;

        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0,tilePrefabs.Length);
        }
        lastPrefabIndex = randomIndex;
        return randomIndex;
    }
}
