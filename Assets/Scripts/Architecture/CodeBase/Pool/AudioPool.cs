using System;
using Architecture.CodeBase.Services.Audio.Components;

namespace Architecture.CodeBase.Pool {
    public class AudioPool : ObjectPool<AudioObject> {
        public AudioPool(Func<AudioObject> objectGenerator) : base(objectGenerator) {}
    }
}
