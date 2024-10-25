using System;
using Architecture.CodeBase.Services.GlobalData;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Architecture.CodeBase.Services.Purchase {
  [CreateAssetMenu(fileName = "ProductAssets", menuName = "Product/ProductAsset")]
  public class ProductAsset : ItemGlobalData {
    [SerializeField]
    private PurchaseItem[] _purchaseItems;

    public PurchaseItem[] GetPurchaseItems() {
      return _purchaseItems;
    }
  }

  [Serializable]
  public class PurchaseItem {
    [SerializeField]
    private string _productID;
    [SerializeField]
    private ProductType _itemType;

    public string ProductID {
      get {
        return _productID;
      }
    }

    public ProductType ItemType {
      get {
        return _itemType;
      }
    }
  }
}