using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTexture : MonoBehaviour {

    // Scroll main texture based on time
    public float scrollSpeed = 1f;
    Renderer rend;

    [SerializeField]
    private bool scrollWalls = false;
    [SerializeField]
    private bool rotateEnd = false;

    private float offset;

    void Start () {
        rend = GetComponent<Renderer> ();
    }
    void Update () {

        offset += Time.deltaTime * scrollSpeed;

        //Ground and ceiling
        if (!scrollWalls) {

            rend.material.SetTextureOffset ("_MainTex", new Vector2 (0.3f, -offset));
            rend.material.SetTextureOffset("_MKGlowTex", new Vector2(0.3f, -offset));
        }
        //Left and right wall
        else
        {
            rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0.3f));
            rend.material.SetTextureOffset("_MKGlowTex", new Vector2(offset, 0.3f));
        }
        if (rotateEnd)
        {
            rend.material.SetTextureOffset("_MainTex", new Vector2(offset, -offset));
            rend.material.SetTextureOffset("_MKGlowTex", new Vector2(offset, -offset));
        }

    }
}