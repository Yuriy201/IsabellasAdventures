using NeoxiderAudio;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public void ArrowShot()
    {
        AudioManager.PlaySound(ClipType.arrowShot);
    }

    public void Hit()
    {
        AudioManager.PlaySound(ClipType.playerHit);
    }

    public void WalkSteps()
    {
        AudioManager.PlaySound(ClipType.stepGrass);
    }
}
