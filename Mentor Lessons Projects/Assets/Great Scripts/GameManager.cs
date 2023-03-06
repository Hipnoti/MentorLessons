using System;
using System.Collections;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Mentor
{
    public class GameManager : MonoBehaviour
    {
        private const float ASYNC_OPERTAION_THRESHOLD = 0.9f;
        
        public static GameManager Instance { get; private set; }
        
        public GateController gateControllerPrefab;
        public GenericTrigger gateTrigger;

        private UnityEvent<int, int> onSomethingHappaned = new UnityEvent<int, int>();
        [SerializeField] private Enemy[] enemies;
        [SerializeField] private Enemy _dummyEnemy;
        
        [Header("Loading")]
        [SerializeField] int levelIndexToLoad = 0;
        [SerializeField] private GameObject loadingPanel;
        [SerializeField] private TMP_Text pressAnyKeyHintText;
        [SerializeField] private Image loadingBarImage;
        [SerializeField] bool canLoadNextLevel = false;

        private Coroutine nextLevelLoadingCoroutine;
        private AsyncOperation loadingAsyncOperation;

        private Scene loadedAdditiveScene;
        private GateController gateControllerComp;
        
        public void LoadNextLevel()
        {
          //   SceneManager.LoadScene(levelIndexToLoad);
        //    nextLevelLoadingCoroutine = StartCoroutine(LoadSceneDelay());
        // loadingAsyncOperation = SceneManager.LoadSceneAsync(levelIndexToLoad);
        //
        // loadingPanel.SetActive(true);
        // loadingBarImage.fillAmount = 0;
        // loadingAsyncOperation.allowSceneActivation = false;
        SceneManager.sceneLoaded += SceneManagerOnsceneLoaded;
        loadingAsyncOperation = SceneManager.LoadSceneAsync(levelIndexToLoad, LoadSceneMode.Additive);
onSomethingHappaned.AddListener(SomethingCool);
        }

        private void SomethingCool(int arg0, int arg1)
        {
            // Enemy ourEnemy = GetComponent<Enemy>();
            //
            // ourEnemy.Damage();


        }

        public void ReloadCurrentScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void EnemyWasDamaged(Enemy damagedEnemy)
        {
            
        }

        public void DummmyMethod(DummyEnemy dummyEnemy)
        {

        }
        
        private void SceneManagerOnsceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            loadedAdditiveScene = arg0;
            gateControllerComp.OpenGate();
        }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {

            gateControllerComp = null;

            if (gateControllerPrefab != null)
            {
                gateControllerComp = Instantiate(gateControllerPrefab);
            }


            if (gateTrigger != null)
            {
                if (gateControllerComp != null)
                {
               //     gateTrigger.onTriggerEnterEvent.AddListener(gateControllerComp.OpenGate);
                    gateTrigger.onTriggerEnterEvent.AddListener(LoadNextLevel);
                    gateTrigger.onTriggerEnterEvent.AddListener(() =>
                    {
                        gateTrigger.onTriggerEnterEvent.RemoveAllListeners();
                    });
                  

                }
            }
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
               StopCoroutine(nextLevelLoadingCoroutine);
                Debug.Log("<color=#FF0000>YOU DIED :(</color>");
            }
            
            if (loadingAsyncOperation != null)
            {
                loadingBarImage.fillAmount = loadingAsyncOperation.progress;
                // if (loadingAsyncOperation.progress >= ASYNC_OPERTAION_THRESHOLD)
                // {
                //     pressAnyKeyHintText.gameObject.SetActive(true);
                //     // if(Input.anyKeyDown)
                //     //   loadingAsyncOperation.allowSceneActivation = true;
                // }

            }
        }
        

        private IEnumerator LoadSceneDelay()
        {
            WaitForSeconds waitForSeconds = new WaitForSeconds(1);
            Debug.Log("Co-Routine Started");
            yield return waitForSeconds;
            Debug.Log("Frame 1");
            yield return waitForSeconds;
            Debug.Log("Frame 2");
            yield return waitForSeconds;
            Debug.Log("Frame 3");
            yield return waitForSeconds;
            Debug.Log("Frame 4");
            SceneManager.LoadScene(levelIndexToLoad);
        }

        [ContextMenu("Spawn Test")]
        private void SpawnTest()
        {
            //Instantiate(gameObject);
            // CharacterStats stats = new CharacterStats { HP = 200, armor = 10, stamina = 100 };
            // foreach (Enemy dummyEnemy in dummiesForTest)
            // {
            //     dummyEnemy.stats = stats;
            // }
        }

        [ContextMenu("Remvoe Scene")]
        private void RemoveScene()
        {
            SceneManager.UnloadSceneAsync(loadedAdditiveScene);
        }

        [ContextMenu("Damage all enemies")]
        private void DamageAllEnemies()
        {
            foreach (Enemy enemy in enemies)
            {

                try
                {
                    enemy.Damage(10);
                }
                catch (NullReferenceException e)
                {
                    Debug.LogError("<color=#FF0000>Expection occoured " + Environment.NewLine +
                                   e.Message + "</color>");
                }
                catch (Exception genericExecpt)
                {
                    Debug.LogError("<color=#FF0000>Expection occoured " + Environment.NewLine +
                                   genericExecpt.Message + "</color>");
                }
                
            
                
            }

            Debug.Log("Damage all enemies done running");
        }
        
        
    }
}
