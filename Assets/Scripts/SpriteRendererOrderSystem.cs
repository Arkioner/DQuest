using UnityEngine;
using yaSingleton;

[CreateAssetMenu(fileName = "SpriteRendererOrderSystem", menuName = "Scripts/SpriteRendererOrderSystem")]
public class SpriteRendererOrderSystem : Singleton<SpriteRendererOrderSystem>
{
    public override void OnUpdate()
    {
        SpriteRenderer[] renderers = FindObjectsOfType<SpriteRenderer>();

        foreach (SpriteRenderer renderer in renderers)
        {
            renderer.sortingOrder = (int) (renderer.transform.position.y * -100);
        }
    }
}