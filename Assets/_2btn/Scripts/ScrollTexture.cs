using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTexture : MonoBehaviour {

    public float scrollSpeed = 10.0f;

    [SerializeField]
    private bool scrollWalls = false;
    [SerializeField]
    private bool rotateEnd = false;

    private float offset;
    private float tempSpeed = 0f;

    private GameManager _gameManager;
    private Renderer rend;

    void Start () {
        rend = GetComponent<Renderer> ();
        tempSpeed = scrollSpeed;
        scrollSpeed = 0.1f;
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

    public void TimerEnded()
    {
        scrollSpeed = tempSpeed;
    }
}