using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveOffset : MonoBehaviour
{
    private Renderer meshRenderer;
    private Material currentMaterial;

    public float incremetoOffset;
    public float offsetSpeed;

    public string sortingLayer;
    public int orderInLayer;

    private float offset;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        meshRenderer.sortingLayerName = sortingLayer;
        meshRenderer.sortingOrder = orderInLayer;

        currentMaterial = meshRenderer.material;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        offset += incremetoOffset;

        currentMaterial.SetTextureOffset("_MainTex", new Vector2(offset * offsetSpeed, 0));
    }
}
