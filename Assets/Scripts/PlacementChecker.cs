//2323012_endou_kusei
using UnityEngine;

public class PlacementChecker : MonoBehaviour
{
    [Header("設定するオブジェクト")]
    public GameObject correctObject; // 置いてほしいオブジェクト
    public GameManager gameManager;  // 正解数管理

    [Header("演出用")]
    public AudioClip correctSE;      // 正解時の音
    public float positionTolerance = 0.15f; // 正解とみなす距離
    public float checkInterval = 0.5f;      // 判定間隔（秒）

    [Header("パーティクル演出")]
    public GameObject effectPrefab;

    private AudioSource audioSource;
    private bool isCorrect = false;
    private float timer = 0f;

    private GameObject currentObject; // トリガー中のオブジェクト保存

    private ColorSwitcher colorSwitcher; // 色変更制御スクリプト参照

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject != correctObject) return;

        currentObject = other.gameObject;
        timer += Time.deltaTime;

        if (timer >= checkInterval)
        {
            float dist = Vector3.Distance(other.transform.position, transform.position);
            if (dist <= positionTolerance)
            {
                if (!isCorrect)
                {
                    isCorrect = true;
                    gameManager?.ReportCorrect();

                    // 色変化処理
                    colorSwitcher = currentObject.GetComponent<ColorSwitcher>();
                    if (colorSwitcher != null) colorSwitcher.SetCorrect();

                    PlayCorrectEffect(currentObject);
                }
            }
            else if (isCorrect)
            {
                isCorrect = false;
                gameManager?.ReportCancel();

                colorSwitcher = currentObject.GetComponent<ColorSwitcher>();
                if (colorSwitcher != null) colorSwitcher.ResetToWhite();

                ResetEffect(currentObject);
            }

            timer = 0f;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == correctObject && isCorrect)
        {
            isCorrect = false;
            gameManager?.ReportCancel();

            colorSwitcher = other.GetComponent<ColorSwitcher>();
            if (colorSwitcher != null) colorSwitcher.ResetToWhite();

            ResetEffect(other.gameObject);
        }
    }

    void PlayCorrectEffect(GameObject obj)
    {
        if (correctSE != null && audioSource != null)
        {
            audioSource.PlayOneShot(correctSE);
        }

        if (effectPrefab != null)
        {
            GameObject effect = Instantiate(effectPrefab, obj.transform.position + Vector3.up * 0.2f, Quaternion.identity);
            effect.transform.SetParent(obj.transform); // 追従
        }
    }

    void ResetEffect(GameObject obj)
    {
        // パーティクルや音のリセット（必要に応じて追加）
    }
}
