using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTexture : MonoBehaviour {

    // Scroll main texture based on time
    public float scrollSpeed = 0.5f;
    Renderer rend;

    public bool scrollWalls = false;
    public bool rotateEnd = false;
    void Start () {
        rend = GetComponent<Renderer> ();
    }
    float offset;
    void Update () {
        //Ground and ceiling
        if (!scrollWalls) {
            offset = Time.time * scrollSpeed;
            rend.material.SetTextureOffset ("_MainTex", new Vector2 (0.3f, -offset));
            rend.material.SetTextureOffset("_MKGlowTex", new Vector2(0.3f, -offset));
        }
        //Left and right wall
        else
        {
            offset = Time.time * scrollSpeed;
            rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0.3f));
            rend.material.SetTextureOffset("_MKGlowTex", new Vector2(offset, 0.3f));
        }
        if (rotateEnd)
        {
            offset = Time.time * scrollSpeed;
            rend.material.SetTextureOffset("_MainTex", new Vector2(offset, -offset));
            rend.material.SetTextureOffset("_MKGlowTex", new Vector2(offset, -offset));
        }

    }
}