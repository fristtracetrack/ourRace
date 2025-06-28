# EndTheScene 脚本使用说明

## 功能描述
这个脚本用于检测旗子是否处于finish状态（播放finish动画），当玩家接触旗子时自动切换到下一个场景。

## 核心特性 ⚡
- ✅ 检测旗子finish动画播放状态
- ✅ 玩家接触触发场景切换
- ✅ 防止重复触发
- ✅ 快速响应（0.2秒默认延迟）
- ✅ 立即切换选项
- ✅ 详细的调试信息

## 使用方法

### 1. 添加脚本到旗子对象
1. 选择您的旗子GameObject
2. 将 `EndTheScene.cs` 脚本添加到该对象上

### 2. 配置脚本参数
在Inspector面板中配置以下参数：

#### 组件引用
- **Flag Animator**: 旗子的Animator组件（如果不设置，会自动从当前对象获取）
- **Flag Collider**: 旗子的碰撞器组件（如果不设置，会自动从当前对象获取）

#### 动画设置
- **Finish Animation Name**: finish动画的名称（默认为"finshi"）

#### 场景设置
- **Next Scene Index**: 下一个场景的索引
  - 设置为具体数字（如1、2、3等）会切换到指定场景
  - 设置为-1会自动切换到下一个场景（当前场景索引+1）
- **Scene Transition Delay**: 场景切换延迟时间（0-3秒，默认0.2秒）
- **Instant Transition**: 是否立即切换场景（无延迟）

#### 触发设置
- **Contact Cooldown**: 接触冷却时间，防止重复触发（0.1-1秒，默认0.2秒）

### 3. 确保设置正确
1. 旗子对象必须有Animator组件
2. 旗子对象必须有Collider2D组件（脚本会自动设置为触发器）
3. 玩家对象必须有"Player"标签
4. 目标场景已添加到Build Settings

## 快速切换设置

### 方法1：使用Inspector设置
- 将 **Scene Transition Delay** 设置为 0
- 勾选 **Instant Transition**

### 方法2：使用代码设置
```csharp
EndTheScene endScene = GetComponent<EndTheScene>();
endScene.SetInstantTransition(true); // 立即切换
endScene.SetTransitionDelay(0f); // 无延迟
```

## 工作原理

### 状态检测
脚本会持续监听旗子的finish动画播放状态：
- 通过Animator的当前动画状态名称检测
- 每0.05秒检查一次，确保快速响应

### 触发条件
场景切换需要满足以下条件：
1. 旗子正在播放finish动画
2. 玩家与旗子发生接触
3. 不在冷却时间内
4. 没有正在切换场景

### 触发流程
1. 玩家接触旗子
2. 检查旗子是否处于finish状态
3. 如果条件满足，开始场景切换
4. 等待延迟时间（或立即切换）
5. 切换到下一个场景

## 调试信息
脚本会在Console中输出以下信息：
- "旗子状态变化 - Finish动画: X" - 状态变化
- "玩家接触旗子，立即触发场景切换" - 成功触发
- "旗子未处于finish状态，无法触发场景切换" - 条件不满足
- "开始切换到下一个场景" - 准备切换场景
- "立即切换到场景 X" - 立即切换场景
- "切换到场景 X" - 延迟切换场景
- "场景索引 X 无效，无法切换场景" - 场景错误

## 公共方法

### ForceTransitionToNextScene()
手动触发场景切换，忽略状态检查。

### ForceTransitionImmediate()
立即切换场景，忽略所有延迟设置。

### SetTransitionDelay(float delay)
设置场景切换延迟时间（0-3秒）。

### SetInstantTransition(bool instant)
设置是否立即切换场景。

### IsFlagReadyForTransition()
检查旗子是否可以触发场景切换。

### IsFinishAnimationPlaying()
获取当前是否正在播放finish动画。

### ResetTransitionState()
重置切换状态，允许重新触发。

## 性能优化
- **状态检查间隔**：0.05秒，确保快速响应
- **默认延迟**：0.2秒，减少等待时间
- **冷却时间**：0.2秒，提高触发频率
- **立即切换**：无延迟切换选项

## 注意事项
1. 确保旗子有正确的动画状态机设置
2. 确保finish动画名称正确（默认为"finshi"）
3. 确保玩家对象有"Player"标签
4. 脚本会自动将碰撞器设置为触发器
5. 如果场景索引无效，会重置状态允许重试
6. 使用立即切换时，确保场景加载速度足够快

## 示例配置

### 最快响应配置
- Scene Transition Delay: 0.0
- Instant Transition: ✅
- Contact Cooldown: 0.1
- Finish Animation Name: "finshi"

### 标准配置
- Scene Transition Delay: 0.2
- Instant Transition: ❌
- Contact Cooldown: 0.2
- Finish Animation Name: "finshi"

### 自定义动画名称
- Finish Animation Name: "your_finish_animation_name"
- 其他参数根据需要调整

## 与其他脚本的兼容性
- 可以与现有的FlagController脚本共存
- 不会影响其他旗子相关脚本的功能
- 提供了丰富的公共方法供其他脚本调用 