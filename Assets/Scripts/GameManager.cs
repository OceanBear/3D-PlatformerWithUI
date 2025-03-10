using UnityEditor;
using UnityEngine;

public class GameManager : SingletonMonoBehavior<GameManager>
{
    protected override void Awake()
    {
        base.Awake();
        // Set the target frame rate to 60 frames per second
        Application.targetFrameRate = 60;
    }
}
