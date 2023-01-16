using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class TerrainTextureData 
{
    public Texture2D terrainTexture;
    public Vector2 tileSize;
    public float minHeight;
    public float maxHeight;
}

[System.Serializable]
public class TreeData
{
    public GameObject treePrefab;
    public float minHeight;
    public float maxHeight;
}

public class GenerateRandomHeight : MonoBehaviour
{
    private Terrain terrain;
    private TerrainData terrainData;

    [SerializeField]
    [Range(0f, 1f)]
    private float minRandomHeightRange = 0f;

    [SerializeField]
    [Range(0f, 1f)]
    private float maxRandomHeightRange = 0.1f;

    [SerializeField]
    private bool flattenTerrain = true;

    [Header("Perlin Noise")]
    [SerializeField]
    private bool perlinNoise = true;

    [SerializeField]
    private float perlinNoiseWidthScale = 0.01f;

    [SerializeField]
    private float perlinNoiseHeightScale = 0.01f;

    [Header("TextureData")]
    [SerializeField]
    private List<TerrainTextureData> terrainTextureData;

    [SerializeField]
    private bool addTerrainTexture = true;

    [SerializeField]
    private float terrainTextureBlendOffset = 0.01f;

    [Header("TreeData")]
    [SerializeField]
    private List<TreeData> treeData;

    [SerializeField]
    private int maxTrees = 2000;

    [SerializeField]
    private int treeSpacing = 10;

    [SerializeField]
    private int terrainLayerIndex;

    [SerializeField]
    private bool addTrees = true;

    [Header("Water")]
    [SerializeField]
    private GameObject water;

    [SerializeField]
    private float waterHeight = 0.35f;

    [SerializeField]
    private bool addWater = true;

    [Header("Fog")]
    [SerializeField]
    private GameObject fog;

    [SerializeField]
    private float fogHeight = 100f;

    [SerializeField]
    private bool addFog = true;

    [Header("Rain")]
    [SerializeField]
    private GameObject rain;

    [SerializeField]
    private float rainHeight = 650f;

    [SerializeField]
    private bool addRain = true;

    Vector3 fogRandomPosition;
    Vector3 rainRandomPosition;

    // Start is called before the first frame update
    void Start()
    {
        if (terrain == null)
        {
            terrain = this.GetComponent<Terrain>();
        }

        if (terrainData == null)
        {
            terrainData = Terrain.activeTerrain.terrainData;
        }

        GenerateHeights();
        AddTerrainTextures();
        AddTrees();
        AddWater();
        AddFog();
        AddRain();
    }


    private void GenerateHeights()
    {
        float[,] heightMap = new float[terrainData.heightmapResolution, terrainData.heightmapResolution];

        for (int width = 0; width < terrainData.heightmapResolution; width++)
        {
            for (int height = 0; height < terrainData.heightmapResolution; height++)
            {
                if (perlinNoise)
                {
                    heightMap[width, height] = Mathf.PerlinNoise(width * perlinNoiseWidthScale, height * perlinNoiseHeightScale);
                }

                else
                {
                    heightMap[width, height] = Random.Range(minRandomHeightRange, maxRandomHeightRange);
                }
            }

            terrainData.SetHeights(0, 0, heightMap);

        }
    }

    private void FlattenTerrain()
    {
        float[,] heightMap = new float[terrainData.heightmapResolution, terrainData.heightmapResolution];

        for (int width = 0; width < terrainData.heightmapResolution; width++)
        {
            for (int height = 0; height < terrainData.heightmapResolution; height++)
            {
                heightMap[width, height] = 0;
            }

            terrainData.SetHeights(0, 0, heightMap);

        }
    }

    private void AddTerrainTextures()
    {
        TerrainLayer[] terrainLayers = new TerrainLayer[terrainTextureData.Count];

        for (int i = 0; i < terrainTextureData.Count; i++)
        {
            if (addTerrainTexture)
            {
                terrainLayers[i] = new TerrainLayer();
                terrainLayers[i].diffuseTexture = terrainTextureData[i].terrainTexture;
                terrainLayers[i].tileSize = terrainTextureData[i].tileSize;
            }
            else
            {
                terrainLayers[i] = new TerrainLayer();
                terrainLayers[i].diffuseTexture = null;
            }
        }

        terrainData.terrainLayers = terrainLayers;

        float[,] heightMap = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);
        float[, ,] alphamapList = new float[terrainData.alphamapWidth, terrainData.alphamapHeight, terrainData.alphamapLayers];

        for(int height = 0; height < terrainData.alphamapHeight; height++)
        {
            for (int width = 0; width < terrainData.alphamapWidth; width++)
            {
                float[] alphamap = new float[terrainData.alphamapLayers];

                for(int i = 0; i < terrainTextureData.Count; i++)
                {
                    float heightBegin = terrainTextureData[i].minHeight - terrainTextureBlendOffset;
                    float heightEnd = terrainTextureData[i].maxHeight + terrainTextureBlendOffset;

                    if(heightMap[width, height] >= heightBegin && heightMap[width, height] <= heightEnd)
                    {
                        alphamap[i] = 1;
                    }
                }

                Blend(alphamap);

                for(int j = 0; j < terrainTextureData.Count; j++)
                {
                    alphamapList[width, height, j] = alphamap[j];
                }
            }
        }

        terrainData.SetAlphamaps(0, 0, alphamapList);
    }

    private void Blend(float[] alphamap)
    {
        float total = 0;

        for(int i = 0; i < alphamap.Length; i++)
        {
            total += alphamap[i];
        }

        for(int i = 0; i < alphamap.Length; i++)
        {
            alphamap[i] = alphamap[i] / total;
        }
    }

    //method that will add tress on the terrain
    private void AddTrees()
    {
        TreePrototype[] trees = new TreePrototype[treeData.Count];
        
        for(int i = 0; i < treeData.Count; i++)
        {
            trees[i] = new TreePrototype();
            trees[i].prefab = treeData[i].treePrefab;
        }

        terrainData.treePrototypes = trees;

        List<TreeInstance> treeInstanceList = new List<TreeInstance>();

        if (addTrees)
        {
            for(int z = 0; z < terrainData.size.z; z+= treeSpacing)
            {
                for (int x = 0; x < terrainData.size.x; x += treeSpacing)
                {
                    for (int treeIndex = 0; treeIndex < trees.Length; treeIndex++)
                    {
                        if(treeInstanceList.Count < maxTrees)
                        {
                            //getting a height value between 0 and 1
                            float currentHeight = terrainData.GetHeight(x, z) / terrainData.size.y;
                        
                            if(currentHeight >= treeData[treeIndex].minHeight && currentHeight <= treeData[treeIndex].maxHeight)
                            {
                                float randomX = (x + Random.Range(-5.0f, 5.0f)) / terrainData.size.x;
                                float randomZ = (z + Random.Range(-5.0f, 5.0f)) / terrainData.size.z;

                                Vector3 treePosition = new Vector3(randomX * terrainData.size.x, currentHeight * terrainData.size.y, randomZ * terrainData.size.z) + this.transform.position;

                                RaycastHit raycastHit;

                                int layerMask = 1 << terrainLayerIndex;

                                //casting the ray so that the trees are positioned directly on the surface of the terrain
                                if (Physics.Raycast(treePosition, -Vector3.up, out raycastHit, 100, layerMask) || Physics.Raycast(treePosition, Vector3.up, out raycastHit, 100, layerMask))
                                {
                                    float treeDistance = (raycastHit.point.y - this.transform.position.y) / terrainData.size.y;

                                    TreeInstance treeInstance = new TreeInstance();

                                    treeInstance.position = new Vector3(randomX, treeDistance, randomZ); //treeDistance / currentHeight
                                    treeInstance.rotation = Random.Range(0, 360);
                                    treeInstance.prototypeIndex = treeIndex;
                                    treeInstance.color = Color.white;
                                    treeInstance.lightmapColor = Color.white;
                                    treeInstance.heightScale = 0.95f;
                                    treeInstance.widthScale = 0.95f;

                                    treeInstanceList.Add(treeInstance);
                                }
                            }
                        }
                    }

                    terrainData.treeInstances = treeInstanceList.ToArray();
                }
            }
        }
    }

    private void AddWater()
    {
        if (addWater)
        {
            GameObject waterGameObject = Instantiate(water, this.transform.position, this.transform.rotation);
            waterGameObject.name = "Water";
            waterGameObject.transform.position = this.transform.position + new Vector3(terrainData.size.x / 2, waterHeight * terrainData.size.y, terrainData.size.z / 2);
            waterGameObject.transform.localScale = new Vector3(terrainData.size.x / 100, 1, terrainData.size.z / 100);

        }
    }

    private void AddFog()
    {
        if (addFog)
        {
            //getting a random range of the fog height
            fogHeight = Random.Range(250, 400);

            //we want the fog to cover all the terrain, which is the reason only the height value is determined by a random range 
            fogRandomPosition = new Vector3(terrainData.size.x / 2, fogHeight, terrainData.size.z / 2);

            //if we wanted the fog to spawn anywhere on the terrain, on a completely random position, the following code would be:
            //fogRandomPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ);

            GameObject fogGameObject = Instantiate(fog, this.transform.position, this.transform.rotation);
            fogGameObject.name = "Fog";
            fogGameObject.transform.position = this.transform.position + fogRandomPosition; //setting the fog height to a random height on the terrain
            fogGameObject.transform.localScale = new Vector3(terrainData.size.x / 100, 1, terrainData.size.z / 100);

        }
    }

    private void AddRain()
    {
        if (addRain)
        {
            //getting a random range of the rain height
            rainHeight = Random.Range(terrainData.size.x, 1500);

            //we want the rain to cover all the terrain, which is the reason only the height value is determined by a random range 
            rainRandomPosition = new Vector3(terrainData.size.x / 2, rainHeight, terrainData.size.z / 2);

            //if we wanted the fog to spawn anywhere on the terrain, on a completely random position, the following code would be:
            //rainRandomPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ);

            GameObject rainGameObject = Instantiate(rain, this.transform.position, this.transform.rotation);
            rainGameObject.name = "Rain";
            //rainGameObject.transform.position = this.transform.position + new Vector3(terrainData.size.x / 2, rainHeight, terrainData.size.z / 2);
            rainGameObject.transform.position = this.transform.position + rainRandomPosition; //setting the rain height to a random height on the terrain
            rainGameObject.transform.localScale = new Vector3(terrainData.size.x / 16, terrainData.size.x / 30, terrainData.size.z / 25);

        }
    }

    private void OnDestroy()
    {
        if (flattenTerrain)
        {
            FlattenTerrain();
        }
    }
}
