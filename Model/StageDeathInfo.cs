using System.ComponentModel.DataAnnotations;
using System.Numerics;
namespace UnityServerProject.Model
{
    //여기 내용의 구조를 수정 할 필요있음
    //수정 필요내용 목록
    /*
    1. 현재 강제로 KEY 키워드를 통해서 어떻게 키를 부여하고 사용중이기는 하나 ID로 구별해야함
    1.1 StageDeathInfo 같은 경우는 stageid 같은 내용처럼 stage를 구별하는 id가 필요함
    1.2 DeathInfo의 경우는  지금 처럼 DeathId로 진행해야함

    2. DeathInfos의 경우 EFCore의 경우 자동으로 리스트는 외래키를 생성해줌 따라서 StageDeathInfo의 리스트는 유지
    3. DeathInfo의 경우 Vector2는 직접 지정해줄 필요 있음 따라서 DeathPositionX DeathPositionY EnemyPositionX EnemyPositionY를 float 형태로 해서 정의 해야함
    4. 클라이언트에서 vector2로 전송하는건 그대로 하되 서버에서 json을 받아서 분해해서 저장하는식으로 접근해야함
    */
    public class StageDeathInfo
    {
        [Key]
        public string ?StageName { get; set; }
        public int DeathCount { get; set; }
        // public List<DeathInfo> DeathInfos { get; set; } = new List<DeathInfo>();
    }

    public class DeathInfo
    {
        [Key]
        public int DeathId{get;set;}
        public string ?EnemyName { get; set; }
        // public Vector2 DeathPosition { get; set; }
        // public Vector2 EnemyPosition { get; set; }
    }
}
