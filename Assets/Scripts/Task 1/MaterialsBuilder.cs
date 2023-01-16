using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialsBuilder
{
    private List<Material> materialsList = new List<Material>();

    public MaterialsBuilder()
    {

        Material redMaterial = new Material(Shader.Find("Specular"));
        redMaterial.color = new Color32(0, 166, 0, 255);

        Material blueMaterial = new Material(Shader.Find("Specular"));
        blueMaterial.color = new Color32(0, 20, 212, 255);

        Material greenMaterial = new Material(Shader.Find("Specular"));
        greenMaterial.color = new Color32(212, 166, 0, 255);

        Material yellowMaterial = new Material(Shader.Find("Specular"));
        yellowMaterial.color = Color.yellow;

        Material magentaMaterial = new Material(Shader.Find("Specular"));
        magentaMaterial.color = Color.magenta;

        Material cyanMaterial = new Material(Shader.Find("Specular"));
        cyanMaterial.color = Color.cyan;

        materialsList.Add(redMaterial);
        materialsList.Add(blueMaterial);
        materialsList.Add(greenMaterial);
        materialsList.Add(yellowMaterial);
        materialsList.Add(magentaMaterial);
        materialsList.Add(cyanMaterial);
    }

    public List<Material> MaterialsList()
    {
        return materialsList;
    }

}
