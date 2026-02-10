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
        public long Duration { get; set; }
        public int DeadCount { get; set; }

        //StageDeathInfo와 연결하기 위해 필한 부분
        // (의도적으로 주석 처리했음: PlayerChapterInfo를 통해서만 접근하도록 계층 구조 유지)
        //public List<StageDeathInfo> StageDeathInfos {get;set;}=new();

        //여기서 부터 새로 추가 하고 있는 부분 테스트 필요
        public List<PlayerChapterInfo> PlayerChapterInfos {get;set;}=new();

        //심문, 일지, 빠른 추리 관련으로 정답지 정할 클래스
    //     public Dictionary<string,int>stageDeaths{get;set;}=new ();
    //     public Dictionary<string,QuizRecord> QuizResults{get;set;}=new();
     
    }



    public class EndTimeUpdateRequest
    {
        public long EndTime{ get; set; }
        public long Duration{ get; set; }
    }
    public class PlayerStageDeathinfoDTO
    {
        public int PlayresultId{get;set;}
        public string StageName{get;set;}
        public int DeathId{get;set;}
        public int StageId{get;set;}

        public string ?EnemyName { get; set; }
        public float DeathPositionX{get;set;}
        public float DeathPositionY{get;set;}

        public float EnemyPositionX{get;set;}
        public float EnemyPositionY{get;set;}
        // public Vector2 DeathPosition { get; set; }
        // public Vector2 EnemyPosition { get; set; }
    }
    
}
