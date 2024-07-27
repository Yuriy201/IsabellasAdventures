using UnityEngine;

[CreateAssetMenu(menuName = "Inside Components/new GlobalConfig")]
public class GameConfig : ScriptableObject
{
    [field: SerializeField] public PlatfotmType PlatfotmType { get; private set; }

    public bool IsMultiplayer = false;
}
