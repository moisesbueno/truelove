using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    private Player player;
    // Start is called before the first frame update

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (!player.invunerable)
            {
                player.DamagePlayer();
            }
        }
    }
}
