using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject prefab;
    public List<Transform> firepoint;
    public float power;
    public Vector3 target;
    public Texture2D cursor;

    private void Start()
    {
        Cursor.SetCursor(cursor, new Vector2(cursor.width / 2, cursor.height / 2), CursorMode.ForceSoftware);
    }

    public bool stopTimer;

    private void Update()
    {
        Setup();
    }

    private void Setup()
    {
        SetTargetPosition();
        SetRotation();
        SetCursor();

        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    private void SetCursor()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = Camera.main.nearClipPlane;
    }

    private void SetTargetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit))
        {
            return;
        }
        target = hit.point;
    }

    private void SetRotation()
    {
        Vector3 rotation = (target - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(rotation, Vector3.up);
    }

    private void Fire()
    {
        var direction = (target - transform.position).normalized;

        for (int i = 0; i < firepoint.Count; i++)
        {
            var instance = Instantiate(prefab, firepoint[i].position, Quaternion.LookRotation(direction)).GetComponent<Bullet>();
            instance.direction = direction;
        }
    }
}
