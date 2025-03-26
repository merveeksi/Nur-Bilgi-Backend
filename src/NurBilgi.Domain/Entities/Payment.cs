using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NurBilgi.Domain.Enum;

namespace NurBilgi.Domain.Entities
{
    public sealed class Payment 
    {
        public int Id { get; set; }
        public string ExternalPaymentId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }

        // Kart verileri hassas olduğundan, şifrelenmiş olarak saklanmalıdır.
        public string EncryptedCardNumber { get; private set; }
        public string EncryptedCvv { get; private set; }
        
        public string CardHolderName { get; set; }
        
        // Kart son kullanma tarihi: ay ve yıl olarak ayrı tutuluyor.
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }
        
        public PaymentStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public bool IsPaid { get; set; }

        /// <summary>
        /// Kart numarası ve CVV bilgilerini şifreleyerek ayarlar.
        /// </summary>
        /// <param name="cardNumber">Kart numarası</param>
        /// <param name="cvv">CVV kodu</param>
        public void SetCardDetails(string cardNumber, string cvv)
        {
            // Örneğin, burada basit bir Base64 şifreleme kullanıyoruz.
            // Gerçek projede güçlü bir şifreleme yöntemi tercih edilmelidir.
            EncryptedCardNumber = Encrypt(cardNumber);
            EncryptedCvv = Encrypt(cvv);
        }

        /// <summary>
        /// Örnek şifreleme metodu. Gerçek uygulamada güvenli bir yöntem kullanılmalıdır.
        /// </summary>
        private string Encrypt(string input)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
        }

        /// <summary>
        /// Örnek şifre çözme metodu.
        /// </summary>
        private string Decrypt(string encrypted)
        {
            byte[] data = Convert.FromBase64String(encrypted);
            return Encoding.UTF8.GetString(data);
        }

        // Eğer ihtiyaç duyarsan, aşağıdaki metotlarla şifrelenmiş veriyi geri alabilirsin.
        public string GetDecryptedCardNumber()
        {
            return Decrypt(EncryptedCardNumber);
        }

        public string GetDecryptedCvv()
        {
            return Decrypt(EncryptedCvv);
        }
    }
}