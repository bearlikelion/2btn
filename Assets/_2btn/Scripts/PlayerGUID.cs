using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class PlayerGUID : MonoBehaviour {

	public static PlayerGUID instance;

    private string guid = "";

    public string Guid {
        get { return guid; }
    }

    void Awake () {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else if (instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

	void Start () {
        if (guid == "") {
            GenerateGUID();
        }
	}

    void GenerateGUID () {
        guid = System.Convert.ToBase64String(System.Guid.NewGuid().ToByteArray()); // Short GUID
        Regex rgx = new Regex("[^a-zA-Z0-9 -]");
        guid = rgx.Replace(guid, "");

        Debug.Log(guid);
    }
}
