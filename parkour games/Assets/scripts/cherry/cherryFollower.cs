using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryFollower : MonoBehaviour
{
    public Transform player;     // 玩家的Transform
    public Vector2 offset;       // 相对玩家的偏移（Inspector中设置）
    [HideInInspector]
    public bool facingRight;     // 玩家朝向（自动更新，无需手动设置）
    private Rigidbody2D playerRb; // 玩家的 Rigidbody2D 组件

    void Start()
    {
        // 获取玩家的 Rigidbody2D 组件
        playerRb = player.GetComponent<Rigidbody2D>();
        // 初始化朝向（根据玩家初始缩放判断）
        facingRight = playerRb.velocity.x > 0;
        UpdatePosition();
    }

    void Update()
    {
        // 根据玩家的水平速度判断朝向
        float horizontalVelocity = playerRb.velocity.x;

        // 实时检测玩家朝向变化
        if (Mathf.Abs(horizontalVelocity) > 0.1f)
        {
            bool newFacingRight = horizontalVelocity > 0;
            if (newFacingRight != facingRight)
            {
                facingRight = newFacingRight;
                UpdatePosition();
            }
        }
    }

    void UpdatePosition()
    {
        // 根据朝向计算方向：朝右时樱桃在玩家右侧（-1 → 实际是左侧，需理解“身后”逻辑）
        // 若想朝右时樱桃在右侧，改为 direction = facingRight ? 1f : -1f;
        float direction = facingRight ? -1f : 1f;
        transform.localPosition = new Vector2(offset.x * direction, offset.y);
    }
}
