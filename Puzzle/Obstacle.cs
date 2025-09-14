using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Obstacle : MonoBehaviour
{
    bool[] isObstalce = { false, false, false, false }; //Left, Right, Up, Down
    Rigidbody2D rigidbody2d;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public bool GetisObstacle(int num)
    {
        return isObstalce[num];
    }

    public void SetisObstacle(int num, bool flag)
    {
        isObstalce[num] = flag;
    }

    public void SetbodyType(int num)
    {
        switch (num)
        {
            case 0:
                if (isObstalce[1])
                {
                    rigidbody2d.bodyType = RigidbodyType2D.Static;
                }
                else
                {
                    rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
                }
                break;
            case 1:
                if (isObstalce[0])
                {
                    rigidbody2d.bodyType = RigidbodyType2D.Static;
                }
                else
                {
                    rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
                }
                break;
            case 2:
                if (isObstalce[3])
                {
                    rigidbody2d.bodyType = RigidbodyType2D.Static;
                }
                else
                {
                    rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
                }
                break;
            case 3:
                if (isObstalce[2])
                {
                    rigidbody2d.bodyType = RigidbodyType2D.Static;
                }
                else
                {
                    rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
                }
                break;
        }
    }
}