﻿using Infrastructure.Handler.Abstraction;
#if IOS
using AVFoundation;
using Infrastructure.Handler;
using Infrastructure.Handler.Device.Media.Audio;
#endif
#if ANDROID

#endif
//using Plugin.AudioRecorder;//für anroid, in XAMARIN war es MediaRecorder native aus der Android SDK

namespace Infrastructure.Handler.Device.Media.Audio
{
    //AVFoundation: https://learn.microsoft.com/de-de/dotnet/api/avfoundation.avaudiorecorder?view=xamarin-ios-sdk-12
    //MediaRecord: https://learn.microsoft.com/en-us/dotnet/api/android.media.mediarecorder?view=xamarin-android-sdk-13
    public abstract class AbstractAudioRecorderHandler : AbstractDeviceActionHandler<Permissions.Microphone, Permissions.StorageRead, Permissions.StorageWrite>, IAudioRecorder
    {
        public virtual void PauseRecord()
        {
            throw new NotImplementedException();
        }

        public virtual void ResetRecord()
        {
            throw new NotImplementedException();
        }

        public virtual void StartRecord()
        {
            throw new NotImplementedException();
        }

        public virtual string StopRecord()
        {
            throw new NotImplementedException();
        }
    }
}
