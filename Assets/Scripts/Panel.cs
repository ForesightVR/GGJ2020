using System.Collections;
using OuterRimStudios.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

public class Panel : MonoBehaviour
{
    private int NUM_SOCKETS = 6;
    private int NUM_ROUNDS = 3;

    public GameObject sockets;
    public GameObject vacuumTubes;
    public Animator buttonPanel;
    public Animator socketedVacuumTubesAnimator;

    public WarpButton warpButton;
    public GameObject vacuumTubePrefab;
    public string possibleCharacters;

    private int lockedSocketCount = 0;
    private int roundIndex = 0;

    private Transform[][] rounds;
    private static readonly int WinRound = Animator.StringToHash("WinRound");

    void Start()
    {
        rounds = InitializeRounds();

        StartNewRound();
    }

    private void StartNewRound()
    {
        // Win
        if (roundIndex == NUM_ROUNDS)
        {
            foreach (Transform socket in sockets.transform)
            {
                socket.GetComponent<Socket>().character = ' ';
            }
            buttonPanel.SetTrigger("OpenPanel");
            warpButton.Activate();
            return;
        }

        var roundSockets = rounds[roundIndex];
        
        var characters = possibleCharacters.ToCharArray().GetRandomItems(roundSockets.Length + 2);
        var socketChars = characters.GetRandomItems(roundSockets.Length);

        foreach (var character in characters)
        {
            GenerateVacuumTube(character);
        }

        for (var i = 0; i < roundSockets.Length; i++)
        {
            roundSockets[i].GetComponent<Socket>().character = socketChars[i];
        }
    }

    public void NewLockedSocket()
    {
        // Win round
        if (++lockedSocketCount == rounds[roundIndex].Length)
        {
            socketedVacuumTubesAnimator.SetTrigger(WinRound);

            lockedSocketCount = 0;
            roundIndex++;
            StartCoroutine(DelayedStartNewRound());
        }
    }

    private IEnumerator DelayedStartNewRound()
    {
        yield return new WaitForSeconds(1.1f);
        StartNewRound();
    }

    private void GenerateVacuumTube(char character)
    {
        var position = transform.position;
        var x = position.x + Random.value * 2 - 1;
        var y = position.y - .4f;
        var z = position.z + Random.value + .5f;

        var newTube = Instantiate(vacuumTubePrefab, new Vector3(x, y, z), Random.rotation);
        newTube.transform.SetParent(vacuumTubes.transform);

        newTube.GetComponent<VacuumTube>().Initialize(character);
    }
    
    private Transform[][] InitializeRounds()
    {
        var roundOneSockets = new[]
        {
            sockets.transform.GetChild(2), sockets.transform.GetChild(3)
        };
        
        var roundTwoSockets = new[]
        {
            sockets.transform.GetChild(1), sockets.transform.GetChild(2),
            sockets.transform.GetChild(3), sockets.transform.GetChild(4),
        };

        var roundThreeSockets = new Transform[6];
        for (var i = 0; i < NUM_SOCKETS; i++)
        {
            roundThreeSockets[i] = sockets.transform.GetChild(i);
        }

        return new[] {roundOneSockets, roundTwoSockets, roundThreeSockets};
    }
}