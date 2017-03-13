using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;
using UnityEngine.SceneManagement;


public class BallManager : MonoBehaviour
{
    // KeywordRecognizer object.
    KeywordRecognizer keywordRecognizer;

    public bool ballStopped;

    public GameObject ball;
    public GameObject cube;

    // Defines which function to call when a keyword is recognized.
    delegate void KeywordAction(PhraseRecognizedEventArgs args);
    Dictionary<string, KeywordAction> keywordCollection;
    // Use this for initialization
    void Start()
    {

        ballStopped = false;
        ball.SetActive(false);
        cube.SetActive(false);

        keywordCollection = new Dictionary<string, KeywordAction>();

        // Add keyword to start manipulation.
        keywordCollection.Add("Stop Ball", StopBallCommand);

        /* TODO: DEVELOPER CODING EXERCISE 5.a */

        // 5.a: Add keyword Expand Model to call the ExpandModelCommand function.
        keywordCollection.Add("Play Ball", PlayBallCommand);

        // 5.a: Add keyword Expand Model to call the ExpandModelCommand function.
        keywordCollection.Add("Reset Game", ResetGame);

        // 5.a: Add keyword Expand Model to call the ExpandModelCommand function.
        keywordCollection.Add("Apply Force", ApplyForce);

        keywordCollection.Add("Place Ball", PlaceBall);
        keywordCollection.Add("Bounce Ball", BounceBall);

        // Initialize KeywordRecognizer with the previously added keywords.
        keywordRecognizer = new KeywordRecognizer(keywordCollection.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    void OnDestroy()
    {
        keywordRecognizer.Dispose();
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        KeywordAction keywordAction;

        if (keywordCollection.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke(args);
        }
    }

    private void StopBallCommand(PhraseRecognizedEventArgs args)
    {
        Debug.Log("stop called");
        StopBall();
    }

    public void StopBall()
    {
        Time.timeScale = 0;
        ballStopped = true;
    }

    private void PlayBallCommand(PhraseRecognizedEventArgs args)
    {
        Debug.Log("play called");
        PlayBall();
    }

    public void PlayBall()
    {
        Time.timeScale = 1;
        ballStopped = false;
    }

    private void ResetGame(PhraseRecognizedEventArgs args)
    {
        Debug.Log("reset game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void PlaceBall(PhraseRecognizedEventArgs args)
    {
        Debug.Log("place ball");
        ball.SetActive(true);
        cube.SetActive(true);
    }

    void Update()
    {

    }

    void ApplyForce(PhraseRecognizedEventArgs args)
    {
        ball.GetComponent<Rigidbody>().AddForce(transform.forward * 5);
    }

    void BounceBall(PhraseRecognizedEventArgs args)
    {
        Debug.Log("bounce ball");
        ball.GetComponent<Rigidbody>().AddForce(transform.up * 50);
    }
}
