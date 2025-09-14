using UnityEngine;

public class GateManager : MonoBehaviour
{
    [SerializeField] Gate[] gates;

    public void CheckGate(int num)
    {
        foreach (Gate gate in gates) {
            if (gate.number == num)
            {
                gate.DisableGate();
                return;
            }
        }
    }
}
