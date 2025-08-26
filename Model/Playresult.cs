namespace UnityServerProject.Model
{
    public class Playresult
    {
        public int Id { get; set; }

        public bool IsNormal { get; set; }
        public float StartTime { get; set; }

        public float EndTime { get; set; }
        public int DeadCount { get; set; }
    }

    public class EndTimeUpdateRequest
    {
        public float EndTime{ get; set; }
    }
}
