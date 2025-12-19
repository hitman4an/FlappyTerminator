using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _offset;

    private void LateUpdate()
    {
        Vector3 position = transform.position;

        position.x = _player.transform.position.x + _offset;
        position.z = -10f;
        transform.position = position;
    }
}
