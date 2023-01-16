using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class Building : MonoBehaviour
{
    [SerializeField]
    private int buildingLengthSize = 5; //number of cubes that make up the length of the building (x)

    [SerializeField]
    private int buildingHeightSize = 10; //number of cubes that make up the height of the building (y)

    [SerializeField]
    private int buildingDepthSize = 5; //number of cubes that make up the depth of the building (z)

    [SerializeField]
    private int submeshCount = 6;

    //positions
    float x;
    float y;
    float z;

    Vector3 pos;


    void Start()
    {
        this.transform.position = new Vector3(0, 0, 0);
        CreateBuilding(name);
    }

    private void CreateBuilding(string name)
    {
        name = "Building";

        //Vector3 initialisePosition = new Vector3(0,0,-10f);
        Vector3 initialisePosition = Vector3.zero;

        //determining the range within the building will be placed
        y = 0; //y is 0 because we dont want the building to float
        x = Random.Range(0, 62);
        z = Random.Range(0, 75);
        //x = Random.Range(-50, 0);
        //z = Random.Range(-50, 30);
        pos = new Vector3(x, y, z);

            

        GameObject building = new GameObject();
        //building.name = "Building";
        building.name = name;
        building.transform.parent = this.transform;
        building.AddComponent<CubeRenderer>();
        building.GetComponent<CubeRenderer>().initialiseCube(buildingLengthSize, buildingHeightSize, buildingDepthSize, initialisePosition);

        //this.transform.rotation = Quaternion.Euler(0, 0, 0);
        this.transform.position = pos;

        ///

        //MeshFilter meshFilter = this.GetComponent<MeshFilter>();

        //MeshBuilder meshBuilder = new MeshBuilder(submeshCount);


        //meshFilter.mesh = meshBuilder.CreateMesh();

        //MeshRenderer meshRenderer = this.GetComponent<MeshRenderer>();

        ////MaterialsBuilder materialsBuilder = new MaterialsBuilder();

        ////
        //Material redMaterial = new Material(Shader.Find("Specular"));
        //redMaterial.color = Color.red;
        ////

        ////meshRenderer.materials = materialsBuilder.MaterialsList().ToArray();
        //meshRenderer.material = redMaterial;

    }

}