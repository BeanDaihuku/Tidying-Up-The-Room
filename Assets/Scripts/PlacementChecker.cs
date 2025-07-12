using UnityEngine;

public class PlacementChecker : MonoBehaviour
{
    [Header("�ݒ肷��I�u�W�F�N�g")]
    public GameObject correctObject; // �u���Ăق����I�u�W�F�N�g
    public GameManager gameManager;  // ���𐔊Ǘ�

    [Header("���o�p")]
    public AudioClip correctSE;      // �������̉�
    public float positionTolerance = 0.15f; // �����Ƃ݂Ȃ�����
    public float checkInterval = 0.5f;      // ����Ԋu�i�b�j

    [Header("�p�[�e�B�N�����o")]
    public GameObject effectPrefab;

    private AudioSource audioSource;
    private bool isCorrect = false;
    private float timer = 0f;

    private GameObject currentObject; // �g���K�[���̃I�u�W�F�N�g�ۑ�

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // ������AudioSource�ǉ�
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

                    // �������o
                    PlayCorrectEffect(currentObject);
                }
            }
            else if (isCorrect)
            {
                // ������Ԃ�����
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

    // �������̉��o�i�F�ύX�{���j
    void PlayCorrectEffect(GameObject obj)
    {
        var rend = obj.GetComponent<Renderer>();
        if (rend != null)
        {
            rend.material.color = Color.green; // �F�ύX�i���j
        }

        if (correctSE != null && audioSource != null)
        {
            audioSource.PlayOneShot(correctSE);
        }
    }

    // ���Z�b�g���̉��o
    void ResetEffect(GameObject obj)
    {
        var rend = obj.GetComponent<Renderer>();
        if (rend != null)
        {
            rend.material.color = Color.white; // ���̐F�ɖ߂��i���j
        }

            // ����炷
            if (correctSE != null && audioSource != null)
            {
                audioSource.PlayOneShot(correctSE);
            }

            // �p�[�e�B�N�����o��
            if (effectPrefab != null)
            {
                GameObject effect = Instantiate(effectPrefab, obj.transform.position + Vector3.up * 0.2f, Quaternion.identity);
                effect.transform.SetParent(obj.transform); // �Ǐ]������I

        }

    }



}
