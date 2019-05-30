using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultsExplode : MonoBehaviour
{

    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f));
    }
}
