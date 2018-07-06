using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    private static InputManager _instance;

    public static InputManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private float escapeValue = 1;

    void Start () {
		
	}
	
	void Update ()
    {
		
	}

    public bool EscapeHasBeenPressed()
    {
        Debug.Log(escapeValue == Input.GetAxis("Cancel"));
        return escapeValue == Input.GetAxis("Cancel");
    }
}
