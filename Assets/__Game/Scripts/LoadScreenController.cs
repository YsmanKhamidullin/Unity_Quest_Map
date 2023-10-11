using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScreenController : MonoBehaviour
{
    [SerializeField] private Image loadImage;
    [SerializeField] private float fakeLoadTime = 1f;
    private float _fakeLoadTimeStamp;

    private void Start()
    {
        loadImage.fillAmount = 0;
    }

    private void Update()
    {
        _fakeLoadTimeStamp += Time.deltaTime;
        loadImage.fillAmount = Mathf.Lerp(0, 1, _fakeLoadTimeStamp / fakeLoadTime);
        if (_fakeLoadTimeStamp >= fakeLoadTime)
        {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}