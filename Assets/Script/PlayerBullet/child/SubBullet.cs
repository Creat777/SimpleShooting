using UnityEngine;

public class SubBullet : Bullet
{
    

    private void Start()
    {
        damage = 3;
    }

    public override void OutOfScreen()
    {
        //Debug.Log(screenTop.ToString());
        //Debug.Log("�Ѿ� �Ҹ�");
        BulletFactory.Instance.ReturnSubBullet(gameObject);
    }
}
