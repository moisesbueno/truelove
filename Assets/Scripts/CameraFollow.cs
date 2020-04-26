using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
       public float maximoX;
    public float minimoX;
    public float MinimoY;
    public float MaximoY;

    public Transform player;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(Mathf.Clamp(player.position.x,minimoX,maximoX),Mathf.Clamp(player.position.y),MinimoY,MaximoY),transform.position.z));


        transform.position = new Vector3(Mathf.Clamp(player.position.x,minimoX,maximoX),Mathf.Clamp(player.position.y,MinimoY,MaximoY),transform.position.z);

    }
}
