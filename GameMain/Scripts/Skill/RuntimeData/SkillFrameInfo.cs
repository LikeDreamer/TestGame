namespace AltarOfSword
{
    public class SkillFrameInfo : SkillRuntimeDataPart
    {
        public bool IsChange => Previous != Current;
        public int Fps { get; private set; }         //fps
        public float Rate { get; private set; }      //帧率
        public float Time { get; private set; }      //零时时间技术
        public int Current { get; private set; }     //当前帧标记
        public int Previous { get; private set; }    //上一帧编号，技能中断或停止时，需要比current小，否则第0帧不会被执行
        public float TotalTime { get; private set; } //当前帧时间
        public int Count { get; set; }       //帧数计数
        public SkillFrameInfo(SkillRuntimeData runtimeData) : base(runtimeData)
        {
            SetFps(SkillDefined.FPS);
        }

        public void SetFps(int fps)
        {
            this.Fps = fps;
            Rate = 1.0f / fps;
            Reset();
            Previous = 0;
        }

        public void OnUpdate(float deltaTime)
        {
            Previous = Current;
            TotalTime += deltaTime;
            Time += deltaTime;

            while (Time >= Rate)
            {
                Time -= Rate;
                Current++;
            }
        }

        public void ChangeFrameCount(int count)
        {
            this.Count += count;
        }

        public void OnFrameUpdateOver(int count)
        {
            this.Count += count;
        }
        
        public void Reset()
        {
            TotalTime = Time;
            Previous = -1;
            Current = 0;
            Count = 0;
        }

        public override void Dispose()
        {
            Reset();
        }
    }
}