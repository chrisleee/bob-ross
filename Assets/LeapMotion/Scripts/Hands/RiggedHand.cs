﻿/******************************************************************************\
* Copyright (C) Leap Motion, Inc. 2011-2014.                                   *
* Leap Motion proprietary. Licensed under Apache 2.0                           *
* Available at http://www.apache.org/licenses/LICENSE-2.0.html                 *
\******************************************************************************/

using UnityEngine;
using System.Collections;
using Leap;
using UnityStandardAssets.Utility;

// Class to setup a rigged hand based on a model.
public class RiggedHand : HandModel {
	
  public Vector3 modelFingerPointing = Vector3.forward;
  public Vector3 modelPalmFacing = -Vector3.up;

    public UnityStandardAssets.Characters.FirstPerson.HandleInput inputHandle;

  public override void InitHand() {
    UpdateHand();
        inputHandle = new UnityStandardAssets.Characters.FirstPerson.HandleInput();
        inputHandle.Start();
  }

  public Quaternion Reorientation() {
    return Quaternion.Inverse(Quaternion.LookRotation(modelFingerPointing, -modelPalmFacing));
  }

  public override void UpdateHand() {
    if (palm != null) {
      palm.position = GetPalmPosition();
      palm.rotation = GetPalmRotation() * Reorientation();
      InteractionBox interactionBox = controller_.GetFrame().InteractionBox;
            int count = 0;
            foreach(Finger finger in controller_.GetFrame().Hands[0].Fingers)
            {
                
                if (finger.IsExtended)
                {
                    count++;
                }

            }
            if(count == 0)
            {
                Vector normalPoint = interactionBox.NormalizePoint(controller_.GetFrame().Hands[0].PalmPosition);
                if (normalPoint.y > 0.75f)
                {
                    
                    GameObject canvasCube = GameObject.Find("EaselCube");
                    //print(canvasCube.transform.position);
                    
                }
                
            }

            //print(controller_.GetFrame().Hands[0].Fingers[0].TipPosition);
            RaycastHit rayHit;
            if (Physics.Raycast(controller_.transform.TransformPoint(controller_.GetFrame().Hands[0].Fingers[0].TipPosition.ToUnityScaled()), controller_.transform.TransformDirection(controller_.GetFrame().Hands[0].Fingers[0].Direction.ToUnity()), out rayHit))
            {
                UnityStandardAssets.Characters.FirstPerson.HandleInput.drawBrush(rayHit);
            }

        }

    if (forearm != null)
      forearm.rotation = GetArmRotation() * Reorientation();

    for (int i = 0; i < fingers.Length; ++i) {
      if (fingers[i] != null) {
				fingers[i].fingerType = (Finger.FingerType)i;
        fingers[i].UpdateFinger();
			}
		}
  }
}
