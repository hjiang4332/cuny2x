using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterAdjustableCamera : MonoBehaviour
{
    private Transform player1, player2;
    private float minSizeY = 4f;

    void Start()
    {
        player1 = GameObject.Find("Player1").GetComponent<Transform>();
        player2 = GameObject.Find("Player2").GetComponent<Transform>();
    }

    void SetCameraPos()
    {
        Vector3 midpoint = (player1.position + player2.position) / 2f;
        GetComponent<Camera>().transform.position = new Vector3(midpoint.x, midpoint.y, GetComponent<Camera>().transform.position.z);
    }

    void SetCameraSize()
    {
        float minSizeX = minSizeY * Screen.width / Screen.height;
        float width = Mathf.Abs(player1.position.x - player2.position.x) / 2f;
        float height = Mathf.Abs(player1.position.y - player2.position.y) / 1.5f;
        
        float camSizeX = Mathf.Max(width, minSizeX)*2f;
        GetComponent<Camera>().orthographicSize = Mathf.Max(height, camSizeX * Screen.height / Screen.width, minSizeY);
    }
    void Update()
    {
        SetCameraPos();
        SetCameraSize();
    }
}
