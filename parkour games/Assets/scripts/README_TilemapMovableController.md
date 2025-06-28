# TilemapMovableController 脚本使用说明

## 功能描述
这个脚本让tilemap_movable对象在玩家**使用樱桃**后自动向左移动一段距离。玩家需要先收集樱桃，然后按指定按键使用樱桃的特殊能力来触发移动。

## 核心特性
- ✅ 樱桃收集和使用分离
- ✅ 按键触发移动（默认E键）
- ✅ 平滑的向左移动动画
- ✅ 一次性移动（移动后不再触发）
- ✅ 可配置的移动参数
- ✅ 调试可视化
- ✅ 事件系统支持
- ✅ 静态状态管理

## 使用方法

### 1. 添加脚本到tilemap_movable对象
1. 选择您的tilemap_movable GameObject
2. 将 `TilemapMovableController.cs` 脚本添加到该对象上

### 2. 配置脚本参数

#### 移动设置
- **Move Distance**: 向左移动的距离（默认3单位）
- **Move Speed**: 移动速度（默认2单位/秒）
- **Move Duration**: 移动持续时间（默认2秒）

#### 樱桃使用检测
- **Cherry Use Key**: 使用樱桃的按键（默认E键）
- **Require Cherry Collected**: 是否需要先收集樱桃才能使用（默认开启）

#### 组件引用
- **Rb**: 刚体组件（自动获取）
- **Movable Collider**: 碰撞器组件（自动获取）

#### 调试
- **Show Debug Gizmos**: 是否显示调试图形（默认开启）

### 3. 确保设置正确
1. tilemap_movable对象有Rigidbody2D组件
2. tilemap_movable对象有Collider2D组件
3. 樱桃对象有"cherry"标签
4. 樱桃对象有Collider2D组件

## 工作流程

### 1. 收集樱桃
- 玩家接触樱桃时，自动调用`MarkCherryCollected()`
- 樱桃跟随玩家移动
- 状态变为"已收集，未使用"

### 2. 使用樱桃
- 玩家按指定按键（默认E键）
- 检查是否已收集樱桃且未使用
- 如果条件满足，开始移动并标记樱桃已使用

### 3. 移动完成
- 对象向左移动指定距离
- 移动完成后停止
- 樱桃状态变为"已使用"

## 状态管理

### 樱桃状态
- **未收集**：红色圆圈
- **已收集，未使用**：黄色圆圈（可以按E键使用）
- **已使用**：绿色圆圈（已移动完成）

### 移动状态
- **isMoving**: 是否正在移动
- **hasMoved**: 是否已经移动过（防止重复触发）
- **cherryCollectedInScene**: 静态变量，跟踪场景中樱桃收集状态
- **cherryUsedInScene**: 静态变量，跟踪场景中樱桃使用状态

## 调试功能

### 可视化调试
当选中对象时，会显示以下调试图形：
- **红色线条**: 移动路径
- **绿色圆圈**: 目标位置
- **状态圆圈**: 
  - 红色：未收集樱桃
  - 黄色：已收集，可按键使用
  - 绿色：已使用，移动完成

### Console输出
- "TilemapMovable初始化 - 初始位置: X, 目标位置: Y"
- "樱桃在当前场景中被收集，可以按E键使用！"
- "TilemapMovable开始向左移动！"
- "TilemapMovable移动完成！"
- "樱桃在当前场景中被使用！"
- "无法移动：需要先收集樱桃或樱桃已被使用"
- "重置樱桃收集和使用状态"

## 静态方法

### MarkCherryCollected()
标记樱桃在当前场景中被收集，允许后续使用。

### MarkCherryUsed()
标记樱桃在当前场景中被使用。

### ResetCherryState()
重置樱桃收集和使用状态，允许重新收集和使用。

### IsCherryCollected()
获取当前场景中樱桃是否已被收集。

### IsCherryUsed()
获取当前场景中樱桃是否已被使用。

## 公共方法

### ResetMovement()
重置移动状态，允许重新触发移动。

### TriggerMovement()
手动触发移动，忽略樱桃状态。

### SetMovementParameters(float distance, float speed, float duration)
动态设置移动参数。

### IsMoving()
获取当前是否正在移动。

### HasMoved()
获取是否已经移动过。

## 事件系统

### OnStartMoving
当开始移动时触发的事件：
```csharp
TilemapMovableController controller = GetComponent<TilemapMovableController>();
controller.OnStartMoving += () => {
    Debug.Log("开始移动！");
};
```

### OnMoveComplete
当移动完成时触发的事件：
```csharp
controller.OnMoveComplete += () => {
    Debug.Log("移动完成！");
};
```

## 示例配置

### 标准配置
- Cherry Use Key: E
- Require Cherry Collected: ✅
- Move Distance: 3.0
- Move Speed: 2.0
- Move Duration: 2.0

### 快速移动配置
- Cherry Use Key: Space
- Move Distance: 5.0
- Move Speed: 4.0
- Move Duration: 1.0

### 无需收集配置
- Require Cherry Collected: ❌
- 可以直接按键使用，无需先收集樱桃

## 与其他脚本的集成

### 自动集成
脚本已自动集成到樱桃收集系统中：
- 当玩家收集樱桃时，自动调用`MarkCherryCollected()`
- 无需手动设置或配置

### 手动调用
如果需要手动触发，可以使用：
```csharp
// 标记樱桃收集
TilemapMovableController.MarkCherryCollected();

// 标记樱桃使用
TilemapMovableController.MarkCherryUsed();

// 重置状态
TilemapMovableController.ResetCherryState();
```

## 注意事项
1. 樱桃收集和使用是分离的两个状态
2. 移动是一次性的，如需重复移动请调用ResetMovement()
3. 调试图形只在Scene视图中显示
4. 移动完成后对象会停在目标位置
5. 可以通过事件系统添加音效、粒子效果等
6. 静态状态在场景切换时会重置
7. 可以自定义使用按键和是否要求先收集樱桃

## 扩展建议
- 添加使用樱桃的音效和视觉效果
- 添加移动音效和粒子效果
- 添加移动动画
- 实现更复杂的移动路径
- 添加多个樱桃的使用机制
- 添加场景切换时的状态保存
- 添加UI提示（如"按E键使用樱桃"） 