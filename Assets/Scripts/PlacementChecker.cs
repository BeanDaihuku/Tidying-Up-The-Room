//2323012_endou_kusei
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

    private ColorSwitcher colorSwitcher; // �F�ύX����X�N���v�g�Q��

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

                    // �F�ω�����
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
            effect.transform.SetParent(obj.transform); // �Ǐ]
        }
    }

    void ResetEffect(GameObject obj)
    {
        // �p�[�e�B�N���≹�̃��Z�b�g�i�K�v�ɉ����Ēǉ��j
    }
}
