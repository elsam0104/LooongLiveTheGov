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
        //보이게 하고 캔버스를 패런트로 잡는다.
        energyText.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
        //카메라를 메인으로 잡은 후에 좌표 변환 , 마우스 포인터 위치 잡을 때 사용
        energyText.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0f);
        //z포지션 초기화

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