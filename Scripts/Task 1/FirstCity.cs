using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class FirstCity : MonoBehaviour
{
    [Header("Buildings")]
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

        //adding a box collider for the car controlled so that the car doesnt go into the buildings
        //leaving the y coordinate under the minimum building height so that the car doesnt spawn on top of the buildings
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.center = new Vector3(4, 4, 6);
        boxCollider.size = new Vector3(10, 10, 10);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SceneManager.LoadScene("Level 2");
        }
    }

    public void ChanageLevel()
    {
        SceneManager.LoadScene("Level 2");
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