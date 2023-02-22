using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [Header("Player Tracking")]
    [SerializeField]
    private float smoothTimeX, smoothTimeY;
    [SerializeField]
    private Vector2 velocity;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Vector2 minPos, maxPos;
    [SerializeField]
    private bool bound;
    [Header("Look up/down")]
    [SerializeField]
    private float lookUpDistance;
    [SerializeField]
    private float smoothTimeLookup;
    [SerializeField]
    private Vector2 lookUpVelocity;

    // Update is called once per frame
    void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
        float posY;
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y + lookUpDistance, ref lookUpVelocity.y, smoothTimeLookup);
            transform.position = new Vector3(posX, posY, transform.position.z);
            return;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y - lookUpDistance, ref lookUpVelocity.y, smoothTimeLookup);
            transform.position = new Vector3(posX, posY, transform.position.z);
            return;
        }

        posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

        transform.position = new Vector3(posX, posY, transform.position.z);

        if (bound)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minPos.x, maxPos.x),
                Mathf.Clamp(transform.position.y, minPos.y, maxPos.y),
                Mathf.Clamp(transform.position.z, transform.position.z, transform.position.z));
        }
    }
}
