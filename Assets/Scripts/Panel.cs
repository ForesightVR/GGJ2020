using OuterRimStudios.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

public class Panel : MonoBehaviour
{
    private int NUM_SOCKETS = 6;
    private int NUM_VACUUM_TUBES = 20;
    
    public GameObject sockets;
    public GameObject vacuumTubes;
    public GameObject vacuumTubePrefab;
    public string possibleCharacters;
    
    void Start()
    {
        var characters = possibleCharacters.ToCharArray().GetRandomItems(NUM_VACUUM_TUBES);
        var socketChars = characters.GetRandomItems(NUM_SOCKETS);

        foreach (var character in characters)
        {
            GenerateVacuumTube(character);
        }

        for (var i = 0; i < NUM_SOCKETS; i++)
        {
            sockets.transform.GetChild(i).GetComponent<Socket>().Initialize(socketChars[i]);
        }
    }

    private void GenerateVacuumTube(char character)
    {
        var position = transform.position;
        var x = position.x + Random.value * 2 - 1;
        var y = position.y + Random.value + 1;
        var z = position.z + Random.value;

        var newTube = Instantiate(vacuumTubePrefab, new Vector3(x, y, z), Quaternion.identity);
        newTube.transform.SetParent(vacuumTubes.transform);

        newTube.GetComponent<VacuumTube>().Initialize(character);
    }
}
