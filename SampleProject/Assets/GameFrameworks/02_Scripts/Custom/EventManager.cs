namespace GameFrameworks
{
    public static partial class EventManager
    {
        [System.Flags]
        public enum EventType
        {
            NONE = 0,

            None = 0,
    
            #region BackEnd

            B_Login = 2000,
            
            NickChanged,
            
            CostDataChanged,
            HeroDataChanged, // 영웅 데이터 변경 (레벨업, 상점 구매)
            ItemDataChanged,
            QuestDataChanged,
            
            HeroTypeChanged, // 영웅 종류 바꾸기
            PetDataChanged,
            
            #endregion
            
            // InGame
            SummonUnit = 20000,
            InstantiateUnit,
            DestroyUnit,
            
            SummonMonster,
            InstantiateMonster,
            DestroyMonster,
            
            InstantiateBoss,
            DestroyBoss,
            
            InstantiateMdBoss,
            DestroyMdBoss,

            NextBossInfo,
            
            SummonMapResidue,
            InstantiateMapResidue,
            DestroyMapResidue,
            
            HeroStatChanged,
            HeroPowerStatChanged,
            
            HeroGetDamage,
            MonGetDamage,
            GetExp,
            GetExpRatio,
            LvUp,
            GetGold,
            GetHeroStone,
            GetItem,
            
            Timer,    // 1초마다 호출  
            TimerEnd, // 시간 종료
            
            GameFinished,
            
            OnClick_SkillSelect,
            SkillSelect,
            SkillAdded,
            SkillRemoved,
            
            UseSkill,
            
            // Hero 111
            TwistedCardSelect,
            
            // Hero112
            RobotCollided,
            
            // Hero 113
            CoinChanged,
            SkillDmgChanged,
            
            BuffAdded,
            BuffEnded,
            
            ShowBuffIcon,
            
            HeroStatAdded,
            
            GetMapItem,
            MapResidueStat,
            
            QuestTry,
            
            SelectTrait,
            TraitChanged,

            // Map3
            Map3FoundationDestroy,
            DayChanged, // 낮저밤이
            
            // Map4
            Map4BossDamaged,
            
            // Map7
            StatueTryActivate,
            
            // Map9
            SealDestroyed,
            FallenAngelHp,
            
            // Map8
            AchieveMiniQuest,
            ActivateOb,
            
            // UI
            OptionChanged,
            InAppCallBack,
            
            // Popup
            ShowPopup,
            
            PadBeginDrag,
            PadDrag,
            PadEndDrag,
            
            ButtonClicked,
            ApplySafeArea,
            
            SetTicker,
            
            // Lobby
            OnClick_PageBtn,
            PageChanged,
            OnClick_ItemElement,
            
            ContentsOpen,
            
            SceneChanged,
            
            GetDia,
            GetDropItem,
            
            // Map14
            SoulCntChanged,


            Design_Upgrade = 1 << 1,
            Design_StageClearGold = 1 << 2,

            HeroAni = 1 << 3,
            MonsterSpawn = 1 << 4,
            Rush = 1 << 5,
            Matchable = 1 << 6,
            TouchPos = 1 << 7,
            Tile = 1 << 8,
            Combo = 1 << 9,
            Event = 1 << 10,
            Dialogue = 1 << 11,
            PropAction = 1 << 12,
            Event_Begin = 1 << 13,
            Event_Condition = 1 << 14,
            Event_Action = 1 << 15,
            Direction = 1 << 16,

            #region FrameWork
            Common = 1 << 30,
            Save = 1 << 29,
            TileSystem_C = 1 << 28,

            DB_ExcelToString = 1 << 27,
            DB_StringToJson = 1 << 26,
            DB_Generate = 1 << 25,
            DB_ExcelToString_IgnoreCol = 1 << 24,
            DB_GoogleAccess = 1 << 23,

            DB_Json = DB_ExcelToString | DB_StringToJson | DB_ExcelToString_IgnoreCol,
            #endregion

            All = ~NONE,
            EventAll = Event | Event_Begin | Event_Condition | Event_Action,
        }
    } 
}