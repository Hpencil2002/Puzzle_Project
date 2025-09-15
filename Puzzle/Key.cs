using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] GateManager gateManager;

    public int number;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (gateManager != null)
            {
                SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
                Color color = renderer.color;

                color.a = 0;
                renderer.color = color;

                GetComponent<BoxCollider2D>().enabled = false;

                gateManager.CheckGate(number);
            }
        }
        else
        {
            Debug.Log("Who are you?");
        }
    }
}
