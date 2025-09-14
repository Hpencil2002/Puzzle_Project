using UnityEngine;

public class WarpZone : MonoBehaviour
{
    [SerializeField] Transform TargetTransform = null;
    [SerializeField] Player player;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.SetCanWarp(true);
            player.SetTargetTransfrom(TargetTransform);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.SetCanWarp(false);
            player.SetTargetTransfrom(null);
        }
    }
}