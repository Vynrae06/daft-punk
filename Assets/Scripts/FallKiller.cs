using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallKiller : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().KillPlayer();
        }
    }
}
