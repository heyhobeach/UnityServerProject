namespace UnityServerProject.Model
{

    public class QuizRecord
    {
        public string ?Answer { get; set; }
        public bool IsCorrect { get; set; }
    }

    public class Playresult
    {
        public int Id { get; set; }

        //long을 사용한 이유는 db big int와 맞춰주기 위함
        public bool IsNormal { get; set; }
        public long StartTime { get; set; }
        //수정
        public long EndTime { get; set; }
        public int DeadCount { get; set; }

        //StageDeathInfo와 연결하기 위해 필한 부분
        public List<StageDeathInfo> StageDeathInfos {get;set;}=new();

    //     public Dictionary<string,int>stageDeaths{get;set;}=new ();
    //     public Dictionary<string,QuizRecord> QuizResults{get;set;}=new();
     
    }

    public class EndTimeUpdateRequest
    {
        public long EndTime{ get; set; }
    }
}
