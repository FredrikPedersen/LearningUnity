using System;
using UnityEngine;

public class Hacker : MonoBehaviour {

    int level;
    String message = "";
    enum Screen {MainMenu, Password, Win};
    Screen currentScreen;

    // Start is called before the first frame update
    void Start() {
        ShowMainMenu();
    }

    void ShowMainMenu() {
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();

        if (message != "") {
            Terminal.WriteLine(message);
        }

        Terminal.WriteLine("Hva ønsker du å hacke? \n 1. Biblioteket i P35 \n 2. Kontoret til Sigrun \n 3. Oraklenes topp-hemmelige data ");
        Terminal.WriteLine("\nSkriv inn ditt valg (1, 2 eller 3): ");
    }

    void OnUserInput(string input) {
        if (input.ToLower() == "meny") { //We can always go directly to MainMenu
            ShowMainMenu();
        } else if (currentScreen == Screen.MainMenu) {
            RunMainMenu(input);
        } else if (currentScreen == Screen.Password) {
            RunPassword();
        }
    }

    private void RunPassword() {
        message = "FEIL PASSORD!";
        ShowMainMenu();
    }

    void RunMainMenu(string input) {
        if (input == "1") {
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
        Terminal.WriteLine("Vennligst oppgi ditt passord: ");
    }
}
