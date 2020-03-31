using System.Collections.Generic;
using UnityEngine;

public class SetMaterialRenderQueue : MonoBehaviour
{
    public int RenderQueue = 2001;
    public List<Material> Materials = new List<Material>();

    void Start()
    {
        foreach (Material material in Materials)
        {
            material.renderQueue = RenderQueue;
        }
    }
}
