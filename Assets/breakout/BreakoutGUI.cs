using TMPro;
using UnityEngine;

public class BreakoutGUI : MonoBehaviour
{
	private TextMeshProUGUI countdownText;

	[SerializeField]
	private BreakoutBall ball;
    // Start is called before the first frame update
    void Start()
    {
        countdownText = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        countdownText.enabled = ball.startWait >= 0;
		countdownText.text = ""+Mathf.Ceil(ball.startWait);
    }
}
