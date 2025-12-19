using UnityEngine;

public class FireSpawner : Spawner<Fire>
{
    [SerializeField] private float _offsetX = 1;
    [SerializeField] private float _offsetY = -0.5f;
    [SerializeField] private bool _isLeftDirection;
    
    public void CreateFire()
    {
        if (IsActive)
        {
            Fire obj = Pool.Get();

            obj.DestroyObj += ReleaseObject;
        }
    }

    protected override void ClearObjectTransform(Fire obj)
    {
        obj.transform.position = new Vector3(transform.position.x + _offsetX, transform.position.y + _offsetY, 0);
        obj.transform.rotation = transform.rotation;
        obj.SetDirection(new Vector2(_isLeftDirection ? - 1 : 1, transform.rotation.z));
        obj.gameObject.SetActive(true);

        base.ClearObjectTransform(obj);
    }
}
