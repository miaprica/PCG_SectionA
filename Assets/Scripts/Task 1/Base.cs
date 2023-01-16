using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField]
    private int baseLengthSize = 20; //number of cubes

    [SerializeField]
    private int baseDepthSize = 25; // number of cubes


    // Start is called before the first frame update
    void Start()
    {
        CreateBase();
        //AddGround();
    }


    private void CreateBase()
    {
        

        Vector3 nextPosition = Vector3.zero;

        float cubeDepth = 0;


        for (int j = 0; j < baseDepthSize; j++)
        {
            for (int i = 0; i < baseLengthSize; i++)
            {
                GameObject cube = new GameObject();
                //name = "Cube " + j + "-" + i;
                cube.name = "Cube " + j + "-" + i;
                cube.AddComponent<Cube>();
                cube.transform.position = nextPosition;
                cube.transform.parent = this.transform;

                nextPosition.x = cube.GetComponent<Cube>().CubeSize().x + nextPosition.x;
                cubeDepth = cube.GetComponent<Cube>().CubeSize().z;
            }

            nextPosition.z = cubeDepth + nextPosition.z;
            nextPosition.x = 0;
        }

        this.transform.position = new Vector3(0, 0, 0);
    }

}
