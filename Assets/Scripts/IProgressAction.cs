using System;
using UnityEngine;

public class ProgressEventArgs : EventArgs {
    public float progress;
}

public interface IProgressAction {

    public event EventHandler<ProgressEventArgs> OnProgressUpdated;
    
    public float GetProgress();

}
