# Unity方块脚本挂载指南

## 已创建的脚本

### 1. BlockController.cs - 高级方块控制器
**功能特性：**
- 可移动方块（来回移动）
- 碰撞时颜色闪烁效果
- 可调节移动速度、方向和距离
- 支持自定义方块颜色

**参数设置：**
- `isMovable`: 是否可移动
- `moveSpeed`: 移动速度
- `moveDirection`: 移动方向
- `moveDistance`: 移动距离
- `blockColor`: 方块颜色
- `changeColorOnHit`: 碰撞时是否改变颜色

### 2. SimpleBlock.cs - 简单方块
**功能特性：**
- 基础方块类型（普通、可破坏、弹跳）
- 可破坏方块系统
- 弹跳效果
- 碰撞闪烁效果

**参数设置：**
- `blockType`: 方块类型（普通方块、可破坏方块、弹跳方块）
- `isBreakable`: 是否可破坏
- `hitPoints`: 生命值
- `canBounce`: 是否有弹跳效果
- `bounceForce`: 弹跳力度

## 如何在Unity中挂载脚本

### 方法一：通过Inspector面板挂载

1. **选择方块对象**
   - 在Hierarchy面板中选择您要挂载脚本的方块GameObject

2. **添加脚本组件**
   - 在Inspector面板中点击"Add Component"按钮
   - 在搜索框中输入脚本名称（如"BlockController"或"SimpleBlock"）
   - 选择对应的脚本并点击添加

3. **配置参数**
   - 脚本添加后，在Inspector中会显示所有可配置的参数
   - 根据需要调整参数值

### 方法二：通过拖拽方式挂载

1. **准备脚本文件**
   - 确保脚本文件在Assets/scripts文件夹中
   - 脚本编译成功后会在Project面板中显示

2. **拖拽挂载**
   - 从Project面板中找到脚本文件
   - 将脚本文件直接拖拽到Hierarchy中的方块对象上
   - 或者拖拽到Inspector面板的Add Component区域

### 方法三：通过代码动态挂载

```csharp
// 在运行时动态添加脚本
GameObject block = GameObject.Find("方块名称");
if (block != null)
{
    block.AddComponent<BlockController>();
    // 或者
    block.AddComponent<SimpleBlock>();
}
```

## 方块设置建议

### 移动方块设置
1. 添加 `BlockController` 脚本
2. 勾选 `isMovable`
3. 设置 `moveSpeed`（建议2-5）
4. 设置 `moveDirection`（如Vector2.right表示向右移动）
5. 设置 `moveDistance`（移动距离）

### 弹跳方块设置
1. 添加 `SimpleBlock` 脚本
2. 设置 `blockType` 为"弹跳方块"
3. 勾选 `canBounce`
4. 调整 `bounceForce`（建议8-15）

### 可破坏方块设置
1. 添加 `SimpleBlock` 脚本
2. 设置 `blockType` 为"可破坏方块"
3. 勾选 `isBreakable`
4. 设置 `hitPoints`（生命值）

## 注意事项

1. **确保有碰撞器组件**
   - 方块必须有Collider2D组件才能检测碰撞
   - 建议使用BoxCollider2D

2. **标签设置**
   - 确保玩家对象有"Player"标签
   - 脚本中的碰撞检测依赖标签

3. **SpriteRenderer组件**
   - 脚本会自动处理SpriteRenderer组件
   - 如果没有会自动添加

4. **性能优化**
   - 移动方块建议使用Rigidbody2D
   - 静态方块可以设置为Static

## 常见问题解决

**Q: 脚本挂载后没有效果？**
A: 检查参数设置是否正确，确保方块有碰撞器组件

**Q: 移动方块不移动？**
A: 检查isMovable是否勾选，moveSpeed是否大于0

**Q: 碰撞没有反应？**
A: 检查玩家对象是否有"Player"标签，碰撞器设置是否正确

**Q: 颜色不改变？**
A: 确保方块有SpriteRenderer组件，检查blockColor设置 