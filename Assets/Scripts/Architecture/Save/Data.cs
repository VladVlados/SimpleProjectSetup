using System;
using Architecture.Utils;

namespace Architecture.Save {
  [Serializable]
  public class Data {
    public SerializableDictionary<ClothingDataType, SerializableDictionary<int, bool>> PurchasedClothing;
    public SerializableDictionary<MiniGamesDataType, bool> MiniGameProgress;
    public int Coins;
    private int _countOfClothingType = 5;

    public Data() {
      Coins = 0;
      MiniGameProgress = new SerializableDictionary<MiniGamesDataType, bool>();
      var miniGamesDataTypes = (MiniGamesDataType[])Enum.GetValues(typeof(MiniGamesDataType));
      foreach (MiniGamesDataType miniGamesDataType in miniGamesDataTypes) {
        MiniGameProgress[miniGamesDataType] = false;

        PurchasedClothing = new SerializableDictionary<ClothingDataType, SerializableDictionary<int, bool>>();
        var clothingDataTypes = (ClothingDataType[])Enum.GetValues(typeof(ClothingDataType));
        foreach (ClothingDataType clothingDataType in clothingDataTypes) {
          PurchasedClothing[clothingDataType] = new SerializableDictionary<int, bool>();
          for (var i = 0; i < _countOfClothingType; i++) {
            PurchasedClothing[clothingDataType].Add(i, false);
          }
        }
      }
    }

    public void IncreaseCoins(int value) {
      Coins += value;
    }

    public void DecreaseCoins(int value) {
      Coins -= value;
    }

    public int GetCoins() {
      return Coins;
    }

    public void SetMiniGameProgress(MiniGamesDataType miniGamesDataType, bool isCompleted) {
      MiniGameProgress[miniGamesDataType] = isCompleted;
    }

    public bool GetMiniGameProgress(MiniGamesDataType miniGamesDataType) {
      return MiniGameProgress[miniGamesDataType];
    }

    public void SetPurchasedClothing(ClothingDataType clothingDataType, int number, bool isBought) {
      SerializableDictionary<int, bool> purchasedClothing = PurchasedClothing[clothingDataType];
      purchasedClothing[number] = isBought;
    }

    public bool GetPurchasedClothing(ClothingDataType clothingDataType, int number) {
      SerializableDictionary<int, bool> purchasedClothing = PurchasedClothing[clothingDataType];
      return purchasedClothing[number];
    }

    public void SetCountOfClothingType(int value) {
      _countOfClothingType = value;
    }
  }

  public enum MiniGamesDataType {
    MushroomGlade,
    BerryPicking,
    Hill
  }

  public enum ClothingDataType {
    Hat,
    Body,
    Shoes
  }
}