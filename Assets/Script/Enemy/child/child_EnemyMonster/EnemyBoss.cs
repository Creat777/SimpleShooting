using UnityEngine;

public class EnemyBoss : EnemyMonster
{
    private void Awake()
    {
        gameObject.SetActive(false);
    }
    public override void Move()
    {
        
    }

    public override void OutOfScreen()
    {
        gameObject.SetActive(false);
        GameManager.Instance.GameOver();
    }

    public override void WhenDamaged(GameObject bullet)
    {
        if (bullet.tag == "MainBullet")
            HpDown(bullet.GetComponent<MainBullet>().damage);
        else if (bullet.tag == "SubBullet")
            HpDown(bullet.GetComponent<SubBullet>().damage);
        if (animator.GetBool("isDamaged") == false)
            AnimeChange();
    }
}
