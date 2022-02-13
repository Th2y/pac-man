using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Pincel de Tilemap que permite usar Prefabs como Tiles
/// </summary>
#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Brush.../Prefab Brush", fileName = "Prefab Brush")]
[CustomGridBrush(false, true, false, "Prefab Brush")]
public class PincelTilemap : GridBrushBase
{
    [SerializeField]
    private GameObject prefab;

    public override void Paint(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
    {
        if (brushTarget.layer == 31)
            return;

        //Instanciar o prefab e colocar ele dentro da camada selecionada do tilempap

        GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);

        //Possibilita desfazer uma alteração
        Undo.RegisterCreatedObjectUndo((Object)instance, "Paint Prefabs");

        instance.transform.SetParent(brushTarget.transform);
        //Posição no centro da célula
        instance.transform.position = gridLayout.CellToLocalInterpolated(position + new Vector3(0.5f, 0.5f, 0.5f));
    }

    public override void Erase(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
    {
        if (brushTarget.layer == 31)
            return;

        Vector2 valueMin = gridLayout.CellToLocalInterpolated(position + new Vector3(0.5f, 0.5f, 0.5f));
        Vector2 valueMax = gridLayout.CellToLocalInterpolated(position + Vector3Int.one);

        Bounds cellBouns = new Bounds((valueMin + valueMax) * 0.5f, valueMax - valueMin);

        int childTarget = brushTarget.transform.childCount;
        for (int i = 0; i < childTarget; i++)
        {
            Transform child = brushTarget.transform.GetChild(i);
            if (cellBouns.Contains(child.position))
            {
                Undo.DestroyObjectImmediate(child.gameObject);
                return;
            }
        }
    }
}
#endif