using UnityEngine;

public class AutoColliderSize : MonoBehaviour
{
    [Header("碰撞箱设置")]
    [SerializeField] private bool autoSize = true;
    [SerializeField] private Vector2 sizeMultiplier = Vector2.one;
    [SerializeField] private Vector2 offset = Vector2.zero;
    [SerializeField] private bool showGizmos = true;
    
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        
        if (autoSize)
        {
            AdjustColliderSize();
        }
    }
    
    void AdjustColliderSize()
    {
        if (spriteRenderer != null && boxCollider != null)
        {
            // 获取Sprite的边界大小
            Vector2 spriteSize = spriteRenderer.sprite.bounds.size;
            
            // 应用缩放和偏移
            Vector2 finalSize = new Vector2(
                spriteSize.x * sizeMultiplier.x,
                spriteSize.y * sizeMultiplier.y
            );
            
            // 设置碰撞箱大小
            boxCollider.size = finalSize;
            
            // 设置偏移
            boxCollider.offset = offset;
            
            Debug.Log($"已调整 {gameObject.name} 的碰撞箱大小: {finalSize}");
        }
        else
        {
            Debug.LogWarning($"无法调整 {gameObject.name} 的碰撞箱大小: 缺少SpriteRenderer或BoxCollider2D组件");
        }
    }
    
    // 公共方法，供其他脚本调用
    public void SetSizeMultiplier(Vector2 multiplier)
    {
        sizeMultiplier = multiplier;
        AdjustColliderSize();
    }
    
    public void SetOffset(Vector2 newOffset)
    {
        offset = newOffset;
        AdjustColliderSize();
    }
    
    public void SetAutoSize(bool auto)
    {
        autoSize = auto;
        if (auto)
        {
            AdjustColliderSize();
        }
    }
    
    // 在编辑器中调用
    [ContextMenu("调整碰撞箱大小")]
    void AdjustSizeInEditor()
    {
        AdjustColliderSize();
    }
    
    // 在Scene视图中显示碰撞箱
    void OnDrawGizmos()
    {
        if (!showGizmos) return;
        
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider != null)
        {
            Gizmos.color = Color.red;
            Vector3 center = transform.position + (Vector3)boxCollider.offset;
            Vector3 size = boxCollider.size;
            Gizmos.DrawWireCube(center, size);
        }
    }
    
    // 获取当前碰撞箱信息
    public string GetColliderInfo()
    {
        if (boxCollider != null)
        {
            return $"大小: {boxCollider.size}, 偏移: {boxCollider.offset}";
        }
        return "无碰撞箱";
    }
} 