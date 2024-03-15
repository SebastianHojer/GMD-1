using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");

        if (player == null)
        {
            Debug.LogWarning("GameObject with tag Player not found.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = player.transform.position;
        var cameraTransform = transform;
        var position = cameraTransform.position;
        position = new Vector3(playerPosition.x, position.y, position.z);
        cameraTransform.position = position;
    }
}
