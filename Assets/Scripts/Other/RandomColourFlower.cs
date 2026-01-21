using UnityEngine;

public class RandomColourFlower : MonoBehaviour
{
    public MeshRenderer[] renderers;

    void Start()
    {
        Color newColor = Random.ColorHSV(0f, 1f, 0f, 1f, 0f, 1f);
        ApplyMaterial(newColor, 0);
    }

    void ApplyMaterial(Color color, int targetMaterialIndex)
    {
        Material generatedMaterial = new Material(renderers[0].materials[targetMaterialIndex]);
        generatedMaterial.color = color;

        foreach (var r in renderers)
        {
            Material[] mats = r.materials;
            mats[targetMaterialIndex] = generatedMaterial;
            r.materials = mats;
        }
    }
}