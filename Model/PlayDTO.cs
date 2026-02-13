namespace UnityServerProject.Model{
    //DTO : Data Transfer Object
    //데이터 전송 및 수신을 위한 객체


    //PositionDTO 클래스의 경우 Vector2상태로 클라이언트에서 전송 오는 데이터를 db에서는 저장할 수 없기 때문에 float x, float y 형태로 분리해 직접 vector2로 변환 해서 하는것
    public class PositionDTO
    {
        public float x{get;set;}
        public float y{get;set;}
    }

//적의 죽음에 대한 정보를 담고 있는 DTO
    public class DeathInfoRequest
    {
        //적의 이름을 답고 있음
        public string EnemyName{get;set;}
        
        //PositionDTO 는 사실상 vector2 역할
        public PositionDTO deathPosition{get;set;}
        public PositionDTO EnemyPosition{get;set;}
    }


    //특정 스테이지에서 몇 번 죽었는지 죽은 위치와 정보를 담고있는 클래스 
    public class StageDeathInfoRequest
    {
        public int stageId {get;set;}
        public int playresultId{get;set;}
        public string StageName{get;set;}
        public int DeathCount{get;set;}
        public List<DeathInfoRequest> deathinfos {get;set;}
    }
    public class RootRequest
    {
        public List<StageDeathInfoRequest> stageDeathInfos {get;set;}
        //public long StagePlayTime {get;set;}
    }
    public class ChapterClearInfoRequest
    {
        public long ChapterDuration{get;set;}
        public string ?ChapterName{get;set;}

    }



}