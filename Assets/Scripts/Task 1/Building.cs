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

    //positions
    float x;
    float y;
    float z;

    Vector3 pos;


    void Start()
    {
        this.transform.position = new Vector3(0, 0, 0);
        CreateBuilding(name);

        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.center = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        boxCollider.size = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
    }

    private void CreateBuilding(string name)
    {
        buildingHeightSize = Random.Range(8, 20);


        name = "Building";

        //Vector3 initialisePosition = new Vector3(0,0,-10f);
        Vector3 initialisePosition = Vector3.zero;

        //determining the range within the building will be placed
        y = 0; //y is 0 because we dont want the building to float
        x = Random.Range(0, 62);
        z = Random.Range(0, 75);
        pos = new Vector3(x, y, z);

            

        GameObject building = new GameObject();
        //building.name = "Building";
        building.name = name;
        building.transform.parent = this.transform;
        building.AddComponent<CubeRenderer>();
        building.GetComponent<CubeRenderer>().initialiseCube(buildingLengthSize, buildingHeightSize, buildingDepthSize, initialisePosition);

        //this.transform.rotation = Quaternion.Euler(0, 0, 0);
        this.transform.position = pos;

    }

}