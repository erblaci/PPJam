using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ButtonClass
{
   public bool isInverted=false;
   public bool isPressed = false;
   public List<Transform> Line_Points = new List<Transform>();

   public ButtonClass(bool _isInverted, bool _isPressed, List<Transform> _Line_Points)
   {
      this.isInverted = _isInverted;
      this.isPressed = _isPressed;
      this.Line_Points = _Line_Points;
   }
}
