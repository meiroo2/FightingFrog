using UnityEngine;

public class CameraController : MonoBehaviour
{
    public WaterCamera waterCamera;
    public Transform Player;
    public float[] BlockCamPos;
    public int GameLv = 1;

    private Vector3 camPos;


    private void Update()
    {
        waterCamera.effectActive = true;

        if (BlockCamPos.Length >= GameLv)
        {
            camPos.x = Player.position.x;
            camPos.y = Player.position.y;
            camPos.z = 0f;
            if (BlockCamPos[GameLv - 1] < camPos.y)
                camPos.y = BlockCamPos[GameLv - 1] - 0.1f;
        }

        transform.position = camPos;
    }

}