using UnityEngine;

[RequireComponent(typeof(Attacker))]
public class FireSpawner : Spawner<Fire>
{
    [SerializeField] private float _offsetX = 1;
    [SerializeField] private float _offsetY = -0.5f;    
    
    private Attacker _attacker;

    protected override void Awake()
    {
        _attacker = GetComponent<Attacker>();
        base.Awake();
    }

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
        obj.transform.position = new Vector3(_attacker.transform.position.x + _offsetX, transform.position.y + _offsetY, 0);
        obj.gameObject.SetActive(true);

        base.ClearObjectTransform(obj);
    }
}
