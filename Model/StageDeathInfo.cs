using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
namespace UnityServerProject.Model
{

    public class StageDeathInfos
    {
        public List<StageDeathInfo> stageDeathInfos{get;set;} = new List<StageDeathInfo>();
    } 


    //실제 DB에 저장되는 적의 죽음에 대한 정보
    //부모는 Playresult 클래스 각각의 클래스가 테이블의 역할을 하고 있음
    public class StageDeathInfo
    {
        [Key]
        public int StageId{get;set;}

        //어떤 스테이지에서 사망했는가
        public int PlayresultId{get;set;}
        public string ?StageName { get; set; }
        public int DeathCount { get; set; }
        
        public List<DeathInfo> DeathInfos { get; set; } = new List<DeathInfo>();
    }

    

    //죽은 위치에 대한 정보를 담고 있는 클래스
    public class DeathInfo
    {
        [Key]   
        public int DeathId{get;set;}
        public int StageId{get;set;}

        [ForeignKey("StageId")]
        [System.Text.Json.Serialization.JsonIgnore]
        public StageDeathInfo StageDeathInfo { get; set; }

        public string ?EnemyName { get; set; }
        public float DeathPositionX{get;set;}
        public float DeathPositionY{get;set;}

        public float EnemyPositionX{get;set;}
        public float EnemyPositionY{get;set;}
        // public Vector2 DeathPosition { get; set; }
        // public Vector2 EnemyPosition { get; set; }
    }
}
