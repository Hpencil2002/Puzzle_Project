using UnityEngine;

public class ObstacleChecker : MonoBehaviour
{
    public int num;

    Obstacle obstalce;

    void Start()
    {
        obstalce = GetComponentInParent<Obstacle>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            obstalce.SetisObstacle(num, true);
        }
        if (collision.CompareTag("Player"))
        {
            obstalce.SetbodyType(num);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            obstalce.SetisObstacle(num, false);
        }
    }
}
