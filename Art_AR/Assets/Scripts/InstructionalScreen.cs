/*==============================================================================
Copyright (c) 2012-2013 QUALCOMM Austria Research Center GmbH.
All Rights Reserved.

This  Vuforia(TM) sample application in source code form ("Sample Code") for the
Vuforia Software Development Kit and/or Vuforia Extension for Unity
(collectively, the "Vuforia SDK") may in all cases only be used in conjunction
with use of the Vuforia SDK, and is subject in all respects to all of the terms
and conditions of the Vuforia SDK License Agreement, which may be found at
https://developer.vuforia.com/legal/license.

By retaining or using the Sample Code in any manner, you confirm your agreement
to all the terms and conditions of the Vuforia SDK License Agreement.  If you do
not agree to all the terms and conditions of the Vuforia SDK License Agreement,
then you may not retain or use any of the Sample Code in any manner.
==============================================================================*/

using UnityEngine;

/// <summary>
/// This MonoBehaviour renders an instructional screen that explains the VideoPlayback sample
/// </summary>
public class InstructionalScreen : MonoBehaviour
{
    #region PRIVATE_MEMBER_VARIABLES

    private GUISkin mGUISkin;

    private VideoPlaybackController mVideoPlaybackController;

    private bool mShowScreen = true;

    private const string mText = "This sample application shows how to play a video in\n" +
                                 "AR mode\n\n" +
                                 "Devices that support video on texture can play the\n" +
                                 "video directly on the image target. Other devices will\n" +
                                 "play the video in full screen mode.\n\n" + 
                                 "Note: iOS devices can only play network files in full\n" +
                                 "screen mode.";

    #endregion // PRIVATE_MEMBER_VARIABLES



    #region UNITY_MONOBEHAVIOUR_METHODS

    void Start()
    {
        // Disable the VideoPlaybackController to prevent touches through this screen
        mVideoPlaybackController = GetComponent<VideoPlaybackController>();
        if (mVideoPlaybackController != null) mVideoPlaybackController.enabled = false;

        // load and set gui style
        mGUISkin = Resources.Load("UserInterfaceSkin") as GUISkin;
    }


    void OnApplicationPause(bool pause)
    {
        if (!pause)
        {
            // Show this screen on resume, but not when returning from full screen playback
            if (mVideoPlaybackController != null && !mVideoPlaybackController.CheckWentToFullScreen())
            {
                // Disable the VideoPlaybackController to prevent touches through this screen
                mVideoPlaybackController.enabled = false;

                // Show this screen
                mShowScreen = true;
            }
        }
    }


    void OnGUI()
    {
        if (mShowScreen)
        {
            // scale the help window to fit device screen size
            // because of this scaling, hardcoded values can be used
            float deviceDependentScale = Screen.width / 480f;
            Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
            GUIUtility.ScaleAroundPivot(new Vector2(deviceDependentScale, deviceDependentScale), screenCenter);

            // draw the instruction dialog
            GUI.Window(0, new Rect(screenCenter.x - 230, screenCenter.y - 130, 460, 260),
                DrawWindowContent, "", mGUISkin.window);

            // reset scale after drawing
            GUIUtility.ScaleAroundPivot(Vector2.one, screenCenter);
        }
    }

    #endregion // UNITY_MONOBEHAVIOUR_METHODS



    #region PRIVATE_METHODS

    private void DrawWindowContent(int id)
    {
        // render the text in the window
        GUI.Label(new Rect(0, 20, 460, 240), mText, mGUISkin.label);

        if (GUI.Button(new Rect(240, 210, 210, 40), "START", mGUISkin.button))
        {
            // Re-enable the VideoPlaybackController
            if (mVideoPlaybackController != null) mVideoPlaybackController.enabled = true;

            // Hide this screen
            mShowScreen = false;
        }
    }

    #endregion // PRIVATE_METHODS
}
