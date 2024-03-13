using System;
using Architecture.CodeBase.Services.GlobalData;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : IStoreListener, IIAPManager {
  private static IStoreController _storeController;
  private static IExtensionProvider _storeExtensionProvider;

  public Action<string> OnBoughtFailed;
  public Action<string> OnBoughtProduct;
  
  private readonly IGlobalDataService _globalDataService;
  
  public IAPManager(IGlobalDataService globalDataService) {
    Debug.Log("IAPManager");
    _globalDataService = globalDataService;
    Initialize();
  }

  private void Initialize() {
    if (_storeController == null) {
      InitializePurchasing();
    }
  }

  public void OnInitialized(IStoreController controller, IExtensionProvider extensions) {
    _storeController = controller;
    _storeExtensionProvider = extensions;
    CheckBoughtProduct();
  }

  public void OnInitializeFailed(InitializationFailureReason error) { }

  public void OnInitializeFailed(InitializationFailureReason error, string message) { }

  public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) {
    Product product = args.purchasedProduct;
    OnBoughtProduct?.Invoke(product.definition.id);
    return PurchaseProcessingResult.Complete;
  }

  public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason) {
    OnBoughtFailed?.Invoke(product.definition.id);
  }

  public bool IsBoughtProduct(string id) {
    if (!PlayerPrefs.HasKey(id)) {
      return false;
    }

    return PlayerPrefs.GetInt(id) == 1;
  }

  private void InitializePurchasing() {
    if (IsInitialized()) {
      return;
    }

    var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
    var purchaseItems = _globalDataService.GetGlobalData<ProductAsset>().GetPurchaseItems();
    foreach (PurchaseItem product in purchaseItems) {
      builder.AddProduct(product.ProductID, product.ItemType);
    }

    UnityPurchasing.Initialize(this, builder);
  }

  private bool IsInitialized() {
    return _storeController != null && _storeExtensionProvider != null;
  }

  private void CheckBoughtProduct() {
    var purchaseItems = _globalDataService.GetGlobalData<ProductAsset>().GetPurchaseItems();
    foreach (PurchaseItem purchase in purchaseItems) {
      if (purchase.ItemType == ProductType.NonConsumable) {
        CheckNonConsumable(purchase.ProductID);
      } else if (purchase.ItemType == ProductType.Subscription) {
        CheckSubscription(purchase.ProductID);
      }
    }
  }

  public void BuyProductID(string productId) {
    if (!IsInitialized()) {
      return;
    }

    Product product = _storeController.products.WithID(productId);
    if (product is { availableToPurchase: true }) {
      _storeController.InitiatePurchase(product);
    }
  }

  private void CheckNonConsumable(string id) {
    if (_storeController == null) {
      return;
    }

    Product product = _storeController.products.WithID(id);
    if (product == null) {
      return;
    }

    PlayerPrefs.SetInt(id, product.hasReceipt ? 1 : 0);
  }

  private void CheckSubscription(string id) {
    if (_storeController == null) {
      return;
    }

    Product subProduct = _storeController.products.WithID(id);
    if (subProduct == null) {
      return;
    }

    if (!subProduct.hasReceipt) {
      return;
    }

    var subManager = new SubscriptionManager(subProduct, null);
    SubscriptionInfo info = subManager.getSubscriptionInfo();
    PlayerPrefs.SetInt(id, info.isSubscribed() == Result.True ? 1 : 0);
  }

  public void RestorePurchases(string productID, Action noBoughtProduct = null, Action restoreProduct = null) {
    if (!IsInitialized()) {
      return;
    }

    if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer) {
      var apple = _storeExtensionProvider.GetExtension<IAppleExtensions>();
      apple.RestoreTransactions(result => {
                                  if (result) {
                                    Product product = _storeController.products.WithID(productID);
                                    if (product.hasReceipt) {
                                      PlayerPrefs.SetInt(productID, 1);
                                      restoreProduct?.Invoke();
                                    } else {
                                      PlayerPrefs.SetInt(productID, 0);
                                      noBoughtProduct?.Invoke();
                                    }
                                  }
                                });
    }
  }
}