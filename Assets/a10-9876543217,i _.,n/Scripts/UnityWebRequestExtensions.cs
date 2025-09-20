using UnityEngine.Networking;
using System.Threading.Tasks;

public static class UnityWebRequestExtensions
{
    public static Task<UnityWebRequest.Result> SendWebRequestAsync(this UnityWebRequest www)
    {
        var tcs = new TaskCompletionSource<UnityWebRequest.Result>();
        www.SendWebRequest().completed += _ => tcs.SetResult(www.result);
        return tcs.Task;
    }
}
