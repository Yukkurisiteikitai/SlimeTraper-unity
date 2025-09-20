using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

[RequireComponent(typeof(RawImage))]
public class MarkerDetector : MonoBehaviour
{
    public enum State { Waiting, Checking, Success, Error }

    [Header("ゲーム設定")]
    [SerializeField] private string gameCode = "maze";

    [Header("API設定")]
    [SerializeField] private string apiKey = "TESTKEY";
    [SerializeField] private string baseApiUrl = "http://localhost:8000";
    [SerializeField] private string markerPath = "/api/get-user-marker";
    [SerializeField] private string rewardPath = "/api/add-rewards";
    
    private string MarkerApiUrl => $"{baseApiUrl.TrimEnd('/')}/{markerPath.TrimStart('/')}";
    private string RewardApiUrl => $"{baseApiUrl.TrimEnd('/')}/{rewardPath.TrimStart('/')}";

    [Header("UI")]
    [SerializeField] private RawImage preview;
    [SerializeField] private Text topText;
    [SerializeField] private Text bottomText;
    [SerializeField] private AspectRatioFitter aspectFitter;
    [SerializeField] private GameObject successImage;

    [Header("切り抜き設定")]
    [Range(0.1f, 1f)]
    [SerializeField] private float squareSizeRatio = 0.5f;

    [Header("拡大設定")]
    [SerializeField] private float scaleDuration = 0.5f;
    [SerializeField] private Vector3 targetScale = Vector3.one;
    [SerializeField] private Vector3 initialScale = Vector3.zero;

    private WebCamTexture webCamTexture;
    private bool detected;
    private State currentState = State.Waiting;
    private int detectedId;

    [SerializeField] private List<string> responses = new List<string>();

    void Start()
    {
        Init();
    }

    void Init()
    {
        detected = false;
        detectedId = -1;
        successImage.transform.localScale = initialScale;
        successImage.SetActive(false);

        webCamTexture = new WebCamTexture();
        webCamTexture.Play();
        preview.texture = webCamTexture;

        SetState(State.Waiting);
        StartCoroutine(UploadLoop());
    }

    void Update()
    {
        UpdateAspectRatio();

        if (currentState == State.Error && Input.GetKeyDown(KeyCode.Space))
        {
            Init();
        }
    }

    private void UpdateAspectRatio()
    {
        if (webCamTexture.width > 16 && aspectFitter != null)
        {
            float ratio = (float)webCamTexture.width / webCamTexture.height;
            aspectFitter.aspectRatio = ratio;
            preview.uvRect = new Rect(0, 0, 1, 1);
            if (webCamTexture.videoVerticallyMirrored)
                preview.uvRect = new Rect(0, 1, 1, -1);
        }
    }

    private void SetState(State newState)
    {
        currentState = newState;
        switch (currentState)
        {
            case State.Waiting:
                SetUI("GAME CLEAR", "マーカーを枠に写してください");
                break;
            case State.Success:
                SetUI("ポイント受け取りに成功しました", "");
                ShowSuccessImage();
                break;
            case State.Error:
                SetUI("エラーが発生しました", "スペースキーで再スタート");
                break;
        }
    }

    private void SetUI(string top, string bottom)
    {
        if (top != null) topText.text = top;
        if (bottom != null) bottomText.text = bottom;
    }

    public void ShowSuccessImage()
    {
        successImage.SetActive(true);
        successImage.transform.localScale = initialScale;
        successImage.transform.DOScale(targetScale, scaleDuration).SetEase(Ease.OutBack);
    }

    IEnumerator UploadLoop()
    {
        while (!detected)
        {
            yield return new WaitForSeconds(1f);
            if (webCamTexture.width <= 16 || webCamTexture.height <= 16) continue;

            int baseSize = Mathf.Min(webCamTexture.width, webCamTexture.height);
            int size = Mathf.RoundToInt(baseSize * squareSizeRatio);
            int x = (webCamTexture.width - size) / 2;
            int y = (webCamTexture.height - size) / 2;

            Color[] pixels = webCamTexture.GetPixels(x, y, size, size);
            Texture2D snap = new Texture2D(size, size, TextureFormat.RGB24, false);
            snap.SetPixels(pixels);
            snap.Apply();

            yield return StartCoroutine(UploadMarker(snap));
        }
    }

    IEnumerator UploadMarker(Texture2D tex)
    {
        byte[] imageData = tex.EncodeToPNG();
        WWWForm form = new WWWForm();
        form.AddBinaryData("imagefile", imageData, "capture.png", "image/png");

        using (UnityWebRequest www = UnityWebRequest.Post(MarkerApiUrl, form))
        {
            www.SetRequestHeader("Authorization", apiKey);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Marker API Error: " + www.error);
                SetState(State.Error);
            }
            else
            {
                string responseText = www.downloadHandler.text;
                MarkerResponse marker = JsonUtility.FromJson<MarkerResponse>(responseText);

                if (marker != null && marker.id != null && marker.id.Length > 0)
                {
                    detectedId = marker.id[0];
                    responses.Add(responseText);
                    SetState(State.Checking);
                    detected = true;
                    webCamTexture.Stop();

                    StartCoroutine(SendReward(detectedId.ToString(), gameCode, Random.Range(500, 1500), Random.Range(60f, 180f)));
                }
                else
                {
                    SetState(State.Waiting);
                }
            }
        }
    }

    IEnumerator SendReward(string userId, string gameCode, int score, float clearTime)
    {
        var payload = new RewardRequest
        {
            user_id = userId,
            game_code = gameCode,
            score = score,
            clear_time = clearTime
        };

        string json = JsonUtility.ToJson(payload);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);

        using (UnityWebRequest www = new UnityWebRequest(RewardApiUrl, "POST"))
        {
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Authorization", apiKey);

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Reward API Error: " + www.error);
                SetState(State.Error);
            }
            else
            {
                string rewardResponse = www.downloadHandler.text;
                Debug.Log("Reward Response: " + rewardResponse);
                RewardResult result = JsonUtility.FromJson<RewardResult>(rewardResponse);

                if (result != null && result.status == "success")
                {
                    SetUI($"GAME CLEAR", $"{userId} に {result.reward_added} ポイント付与完了！");
                    SetState(State.Success);
                }
                else
                {
                    SetState(State.Error);
                }
            }
        }
    }

    [System.Serializable]
    private class MarkerResponse { public int[] id; }

    [System.Serializable]
    public class RewardRequest
    {
        public string user_id;
        public string game_code;
        public int score;
        public float clear_time;
    }

    [System.Serializable]
    public class RewardResult
    {
        public string status;
        public int reward_added;
        public int new_balance;
    }

    public List<string> GetResponses() => responses;
}