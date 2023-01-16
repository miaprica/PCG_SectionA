using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MaterialsBuilder
{
    private List<Material> materialsList = new List<Material>();

    public float R;
    public float G;
    public float B;

    public int RGB;

    public MaterialsBuilder()
    {
        //scene reference
        Scene currentScene = SceneManager.GetActiveScene();

        //get name
        string sceneName = currentScene.name;

        Material material1 = new Material(Shader.Find("Specular"));
        material1.color = new Color32(0, 166, 0, 255);

        Material material2 = new Material(Shader.Find("Specular"));
        material2.color = new Color32(0, 255, 246, 255);

        Material material3 = new Material(Shader.Find("Specular"));
        material3.color = new Color32(212, 22, 0, 255);

        Material material4 = new Material(Shader.Find("Specular"));
        material4.color = new Color32(255, 235, 4, 255);

        Material material5 = new Material(Shader.Find("Specular"));
        material5.color = new Color32(255, 0, 183, 255);

        Material material6 = new Material(Shader.Find("Specular"));
        material6.color = new Color32(0, 83, 255, 255);

        if (sceneName == "Level 1")
        {
            materialsList.Add(material1);
            materialsList.Add(material2);
            materialsList.Add(material3);
            materialsList.Add(material4);
            materialsList.Add(material5);
            materialsList.Add(material6);

        }

        else if (sceneName == "Level 2")
        {
            materialsList.Add(material6);
            materialsList.Add(material1);
            materialsList.Add(material5);
            materialsList.Add(material4);
            materialsList.Add(material3);
            materialsList.Add(material2);
        }
    }

    public List<Material> MaterialsList()
    {
        return materialsList;
    }

}
