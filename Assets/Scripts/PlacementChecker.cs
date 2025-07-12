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

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // 自動でAudioSource追加
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

                    // 正解演出
                    PlayCorrectEffect(currentObject);
                }
            }
            else if (isCorrect)
            {
                // 正解状態を解除
                isCorrect = false;
                gameManager?.ReportCancel();
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
            ResetEffect(other.gameObject);
        }
    }

    // 正解時の演出（色変更＋音）
    void PlayCorrectEffect(GameObject obj)
    {
        var rend = obj.GetComponent<Renderer>();
        if (rend != null)
        {
            rend.material.color = Color.green; // 色変更（仮）
        }

        if (correctSE != null && audioSource != null)
        {
            audioSource.PlayOneShot(correctSE);
        }
    }

    // リセット時の演出
    void ResetEffect(GameObject obj)
    {
        var rend = obj.GetComponent<Renderer>();
        if (rend != null)
        {
            rend.material.color = Color.white; // 元の色に戻す（仮）
        }

            // 音を鳴らす
            if (correctSE != null && audioSource != null)
            {
                audioSource.PlayOneShot(correctSE);
            }

            // パーティクルを出す
            if (effectPrefab != null)
            {
                GameObject effect = Instantiate(effectPrefab, obj.transform.position + Vector3.up * 0.2f, Quaternion.identity);
                effect.transform.SetParent(obj.transform); // 追従させる！

        }

    }



}
