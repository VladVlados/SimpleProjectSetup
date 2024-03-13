using System;

namespace Architecture.CodeBase.Services.Purchase {
  public interface IIAPManager : IService {
    bool IsBoughtProduct(string id);
    void BuyProductID(string productId);
    void RestorePurchases(string productID, Action noBoughtProduct = null, Action restoreProduct = null);


  }
}