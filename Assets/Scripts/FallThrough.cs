using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlatformEffector2D))]
public class FallThrough : MonoBehaviour
{
    private PlatformEffector2D pe2d;
    [SerializeField]
    private DetectGround dg;
    private void Awake()
    {
        pe2d = GetComponent<PlatformEffector2D>();
    }

    public void OnDown(InputAction.CallbackContext ctx)
    {
        Vector2 input = ctx.ReadValue<Vector2>();
        if (input == Vector2.down && dg.IsOnGround)
        {
            dg.castFilter.useLayerMask = true;
            pe2d.rotationalOffset = 180f;
            StartCoroutine(FlipRotation());
        }
    }

    private IEnumerator FlipRotation()
    {
        yield return new WaitForSeconds(0.4f);
        pe2d.rotationalOffset = 0f;
    }
}
