using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTexture : MonoBehaviour {

    // Scroll main texture based on time
    public float scrollSpeed = 0.5f;
    Renderer rend;

    public bool scrollWalls = false;
    void Start () {
        rend = GetComponent<Renderer> ();
    }

    void Update () {
        //Ground and ceiling
        if (!scrollWalls) {
            float offset = Time.time * scrollSpeed;
            rend.material.SetTextureOffset ("_MainTex", new Vector2 (0, -offset));
        }
        //Left and right wall
        else {
            float offset = Time.time * scrollSpeed;
            rend.material.SetTextureOffset ("_MainTex", new Vector2 (offset, 0));
        }
    }
}