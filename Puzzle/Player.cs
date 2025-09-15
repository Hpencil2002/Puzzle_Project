using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Grid / Tilemap")]
    [Tooltip("월드의 Grid나 타일맵 상위의 Grid를 할당. 비워두면 cellSize = (1, 1)로 동작.")]
    public Grid grid;

    [Header("Movement")]
    [Tooltip("한 칸 이동에 걸리는 시간(초)")]
    public float speed = 0.3f;
    [Tooltip("장애물로 취급할 레이어 마스크")]
    public LayerMask obstacleMask;

    Rigidbody2D rigidbody2d;
    Collider2D collider2d;
    Animator animator;

    bool isMoving;
    Vector3 targetWorld;

    static readonly int HashIsMoving = Animator.StringToHash("IsMoving");
    static readonly int HashDirection = Animator.StringToHash("Direction");

    Vector2 CellSize => grid != null ? (Vector2)grid.cellSize : Vector2.one;

    [Header("Talk")]
    [SerializeField] Vector3 dirVec;
    [SerializeField] GameObject scanObject;

    [Header("Warp")]
    [SerializeField] bool canWarp;
    [SerializeField] Transform TargetTransform = null;

    [Header("Scene Change")]
    [SerializeField] bool isClear;
    [SerializeField] Goal goal;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider2d = GetComponent<Collider2D>();
    }

    void Start()
    {
        SnapToGrid();
    }

    void Update()
    {
        if (isMoving)
        {
            return;
        }

        Vector2Int dir = Vector2Int.zero;
        if (Input.GetKey(KeyCode.W))
        {
            dir = Vector2Int.up;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            dir = Vector2Int.down;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            dir = Vector2Int.right;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            dir = Vector2Int.left;
        }

        if (Input.GetKeyDown(KeyCode.Space) && scanObject != null)
        {
            GameManager.instance.Action(scanObject);
        }

        if (Input.GetKeyDown(KeyCode.F) && isClear)
        {
            isClear = false;
            goal.GoToNextScene();
        }

        if (Input.GetKeyDown(KeyCode.E) && canWarp)
        {
            canWarp = false;
            gameObject.transform.position = TargetTransform.position;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (dir == Vector2Int.zero || GameManager.instance.isAction)
        {
            return;
        }

        SetDirectionParam(dir);

        if (CanMove(dir))
        {
            Vector3Int currentCell = WorldToCell(transform.position);
            Vector3Int nextCell = currentCell + new Vector3Int(dir.x, dir.y, 0);

            targetWorld = CellCenterToWorld(nextCell);
            StartCoroutine(MoveOneTile(targetWorld));
        }
        else
        {
            animator.SetBool(HashIsMoving, false);
        }
    }

    void FixedUpdate()
    {
        // Ray
        Debug.DrawRay(rigidbody2d.position, dirVec * 0.7f, Color.red);
        RaycastHit2D rayHit = Physics2D.Raycast(rigidbody2d.position, dirVec, 0.7f, LayerMask.GetMask("Object"));

        if (rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
        }
        else
        {
            scanObject = null;
        }
    }

    IEnumerator MoveOneTile(Vector3 target)
    {
        isMoving = true;
        animator.SetBool(HashIsMoving, true);

        Vector3 start = rigidbody2d.position;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / Mathf.Max(0.0001f, speed);
            Vector3 p = Vector3.Lerp(start, target, Mathf.Clamp01(t));
            rigidbody2d.MovePosition(p);

            yield return null;
        }

        rigidbody2d.MovePosition(target);

        animator.SetBool(HashIsMoving, false);
        isMoving = false;
    }

    void SetDirectionParam(Vector2Int dir)
    {
        int d;

        if (dir == Vector2Int.down)
        {
            d = 0;
            dirVec = Vector3.down;
        }
        else if (dir == Vector2Int.up)
        {
            d = 1;
            dirVec = Vector3.up;
        }
        else if (dir == Vector2Int.right)
        {
            d = 2;
            dirVec = Vector3.right;
        }
        else
        {
            d = 3;
            dirVec = Vector3.left;
        }

        animator.SetInteger(HashDirection, d);
    }

    void SnapToGrid()
    {
        Vector3Int cell = WorldToCell(transform.position);
        Vector3 snapped = CellCenterToWorld(cell);
        rigidbody2d.position = snapped;
    }

    Vector3Int WorldToCell(Vector3 worldPos)
    {
        if (grid != null)
        {
            return grid.WorldToCell(worldPos);
        }

        int x = Mathf.RoundToInt(worldPos.x / CellSize.x);
        int y = Mathf.RoundToInt(worldPos.y / CellSize.y);
        return new Vector3Int(x, y, 0);
    }

    Vector3 CellCenterToWorld(Vector3Int cell)
    {
        if (grid != null)
        {
            return grid.GetCellCenterWorld(cell);
        }

        return new Vector3(cell.x * CellSize.x, cell.y * CellSize.y, 0f);
    }

    bool CanMove(Vector2Int dir)
    {
        Vector3Int nextCell = WorldToCell(transform.position) + new Vector3Int(dir.x, dir.y, 0);
        Vector3 nextCenter = CellCenterToWorld(nextCell);

        Vector2 size = collider2d.bounds.size * 0.9f;

        Collider2D hit = Physics2D.OverlapBox(nextCenter, size, 0f, obstacleMask);
        return hit == null;
    }

    void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying)
        {
            return;
        }

        Gizmos.matrix = Matrix4x4.identity;
        Gizmos.DrawWireCube(targetWorld, (Vector3)(collider2d != null ? collider2d.bounds.size * 0.9f : Vector3.one));
    }

    public void SetisClear(bool flag)
    {
        isClear = flag;
    }

    public void SetCanWarp(bool flag)
    {
        canWarp = flag;
    }

    public void SetTargetTransfrom(Transform target)
    {
        TargetTransform = target;
    }
}
