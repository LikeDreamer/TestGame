using Google.Protobuf.Collections;


namespace AltarOfSword
{
    public static class SkillDefined
    {
         //SkillFrameRate
        public const int FPS = 60;//帧率
        public const float FrameRate = 1.0f/FPS;//帧率
        //SkillState
        public const int SS_None = 0;//空数据
        public const int SS_Idle = SS_None + 1;//空闲中
        public const int SS_Update = SS_Idle + 1;//释放中
        public const int SS_Over = SS_Update + 1;//当数据的Leave执行完成时候切换到结束状态
        public const int SS_Forbid = SS_Over + 1;//禁用状态
        public const int SS_Cooling = SS_Forbid + 1;//冷却中

        //ActorState
        public const int AS_Grounded = 0;//在地面
        public const int AS_Airborne = 1;//在空中
        public const int AS_Other = 2;//其他状态

        //DirectionState
        public const int DS_Left = 1;//向左
        public const int DS_Right = -1;//向右

        //空技能ID
        public const int NullSkillID = -1;//向左

        //SkillType   Grounded Airborne
        public const int ST_Grounded = 0;//在地面
        public const int ST_Airborne = 1;//在空中
        public const int ST_GroundedTemp = 2;//地面零时
        public const int ST_AirborneTemp = 3;//在空中零时
        public const int ST_NullCMD = 4;//非命令触发技能，如下落等等

        //SkillValueModifierKey
        public const int SVMK_SlowFrameRate = 0;                    //缓帧系数
        public const int SVMK_GravityRate = SVMK_SlowFrameRate + 1; //1 重力加速度
        public const int SVMK_VelocityY = SVMK_GravityRate + 1;     //2 垂直速度
        public const int SVMK_VelocityX = SVMK_VelocityY + 1;       //3 水平速度
        public const int SVMK_CBoxSiteX = SVMK_VelocityX + 1;       //4 碰撞框X偏移 Collison
        public const int SVMK_CBoxSiteY = SVMK_CBoxSiteX + 1;       //5 碰撞框Y偏移
        public const int SVMK_CBoxSizeX = SVMK_CBoxSiteY + 1;       //6 碰撞框X大小
        public const int SVMK_CBoxSizeY = SVMK_CBoxSizeX + 1;       //7 碰撞框Y大小
        public const int SVMK_BBoxSiteX = SVMK_CBoxSizeY + 1;       //8 受击框X偏移 Behit
        public const int SVMK_BBoxSiteY = SVMK_BBoxSiteX + 1;       //9  受击框Y偏移
        public const int SVMK_BBoxSizeX = SVMK_BBoxSiteY + 1;       //10  受击框X大小
        public const int SVMK_BBoxSizeY = SVMK_BBoxSizeX + 1;       //11  受击框Y大小
        public const int SVMK_SBoxSiteX = SVMK_BBoxSizeY + 1;       //12  防御框X偏移 Shield
        public const int SVMK_SBoxSiteY = SVMK_SBoxSiteX + 1;       //13  防御框Y偏移
        public const int SVMK_SBoxSizeX = SVMK_SBoxSiteY + 1;       //14  防御框X大小
        public const int SVMK_SBoxSizeY = SVMK_SBoxSizeX + 1;       //15  防御框Y大小
        public const int SVMK_Max = SVMK_SBoxSizeY + 1;             //16  最大值

        public const int ECMD_None = 0; //0
        public const int ECMD_Nothing = ECMD_None + 1;  //1
        public const int ECMD_Hrzt = ECMD_Nothing + 1;  //2
        public const int ECMD_Vert = ECMD_Hrzt + 1; //3
        public const int ECMD_Jump = ECMD_Vert + 1; //4
        public const int ECMD_HrztD = ECMD_Jump + 1;    //5
        public const int ECMD_VertD = ECMD_HrztD + 1;   //6
        public const int ECMD_JumpD = ECMD_VertD + 1;   //7
        public const int ECMD_KeyA = ECMD_JumpD + 1;    //8
        public const int ECMD_KeyD = ECMD_KeyA + 1; //9
        public const int ECMD_KeyW = ECMD_KeyD + 1; //10
        public const int ECMD_KeyS = ECMD_KeyW + 1; //11
        public const int ECMD_KeyR = ECMD_KeyS + 1; //12
        public const int ECMD_Key1 = ECMD_KeyR + 1; //13
        public const int ECMD_Key2 = ECMD_Key1 + 1; //14
        public const int ECMD_Key3 = ECMD_Key2 + 1; //15
        public const int ECMD_Key4 = ECMD_Key3 + 1; //16
        public const int ECMD_KeyW1 = ECMD_Key4 + 1;    //17
        public const int ECMD_KeyW2 = ECMD_KeyW1 + 1;   //18
        public const int ECMD_KeyW3 = ECMD_KeyW2 + 1;   //19
        public const int ECMD_KeyW4 = ECMD_KeyW3 + 1;   //20
        public const int ECMD_KeyS1 = ECMD_KeyW4 + 1;   //21
        public const int ECMD_KeyS2 = ECMD_KeyS1 + 1;   //22
        public const int ECMD_KeyS3 = ECMD_KeyS2 + 1;   //23
        public const int ECMD_KeyS4 = ECMD_KeyS3 + 1;   //24
        public const int ECMD_KeyTab = ECMD_KeyS4 + 1;  //25
        public const int ECMD_KeyF = ECMD_KeyTab + 1;   //26
        public const int ECMD_KeyQ = ECMD_KeyF + 1; //27
        public const int ECMD_KeyE = ECMD_KeyQ + 1; //28
        public const int ECMD_KeyShift = ECMD_KeyE + 1; //29
        public const int ECMD_KeyCtrl = ECMD_KeyShift + 1;  //30
        public const int ECMD_MouseL = ECMD_KeyCtrl + 1;    //31
        public const int ECMD_MouseR = ECMD_MouseL + 1; //32
        public const int ECMD_MouseM = ECMD_MouseR + 1; //33
        public const int ECMD_MouseLD = ECMD_MouseM + 1;    //34
        public const int ECMD_MouseRD = ECMD_MouseLD + 1;   //35
        public const int ECMD_MouseMD = ECMD_MouseRD + 1;   //36
        public const int ECMD_KeyWML = ECMD_MouseMD + 1;    //37
        public const int ECMD_KeySML = ECMD_KeyWML + 1; //38
        public const int ECMD_KeyWMR = ECMD_KeySML + 1; //39
        public const int ECMD_KeySMR = ECMD_KeyWMR + 1; //40
        public const int ECMD_KeyWMM = ECMD_KeySMR + 1; //41
        public const int ECMD_KeySMM = ECMD_KeyWMM + 1; //42
        public const int ECMD_KeyWSpece = ECMD_KeySMM + 1;  //43
        public const int ECMD_KeySSpece = ECMD_KeyWSpece + 1;   //44
        public const int ECMD_Max = ECMD_KeySSpece + 1;	//45

    }
}
