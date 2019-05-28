using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBlock : MonoBehaviour
{
    public GameObject playerPrefab;

    public void StartGame() {
        Instantiate(playerPrefab, transform.position, Quaternion.identity);
    }
}
