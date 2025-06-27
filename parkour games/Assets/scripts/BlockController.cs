using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    [Header("方块设置")]
    [SerializeField] private bool isMovable = false;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private Vector2 moveDirection = Vector2.right;
    [SerializeField] private float moveDistance = 5f;
    
    [Header("视觉效果")]
    [SerializeField] private Color blockColor = Color.white;
    [SerializeField] private bool changeColorOnHit = true;
    
    private Vector3 startPosition;
    private SpriteRenderer spriteRenderer;
    private bool movingForward = true;
    
    void Start()
    {
        // 保存初始位置
        startPosition = transform.position;
        
        // 获取SpriteRenderer组件
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = blockColor;
        }
        
        // 如果没有SpriteRenderer，尝试添加一个
        if (spriteRenderer == null)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            spriteRenderer.color = blockColor;
        }
    }
    
    void Update()
    {
        if (isMovable)
        {
            MoveBlock();
        }
    }
    
    void MoveBlock()
    {
        Vector3 targetPosition;
        
        if (movingForward)
        {
            targetPosition = startPosition + (Vector3)(moveDirection * moveDistance);
        }
        else
        {
            targetPosition = startPosition;
        }
        
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        
        // 检查是否到达目标位置
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            movingForward = !movingForward;
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (changeColorOnHit && collision.gameObject.CompareTag("Player"))
        {
            // 碰撞时改变颜色
            if (spriteRenderer != null)
            {
                StartCoroutine(FlashColor());
            }
        }
    }
    
    IEnumerator FlashColor()
    {
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = originalColor;
    }
    
    // 公共方法，供其他脚本调用
    public void SetMovable(bool movable)
    {
        isMovable = movable;
    }
    
    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }
    
    public void SetMoveDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
    }
} 