using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class created with the goal of ease the 
 * input management
 */
public class InputController : Input
{
    /* Tells if the game is being executed on a mobile */
    public static bool isOnMobile;
    /* Gives the name for each PS4 button on PC */
    private static Dictionary<string, string> pcButtons = 
        new Dictionary<string, string>() {
            {"square", "0" },
            {"x", "1" },
            {"circle", "2" },
            {"triangle", "3" },
            {"L1", "4" },
            {"R1", "5" },
            {"L2", "6" },
            {"R2", "7" },
            {"share", "8" },
            {"options", "9" },
            {"L3", "10" },
            {"R3", "11" },
            {"PS", "12" },
            {"PAD", "13" },
        };
    /* Gives the name for each PS4 button on mobile */
    private static Dictionary<string, string> mobileButtons =
        new Dictionary<string, string>() {
            {"square", "0" },
            {"x", "1" },
            {"circle", "13" },
            {"triangle", "2" },
            {"L1", "3" },
            {"R1", "14" },
            {"L2", "4" },
            {"R2", "5" },
            {"share", "6" },
            {"options", "7" },
            {"L3", "11" },
            {"R3", "10" },
            {"PS", "12" },
            {"PAD", "8" },
        };


    /*
     * Returns the requested PS4 button name
     * @param button Button
     * @return Button name
     */
    public static string GetPS4ButtonName(string button) {
        return "Joystick Button " + ((isOnMobile) ? mobileButtons[button] : pcButtons[button]);
    }
}
