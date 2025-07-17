//2323012_endou_kusei
using UnityEngine;

public class ColorSwitcher : MonoBehaviour
{
    public Material originalMaterial; // 元の色
    public Material whiteMaterial;    // 白マテリアル

    private MeshRenderer meshRenderer;
    private bool isCorrect = false;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        // ゲーム開始時に白くする
        if (whiteMaterial != null)
            meshRenderer.material = whiteMaterial;
    }

    // 正しい場所に置かれた時に呼ぶ
    public void SetCorrect()
    {
        if (isCorrect) return;

        isCorrect = true;

        if (originalMaterial != null)
            meshRenderer.material = originalMaterial;
    }

    // 正解から外れた時に呼ぶ
    public void ResetToWhite()
    {
        if (!isCorrect && whiteMaterial != null)
            meshRenderer.material = whiteMaterial;
    }
}
