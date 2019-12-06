using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour {
    public SceneAsset scene;
    public Button button;
    
    private void OnValidate()  => this.button = this.GetComponent<Button>();

    private void Awake() => this.button.onClick.AddListener(() => SceneManager.LoadScene(this.scene.name));
}
