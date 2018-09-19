using System.Collections;
using UnityEngine;

/// <summary>
/// Vibration strength.
/// </summary>
public enum VibrationForce
{
    Light,
    Medium,
    Hard,
}

public class Feedback : MonoBehaviour
{
    // Declare variables.
    [SerializeField] private OVRInput.Controller _controllerMask;
    private OVRHapticsClip _clipLight;
    private OVRHapticsClip _clipMedium;
    private OVRHapticsClip _clipHard;

    /// <summary>
    /// Initializing.
    /// </summary>
    private void Start()
    {
        InitializeOvrVibration();
    }

    /// <summary>
    /// Initializing.
    /// </summary>
    private void OnEnable()
    {
        InitializeOvrVibration();
    }
    
    /// <summary>
    /// Initialize strengths of vibration.
    /// </summary>
    private void InitializeOvrVibration()
    {
        // Declare variables.
        const int count = 10;
        
        // Sets vibration clips by count.
        _clipLight = new OVRHapticsClip(count);
        _clipMedium = new OVRHapticsClip(count);
        _clipHard = new OVRHapticsClip(count);
        
        // Set samples with vibration strengths.
        for (int i = 0; i < count; i++)
        {
            _clipLight.Samples[i] = i % 2 == 0 ? (byte)0 : (byte)45;
            _clipMedium.Samples[i] = i % 2 == 0 ? (byte)0 : (byte)100;
            _clipHard.Samples[i] = i % 2 == 0 ? (byte)0 : (byte)180;
        }

        // Set the vibration strengths by samples.
        _clipLight = new OVRHapticsClip(_clipLight.Samples, _clipLight.Samples.Length);
        _clipMedium = new OVRHapticsClip(_clipMedium.Samples, _clipMedium.Samples.Length);
        _clipHard = new OVRHapticsClip(_clipHard.Samples, _clipHard.Samples.Length);
    }

    /// <summary>
    /// Vibration.
    /// </summary>
    /// <param name="vibrationForce">Strengths of vibration.</param>
    public void Vibrate(VibrationForce vibrationForce)
    {
        // Declare variables.
        OVRHaptics.OVRHapticsChannel channel = OVRHaptics.RightChannel;

        if (!Setting.Vibration)
            return;
        
        // When you don't set the vibration hand, then vibrate the default hand (right hand).
        if (_controllerMask == OVRInput.Controller.LTouch)
            channel = OVRHaptics.LeftChannel;

        // Vibration effect start and end.
        switch (vibrationForce)
        {
            case VibrationForce.Light:
                channel.Preempt(_clipLight);
                break;
            case VibrationForce.Medium:
                channel.Preempt(_clipMedium);
                break;
            case VibrationForce.Hard:
                channel.Preempt(_clipHard);
                break;
            default:
                return;
        }
    }

    /// <summary>
    /// Vibration with time.
    /// </summary>
    /// <param name="force">Strengths of vibration.</param>
    /// <param name="time">Duration of the vibration.</param>
    /// <returns>The duration seconds of the vibration.</returns>
    public IEnumerator VibrateTime(VibrationForce force, float time)
    {
        // Declare variables.
        OVRHaptics.OVRHapticsChannel channel = OVRHaptics.RightChannel;
        
        // When you don't set the vibration hand, then vibrate the default hand (right hand).
        if (_controllerMask == OVRInput.Controller.LTouch)
            channel = OVRHaptics.LeftChannel;

        // Vibration effect with time start and end.
        for (float t = 0; t <= time; t += Time.deltaTime)
        {
            switch (force)
            {
                case VibrationForce.Light:
                    channel.Queue(_clipLight);
                    break;
                case VibrationForce.Medium:
                    channel.Queue(_clipMedium);
                    break;
                case VibrationForce.Hard:
                    channel.Queue(_clipHard);
                    break;
                default:
                    yield return null;
                    break;
            }
        }
        yield return new WaitForSeconds(time);
        
        // Clear all set channels.
        channel.Clear();
        yield return null;
    }
}