// namespace BaseDotNetApp.Lecture7;
//
// public class DryBadExample
// {
//     
// }
//
// public class OrderProcessingService
// {
//     public decimal CalculateTotalPrice(decimal baseAmount, string customerTier, int accumulatedLoyaltyPoints)
//     {
//         // Небезпечне дублювання бізнес-правила розрахунку знижки
//         decimal discountPercentage = 0m;
//         if (customerTier == "VIP")
//         {
//             discountPercentage = 0.15m;
//         }
//         else if (customerTier == "Premium" && accumulatedLoyaltyPoints > 500)
//         {
//             discountPercentage = 0.10m;
//         }
//
//         return baseAmount - (baseAmount * discountPercentage);
//     }
// }
//
// public class InvoiceGenerationService
// {
//     public string GenerateFormattedInvoice(decimal baseAmount, string customerTier, int accumulatedLoyaltyPoints)
//     {
//         // Абсолютно ідентична логіка дублюється у генераторі інвойсів, створюючи друге джерело правди
//         decimal discountPercentage = 0m;
//         if (customerTier == "VIP")
//         {
//             discountPercentage = 0.15m;
//         }
//         else if (customerTier == "Premium" && accumulatedLoyaltyPoints > 500)
//         {
//             discountPercentage = 0.10m;
//         }
//
//         decimal finalAmount = baseAmount - (baseAmount * discountPercentage);
//         return $"Дякуємо за покупку! До сплати: {finalAmount:C}";
//     }
// }
