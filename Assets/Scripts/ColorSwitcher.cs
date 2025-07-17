//2323012_endou_kusei
using UnityEngine;

public class ColorSwitcher : MonoBehaviour
{
    public Material originalMaterial; // ���̐F
    public Material whiteMaterial;    // ���}�e���A��

    private MeshRenderer meshRenderer;
    private bool isCorrect = false;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        // �Q�[���J�n���ɔ�������
        if (whiteMaterial != null)
            meshRenderer.material = whiteMaterial;
    }

    // �������ꏊ�ɒu���ꂽ���ɌĂ�
    public void SetCorrect()
    {
        if (isCorrect) return;

        isCorrect = true;

        if (originalMaterial != null)
            meshRenderer.material = originalMaterial;
    }

    // ��������O�ꂽ���ɌĂ�
    public void ResetToWhite()
    {
        if (!isCorrect && whiteMaterial != null)
            meshRenderer.material = whiteMaterial;
    }
}
