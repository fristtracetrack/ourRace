# FlagFinishTrigger 脚本使用说明

## 功能描述
这个脚本实现了当旗子升起后（处于finish状态），只有player与旗子接触才能切换到下一个场景的功能。

## 核心特性
- ✅ 检测旗子升起状态
- ✅ 检测finish动画播放状态
- ✅ 玩家接触触发场景切换
- ✅ 防止重复触发
- ✅ 可配置的触发条件
- ✅ 详细的调试信息

## 使用方法

### 1. 添加脚本到旗子对象
1. 选择您的旗子GameObject
2. 将 `FlagFinishTrigger.cs` 脚本添加到该对象上

### 2. 配置脚本参数

#### 组件引用
- **Flag Animator**: 旗子的Animator组件（自动获取）
- **Flag Collider**: 旗子的碰撞器组件（自动获取）

#### 动画设置
- **Finish Animation Name**: finish动画的名称（默认为"finshi"）
- **Up Animation Name**: 升起动画的名称（默认为"up"）

#### 场景设置
- **Next Scene Index**: 下一个场景的索引
  - 设置为-1：自动切换到下一个场景
  - 设置为具体数字：切换到指定场景
- **Scene Transition Delay**: 场景切换延迟时间（秒）

#### 触发设置
- **Require Finish Animation**: 是否需要播放finish动画才能触发
  - ✅ 勾选：必须播放finish动画才能触发
  - ❌ 取消：只要旗子升起就能触发
- **Contact Cooldown**: 接触冷却时间，防止重复触发

### 3. 确保设置正确
1. 旗子对象必须有Animator组件
2. 旗子对象必须有Collider2D组件（脚本会自动设置为触发器）
3. 玩家对象必须有"Player"标签
4. 目标场景已添加到Build Settings

## 工作原理

### 状态检测
脚本会持续监听以下状态：
1. **旗子升起状态**：通过Animator的"up"参数检测
2. **Finish动画状态**：通过当前播放的动画名称检测

### 触发条件
场景切换需要满足以下条件：
1. 旗子已升起（`isFlagRaised = true`）
2. 如果需要finish动画，则必须正在播放finish动画
3. 玩家与旗子发生接触
4. 不在冷却时间内
5. 没有正在切换场景

### 触发流程
1. 玩家接触旗子
2. 检查触发条件
3. 如果条件满足，开始场景切换
4. 等待延迟时间
5. 切换到下一个场景

## 调试信息
脚本会在Console中输出以下信息：
- "旗子状态变化 - 升起: X, Finish动画: Y" - 状态变化
- "玩家接触旗子，触发场景切换" - 成功触发
- "旗子未升起或未播放finish动画，无法触发场景切换" - 条件不满足
- "开始切换到下一个场景" - 开始切换
- "切换到场景 X" - 实际切换
- "场景索引 X 无效，无法切换场景" - 场景错误

## 公共方法

### ForceTransitionToNextScene()
手动触发场景切换，忽略所有条件检查。

### IsFlagReadyForTransition()
检查旗子是否可以触发场景切换。

### IsFlagRaised()
获取当前旗子是否已升起。

### IsFinishAnimationPlaying()
获取当前是否正在播放finish动画。

### ResetTransitionState()
重置切换状态，允许重新触发。

## 事件系统

### OnFlagStateChanged
当旗子状态发生变化时触发的事件：
```csharp
flagFinishTrigger.OnFlagStateChanged += (bool canTrigger) => {
    Debug.Log($"旗子可以触发: {canTrigger}");
};
```

## 示例配置

### 基础配置
- Finish Animation Name: "finshi"
- Up Animation Name: "up"
- Next Scene Index: -1
- Scene Transition Delay: 1.0
- Require Finish Animation: ✅
- Contact Cooldown: 0.5

### 简单配置（不需要finish动画）
- Require Finish Animation: ❌
- 其他参数保持默认

## 注意事项
1. 确保旗子有正确的动画状态机设置
2. 确保玩家对象有"Player"标签
3. 脚本会自动将碰撞器设置为触发器
4. 如果场景索引无效，会重置状态允许重试
5. 可以通过事件系统监听状态变化

## 与其他脚本的兼容性
- 可以与现有的FlagController脚本共存
- 不会影响其他旗子相关脚本的功能
- 提供了丰富的公共方法供其他脚本调用 