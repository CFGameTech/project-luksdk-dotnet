namespace LukSDK.Enums
{
    /// <summary>
    /// LukSDK 枚举定义
    /// </summary>
    public static class LukSDKEnums
    {
        /// <summary>
        /// 身份枚举
        /// </summary>
        public static class Identity
        {
            /// <summary>
            /// 房主
            /// </summary>
            public const int Owner = 1;

            /// <summary>
            /// 管理员
            /// </summary>
            public const int Admin = 2;

            /// <summary>
            /// 普通用户
            /// </summary>
            public const int Normal = 3;
        }

        /// <summary>
        /// 控制事件类型
        /// </summary>
        public static class ControlEventType
        {
            /// <summary>
            /// 加入游戏
            /// </summary>
            public const int JoinGame = 1;

            /// <summary>
            /// 离开游戏
            /// </summary>
            public const int LeaveGame = 2;

            /// <summary>
            /// 开始游戏
            /// </summary>
            public const int StartGame = 3;

            /// <summary>
            /// 结束游戏
            /// </summary>
            public const int EndGame = 4;

            /// <summary>
            /// 暂停游戏
            /// </summary>
            public const int PauseGame = 5;

            /// <summary>
            /// 恢复游戏
            /// </summary>
            public const int ResumeGame = 6;

            /// <summary>
            /// 用户道具授予
            /// </summary>
            public const int UserItemGrant = 7;

            /// <summary>
            /// 用户道具消耗
            /// </summary>
            public const int UserItemConsume = 8;

            /// <summary>
            /// 用户金币授予
            /// </summary>
            public const int UserCoinsGrant = 9;

            /// <summary>
            /// 用户金币消耗
            /// </summary>
            public const int UserCoinsConsume = 10;

            /// <summary>
            /// 用户积分授予
            /// </summary>
            public const int UserScoreGrant = 11;

            /// <summary>
            /// 用户积分消耗
            /// </summary>
            public const int UserScoreConsume = 12;

            /// <summary>
            /// 用户升级
            /// </summary>
            public const int UserLevelUp = 13;

            /// <summary>
            /// 用户成就解锁
            /// </summary>
            public const int UserAchievementUnlock = 14;

            /// <summary>
            /// 房间创建
            /// </summary>
            public const int RoomCreate = 15;

            /// <summary>
            /// 房间销毁
            /// </summary>
            public const int RoomDestroy = 16;

            /// <summary>
            /// 加入房间
            /// </summary>
            public const int RoomJoin = 17;

            /// <summary>
            /// 离开房间
            /// </summary>
            public const int RoomLeave = 18;

            /// <summary>
            /// 踢出房间
            /// </summary>
            public const int RoomKick = 19;

            /// <summary>
            /// 房间设置变更
            /// </summary>
            public const int RoomSettingChange = 20;

            /// <summary>
            /// 游戏设置变更
            /// </summary>
            public const int GameSettingChange = 21;

            /// <summary>
            /// 用户状态变更
            /// </summary>
            public const int UserStatusChange = 22;

            /// <summary>
            /// 系统公告
            /// </summary>
            public const int SystemAnnouncement = 23;

            /// <summary>
            /// 自定义事件
            /// </summary>
            public const int CustomEvent = 24;
        }
    }
}