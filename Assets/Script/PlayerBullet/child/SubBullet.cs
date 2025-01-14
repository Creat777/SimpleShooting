using UnityEngine;

public class SubBullet : Bullet
{
    public int damage { get; private set; }

    private void Start()
    {
        damage = 3;
    }

    public override void OutOfScreen()
    {
        //Debug.Log(screenTop.ToString());
        //Debug.Log("ÃÑ¾Ë ¼Ò¸ê");
        BulletFactory.Instance.ReturnSubBullet(gameObject);
    }
}
