namespace Player47.Core
{
    public interface IAudioEmitter
    {
        bool IsPlaying { get; }

        void PlaySound();
    }
}