using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class mover : MonoBehaviour
{
    [SerializeField] private RawImage _img;
    [SerializeField] private float _speed;
    [SerializeField] private Timer Timer;
    [SerializeField] private bool ecg;

    private Vector2 _direction;
    private Coroutine _changeDirectionCoroutine;
    [SerializeField] private float _x, _y;

 
    void Update()
    {
        if (ecg == true)
            if (Timer.anim.isPlaying == true)
                _img.uvRect = new Rect(_img.uvRect.position + new Vector2(_x,_y) * Time.deltaTime,_img.uvRect.size);
            else
                _img.uvRect = new Rect(_img.uvRect.position + new Vector2(0,0) * Time.deltaTime,_img.uvRect.size);
        else
        {
            MoveImage(_direction);
        }
    }

    private void Start()
    {
        _direction = GetRandomDirection();
        _changeDirectionCoroutine = StartCoroutine(ChangeDirectionRoutine());
    }

    private void MoveImage(Vector2 direction)
    {
        _img.uvRect = new Rect(_img.uvRect.position + direction * _speed * Time.deltaTime, _img.uvRect.size);
    }

    private Vector2 GetRandomDirection()
    {
        float angle = Random.Range(0f, 360f);
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
    }

    private IEnumerator ChangeDirectionRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3f, 7f));
            _direction = GetRandomDirection();
        }
    }

    private void OnDestroy()
    {
        if (_changeDirectionCoroutine != null)
        {
            StopCoroutine(_changeDirectionCoroutine);
        }
    }
}
