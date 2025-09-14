using System.Security.Cryptography;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public int number;

    public void DisableGate()
    {
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        Color color = renderer.color;

        color.a = 0;
        renderer.color = color;

        SpriteRenderer[] childrenRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < childrenRenderers.Length; i++)
        {
            Color childColor = childrenRenderers[i].color;

            childColor.a = 0;
            childrenRenderers[i].color = childColor;
        }

        GetComponent<BoxCollider2D>().enabled = false;
    }
}
