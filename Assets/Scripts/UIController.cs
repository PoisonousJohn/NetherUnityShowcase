using UnityEngine;
using NetherSDK;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIController : MonoBehaviour {

    public InputField url;
    public InputField registrationLogin;
    public Text debugInfo;
    public Dropdown login;

    private void Awake()
    {
        _client = gameObject.AddComponent<NetherClient>();
    }

    private void Start()
    {
    }

    private void UpdateServerUrl()
    {
        _client.NetherDeploymentUrl = url.text;
    }

    public void Register()
    {
        UpdateServerUrl();
        _client.Register(registrationLogin.text, resp =>
        {
            if (resp.Status != NetherSDK.Models.CallBackResult.Success )
            {
                Debug.LogError("Registration Error: " + resp.Exception ?? "" + 
                        (resp.NetherError != null ? resp.NetherError.error.message : ""));
                return;
            }
            Debug.LogError("Registration Response: " + resp.Result.providerData);
            _passwords.Add(resp.Result.providerId, resp.Result.providerData);
            login.AddOptions(new List<string> { resp.Result.providerId });
            login.RefreshShownValue();
        });
    }

    public void SignIn()
    {
        UpdateServerUrl();
        string loginString = login.options[login.value].text;
        _client.GetNetherAccessToken(loginString, _passwords[loginString], token => Debug.LogError("Token received: " + token ?? ""));
        debugInfo.text = "Signed in as: " + loginString;
    }


    private NetherClient _client;
    private readonly Dictionary<string, string> _passwords = new Dictionary<string, string>();
}
