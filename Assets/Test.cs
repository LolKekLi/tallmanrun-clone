using UnityEngine;

public class Test : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            PlayerMovementControl.Instance.MakeThePlayerTall(10);
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            PlayerMovementControl.Instance.MakeThePlayerTall(-10);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            PlayerMovementControl.Instance.BuffThePlayer(10);
        }
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            /////////sdsdsdadskjfbnasdkJB
            PlayerMovementControl.Instance.DeBuffThePlayer(10);
        }
    }
}
