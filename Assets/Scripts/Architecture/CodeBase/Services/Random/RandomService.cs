namespace Architecture.CodeBase.Services.Random {
  public class RandomService : IRandomService {
    public int Range(int min, int max) {
      return UnityEngine.Random.Range(min, max + 1);
    }

    public float Range(float min, float max) {
      return UnityEngine.Random.Range(min, max);
    }
  }
}