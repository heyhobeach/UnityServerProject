using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
namespace UnityServerProject.Model
{
        //챕터 정보 기록할 클래스
    public class PlayerChapterInfo
    {
        [Key]
        public int ChapterId{get;set;}
        public int PlayresultId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public Playresult? Playresult { get; set; }
        public string ?ChapterName{get;set;}
        public long ChapterDuration{get;set;}
        public List<StageDeathInfo> StageDeathInfos {get;set;}=new();
    }



    // 실제 DB에 저장되는 맵(스테이지) 단위의 죽음 정보
    public class StageDeathInfo
    {
        [Key]
        public int StageId { get; set; }

        // Chapter와의 관계
        public int ChapterId { get; set; }
        [ForeignKey("ChapterId")]
        [System.Text.Json.Serialization.JsonIgnore]
        public PlayerChapterInfo? PlayerChapter { get; set; }

        // 어느 플레이 결과(플레이 세션)에 속하는지
        public int PlayresultId { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public Playresult? Playresult { get; set; }

        public string? StageName { get; set; }
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

        public float EnemyPositionX { get; set; }
        public float EnemyPositionY { get; set; }
    }
}
