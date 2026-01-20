namespace UnityServerProject.Model{
    public class PositionDTO
    {
        public float x{get;set;}
        public float y{get;set;}
    }

    public class DeathInfoRequest
    {
        public string EnemyName{get;set;}
        public PositionDTO deathPosition{get;set;}
        public PositionDTO EnemyPosition{get;set;}
    }

    public class StageDeathInfoRequest
    {
        public int stageId {get;set;}
        public int playresultId{get;set;}
        public string stageName{get;set;}
        public int DeathCount{get;set;}
        public List<DeathInfoRequest> deathinfos {get;set;}
    }
    public class RootRequest
    {
        public List<StageDeathInfoRequest> stageDeathInfos {get;set;}
    }



}