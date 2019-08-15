using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        ShowMainMenu();
    }

    void ShowMainMenu() {
        var username = "sup3rh4ckerxX1337Xx";
        var greeting = "God dag " + username;
        Terminal.ClearScreen();

        Terminal.WriteLine(greeting);
        Terminal.WriteLine("Hva ønsker du å hacke? \n 1. Biblioteket i P35 \n 2. Kontoret til Sigrun \n 3. Oraklenes super topp-hemmelige data ");
        Terminal.WriteLine("Skriv inn ditt valg: ");
    }

    // Update is called once per frame
    void Update() {
        
    }
}
