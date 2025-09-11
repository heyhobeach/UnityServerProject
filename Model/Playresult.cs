namespace UnityServerProject.Model
{
    public class Playresult
    {
        public int Id { get; set; }

        public bool IsNormal { get; set; }
        public long StartTime { get; set; }
        //수정
        public long EndTime { get; set; }
        public int DeadCount { get; set; }
    }

    public class EndTimeUpdateRequest
    {
        public long EndTime{ get; set; }
    }
}
