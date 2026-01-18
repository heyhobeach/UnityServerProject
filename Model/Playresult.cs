namespace UnityServerProject.Model
{

    public class QuizRecord
    {
        public string Answer { get; set; }
        public bool IsCorrect { get; set; }
    }
    public class Playresult
    {
        public int Id { get; set; }

        public bool IsNormal { get; set; }
        public double StartTime { get; set; }
        //수정
        public double EndTime { get; set; }
        public int DeadCount { get; set; }

    //     public Dictionary<string,int>stageDeaths{get;set;}=new ();
    //     public Dictionary<string,QuizRecord> QuizResults{get;set;}=new();
     
    }

    public class EndTimeUpdateRequest
    {
        public double EndTime{ get; set; }
    }
}
