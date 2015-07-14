using UnityEngine;

public class MuteSound : MonoBehaviour
{
    private void OnClick()
    {
        var objectManager = ObjectManager.GetInstance();
        objectManager.gameState.isMuted = !objectManager.gameState.isMuted;
    }
}