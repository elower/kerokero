using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Frog
{
    bool owned;
    string type;
    string specie;
    int coins;
    int time;
    int rarity;
    int mates;
    int requiredMates;
    string description;

    public Frog(string frogType) {
        owned = false;
        mates = 0;
        // RARITY : scale of 0 to 5
        switch(frogType){
            case "Pond":
                type = "Pond Frog";
                specie = "Basic";
                coins = 1;
                time = 1;
                rarity = 1;
                requiredMates = 1;
                description = "This frog has been with you since the beginning! They aren't picky, and will come visit you no matter what furniture you place in your garden.";
                break;
            case "Wood":
                type = "Wood Frog";
                specie = "Basic";
                coins = 3;
                time = 3;
                rarity = 1;
                requiredMates = 2;
                description = "Wood frogs love their family! They like to hang out no matter the weather, and particularly enjoy water and diamonds!";
                break;
            case "Bullfrog":
                type = "Bullfrog";
                specie = "Basic";
                coins = 10;
                time = 10;
                rarity = 1;
                requiredMates = 3;
                description = "Bullfrogs love big, open bodies of water to soak their fat bellies in! They love to relax on benches, but they also love to exercise, and can jump distances up to 10x their body length!";
                break;
            case "Tomato":
                type = "Tomato Frog";
                specie = "Basic";
                coins = 15;
                time = 15;
                rarity = 1;
                requiredMates = 3;
                description = "Tomato frogs love Summer! They love to relax in the mud when it's humid, and they enjoy the water.";
                break;
            case "Flying":
                type = "Flying Frog";
                specie = "Basic";
                coins = 60;
                time = 12;
                rarity = 2;
                requiredMates = 5;
                description = "Flying Frogs love to glide from tree to tree. They also love to swing around on Tree Swings!";
                break;
            case "Amazon_Milk":
                type = "Amazon Milk Frog";
                specie = "Basic";
                coins = 100;
                time = 20;
                rarity = 2;
                requiredMates = 5;
                description = "Amazon Milk frogs love to hang out in trees and enjoy a cup of warm milk on a bench! They also love to swim!";
                break;
            case "Darwins":
                type = "Darwin's Frog";
                specie = "Basic";
                coins = 480;
                time = 96;
                rarity = 2;
                requiredMates = 5;
                description = "Darwin's Frogs are masters of disguise. They're most comfortable around leaves, but once they get to know you, they love to relax on a bench.";
                break;
            case "Glass":
                type = "Glass Frog";
                specie = "Basic";
                coins = 1920;
                time = 96;
                rarity = 3;
                requiredMates = 7;
                description = "Glass Frogs love to play in the water! They also love to sit on lily pads on humid days.";
                break;
            case "Atelopus":
                type = "Atelopus Toad";
                specie = "Basic";
                coins = 19200;
                time = 384;
                rarity = 4;
                requiredMates = 7;
                description = "Atelopus are actually a species of toad, and don't you forget it! They love everything - from swimming, to climbing trees, to gazing at a diamond on a humid day.";
                break;
            case "Black_Rain":
                type = "Black Rain Frog";
                specie = "Basic";
                coins = 19200;
                time = 384;
                rarity = 4;
                requiredMates = 7;
                description = "The Black Rain frog doesn't need water to be happy, though they love a rain shower! Despite their grumpy appearance, they mainly like hanging out with friends on a walk or a bench.";
                break;
            case "Brown":
                type = "Brown Tree Frog";
                specie = "Tree";
                coins = 3;
                time = 3;
                rarity = 1;
                requiredMates = 3;
                description = "These frogs love to hang out on any foliage you can imagine! They're scared of the dark, and may hide away if you don't have a night light.";
                break;
            case "Whites":
                type = "White's Tree Frog";
                specie = "Tree";
                coins = 10;
                time = 10;
                rarity = 1;
                requiredMates = 3;
                description = "The White's Tree Frog is always sleepy! They love hanging out around the foliage and are particularly drawn to dragonflies.";
                break;
            case "Red_Eyed":
                type = "Red-Eyed Tree Frog";
                specie = "Tree";
                coins = 225;
                time = 45;
                rarity = 2;
                requiredMates = 5;
                description = "The Red-Eyed Tree Frog is quite particular about the type of foliage they spend their time in - it must only be the best of the best!";
                break;
            case "Gray":
                type = "Gray Tree Frog";
                specie = "Tree";
                coins = 1800;
                time = 90;
                rarity = 3;
                requiredMates = 7;
                description = "Since the Gray Tree Frog is nocturnal, they love to hide in holes during the day. Being a tree frog, this species loves to hang out in foliage!";
                break;
            case "White_Lipped":
                type = "White-Lipped Tree Frog";
                specie = "Tree";
                coins = 9000;
                time = 180;
                rarity = 4;
                requiredMates = 10;
                description = "The White-Lipped Tree Frog loves any and all types of foliage. They love their privacy, and think dragonflies are really neat!";
                break;
            case "Yellow_Headed":
                type = "Yellow-Headed Dart Frog";
                specie = "Dart";
                coins = 9000;
                time = 180;
                rarity = 4;
                requiredMates = 10;
                description = "Yellow-Headed Dart Frogs love bright things that remind them of their own bright skin! They need their home to be humid and misty.";
                break;
            case "Golden_Poison":
                type = "Golden Poison Dart Frog";
                specie = "Dart";
                coins = 9000;
                time = 180;
                rarity = 4;
                requiredMates = 10;
                description = "";
                break;
            case "Blue_Jeans_Poison":
                type = "Blue Jeans Poison Dart Frog";
                specie = "Dart";
                coins = 9000;
                time = 180;
                rarity = 4;
                requiredMates = 10;
                description = "";
                break;
            case "Blue_Poison":
                type = "Blue Poison Dart Frog";
                specie = "Dart";
                coins = 3000;
                time = 300;
                rarity = 5;
                requiredMates = 15;
                description = "";
                break;
            case "Strawberry_Poison":
                type = "Strawberry Poison Dart Frog";
                specie = "Dart";
                coins = 3000;
                time = 300;
                rarity = 5;
                requiredMates = 15;
                description = "";
                break;
            default:
                type = "UNDEFINED";
                specie = "UNDEFINED";
                coins = 0;
                time = 0;
                rarity = 0;
                description = "";
                break;
        }
    }
    // gets
    public bool getOwned() { return owned; }
    public int getCoins() { return coins; }
    public int getTime() { return time; }
    public int getMates() { return mates; }
    public int getRarity() { return rarity; }
    public string getSpecie() { return specie; }
    public string getDescription() { return description; }
    public string getName() { return type; }
    public int getRequiredMates() { return requiredMates; }

    // sets
    public void setOwned() { owned = true; }
    public void setTime(int setTime) { time = setTime; }
    public void setMates(int setMates) { mates = setMates; }

    public float getAnimationSpeed() {
        // 1/(TIME/ANIM_LENGTH) = MULTIPLIER
        return (1.0f/(time/0.5f));
    }
}
