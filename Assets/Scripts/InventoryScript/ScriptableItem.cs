using UnityEngine;

[CreateAssetMenu(menuName = "GameObject/Item")]
public class ScriptableItem : ScriptableObject
{
    public string Name;
    public Sprite Image;
    public int MaxStack = 1;
}
