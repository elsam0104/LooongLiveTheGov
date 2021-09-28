using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class EarnText : MonoBehaviour
{
    private Text energyText = null;

    public void Show(Vector2 mousePosition)
    {
        energyText = GetComponent<Text>();
        energyText.text = string.Format("+ {0}", GameManager.Instance.UI.showClickPerMeters);

        energyText.gameObject.SetActive(true);
        energyText.transform.SetParent(GameManager.Instance.showCanvas.transform);
        //���̰� �ϰ� ĵ������ �з�Ʈ�� ��´�.
        energyText.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
        //ī�޶� �������� ���� �Ŀ� ��ǥ ��ȯ , ���콺 ������ ��ġ ���� �� ���
        energyText.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0f);
        //z������ �ʱ�ȭ

        RectTransform rectTransform = GetComponent<RectTransform>();
        float targetPositionY = rectTransform.anchoredPosition.y + 100f;

        energyText.DOFade(0f, 0.5f).OnComplete(() => Despawn());
        rectTransform.DOAnchorPosY(targetPositionY, 0.5f);
    }
    private void Despawn()
    {
        energyText.DOFade(1f, 0f);
        transform.SetParent(GameManager.Instance.Pool);
        gameObject.SetActive(false);
    }
}