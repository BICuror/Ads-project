using UnityEngine;

public class AddsTimer
{
    public AddsTimer(float timeout)
    {
        _timeout = timeout;
    }
    
    private float _timeout;
    
    private float _lastTimeActivated = float.MinValue;

    public bool CanBeShown()
    {
        if (_lastTimeActivated + _timeout <= Time.fixedTime)
        {
            _lastTimeActivated = Time.fixedTime;

            return true;
        }
        
        return false;
    }
}
