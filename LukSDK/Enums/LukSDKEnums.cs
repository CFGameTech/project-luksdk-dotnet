namespace LukSDK.Enums
{
    public static class LukSDKEnums
    {
        public static class Identity { public const int Owner = 1, Admin = 2, Normal = 3; }
        public static class ControlEventType
        {
            public const int JoinGame = 1;
            public const int LeaveGame = 2;
            public const int ChangeReadyStatus = 3;
            public const int KickPlayer = 4;
            public const int StartGame = 5;
            public const int ForceCloseGame = 6;
            public const int ChangeRoomSetting = 7;
            public const int ChangeUserIdentity = 8;
            public const int RoomSeatSync = 9;
            public const int RefreshUserInfo = 10;
            public const int QuickStartGame = 11;
            public const int AddRobot = 13;
            public const int UserItemGrant = 1000;
            public const int FetchBagStatus = 1001;
            public const int QueryIssuePropStatus = 1002;
            public const int EquippedProp = 1003;
            public const int UnequippedProp = 1004;
        }
    }
}
