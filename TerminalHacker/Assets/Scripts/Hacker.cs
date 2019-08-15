using UnityEngine;

public class Hacker : MonoBehaviour {

    int level;
    enum Screen {MainMenu, Password, Win};
    Screen currentScreen;

    // Start is called before the first frame update
    void Start() {
        ShowMainMenu();
    }

    void ShowMainMenu() {
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();

        Terminal.WriteLine("Hva ønsker du å hacke? \n 1. Biblioteket i P35 \n 2. Kontoret til Sigrun \n 3. Oraklenes topp-hemmelige data ");
        Terminal.WriteLine("Skriv inn ditt valg (1, 2 eller 3): ");
    }

    void OnUserInput(string input) {
        if (input.ToLower() == "meny") {
            ShowMainMenu();
        } else if (input == "1") {
            level = 1;
            startGame();
        } else if (input == "2") {
            level = 2;
            startGame();
        } else if (input.ToLower() == "hello there") {
            Terminal.WriteLine("GENERAL KENOBI");
        } else {
            Terminal.WriteLine("Vennligst oppgi et gyldig hackermål");
        }
     }

    void startGame() {
        currentScreen = Screen.Password;
        Terminal.WriteLine("Du har valgt alternativ " + level);
    }
}
