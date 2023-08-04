using RestSharp;
using System.Globalization;
using System.Security.Cryptography;
namespace sanalpos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Finansbank();
            string MerchantID = "7000679";
            string ProvUserID = "PROVAUT";
            string ProvisionPassword = "123qweASD";
            string TerminalID = "30691297";
            string StoreKey = "12345678";
            string KullanıcıAdı = "99999999999";
            string Parola = "Destek02";
            string Şifre = "147852";
            string mode = "TEST";
            string apiversion = "512";
            string secure3dsecuritylevel = "3D_PAY";
            string terminalprovuserid = "PROVAUT";
            string terminaluserid = "GARANTI";
            string terminalmerchantid = "7000679";
            string terminalid = "30691297";
            string orderid = Guid.NewGuid().ToString();
            string successurl = "https://localhost:44315/api/Products/payment";
            string errorurl = "https://localhost:44315/api/Products/payment";
            string customeremailaddress = "eticaret@garanti.com.tr";
            string customeripaddress = "192.168.0.1";
            string companyname = "GARANTI TEST";
            string lang = "tr";
            string txntimestamp = "2023-04-30T11:31:53Z";
            string refreshtime = "1";
            ulong txnamount = 100;
            string txntype = "sales";
            int txncurrencycode = 949;
            int txninstallmentcount = 1;
            string secure3dhash = GarantiHash.GetHashData(ProvisionPassword, TerminalID, "ef43ef579b97484d9f67d445e4b15b9321", txninstallmentcount, StoreKey, txnamount, txncurrencycode, successurl, txntype, errorurl);
            string cardholdername = "Test User";
            string cardnumber = "5406697543211173";
            string cardexpiredatemonth = "03";
            string cardexpiredateyear = "23";
            string cardcvv2 = "465";

            var options = new RestClientOptions()
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("https://sanalposprovtest.garantibbva.com.tr/servlet/gt3dengine", Method.Post);
            request.AlwaysMultipartFormData = true;
            request.AddParameter("mode", "TEST");
            request.AddParameter("apiversion", "512");
            request.AddParameter("secure3dsecuritylevel", "3D_PAY");
            request.AddParameter("terminalprovuserid", "PROVAUT");
            request.AddParameter("terminaluserid", "GARANTI");
            request.AddParameter("terminalmerchantid", "7000679");
            request.AddParameter("terminalid", "30691297");
            request.AddParameter("orderid", "ef43ef579b97484d9f67d445e4b15b9321");
            request.AddParameter("successurl", "https://localhost:44315/api/Products/payment");
            request.AddParameter("errorurl", "https://localhost:44315/api/Products/payment");
            request.AddParameter("customeremailaddress", "eticaret@garanti.com.tr");
            request.AddParameter("customeripaddress", "192.168.0.1");
            request.AddParameter("companyname", "GARANTI TEST");
            request.AddParameter("lang", "tr");
            request.AddParameter("txntimestamp", "2023-08-4T14:45:53Z");
            request.AddParameter("refreshtime", "1");
            request.AddParameter("secure3dhash", "33C80ED09C18F6879997F35F54E85D8FE93AB6C42BD86A1322E723C3DDAFD0FA5B8FD309AF1F638A33E146FB313447F55D4C432A33C4A0BB613D8674596FCC07");
            request.AddParameter("txnamount", "100");
            request.AddParameter("txntype", "sales");
            request.AddParameter("txncurrencycode", "949");
            request.AddParameter("txninstallmentcount", "");
            request.AddParameter("cardholdername", "Test User");
            request.AddParameter("cardnumber", "5406697543211173");
            request.AddParameter("cardexpiredatemonth", "03");
            request.AddParameter("cardexpiredateyear", "23");
            request.AddParameter("cardcvv2", "465");
            RestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
        }

        private static void Finansbank()
        {
            String MbrId = "5";//Kurum Kodu
            String MerchantID = "085300000009597";  //Üye işyeri numarası
            String MerchantPass = "12345678";  //Üye işyeri 3D şifresi
            String UserCode = "QNB_API_KULLANICI";  //Kullanıcı Kodu
            String UserPass = "FwCX2";  //Kullanıcı Şifre
            String SecureType = "3DPay"; //Güvenlik tipi
            String TxnType = "Auth"; //İşlem  Tipi
            String InstallmentCount = "1";   //Taksit Sayısı
            String Currency = "949";  //Para Birimi
            String OkUrl = "https://localhost:44315/api/Products/payment";//Başarılı işlem URL
            String FailUrl = "https://localhost:44315/api/Products/payment";//Başarısız işlem URL
            String OrderId = Guid.NewGuid().ToString();//Sipariş Numarası
            //String OrderId = "";  // İşlem  Sipariş Numarası
            String PurchAmount = "1";//Tutar
            String Lang = "TR";  //Dil bilgisi

            String rnd = DateTime.Now.Ticks.ToString();
            String str = MbrId + OrderId + PurchAmount + OkUrl + FailUrl + TxnType + InstallmentCount + rnd + MerchantPass;
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(str);
            byte[] hashingbytes = sha.ComputeHash(bytes);
            String hash = Convert.ToBase64String(hashingbytes);
            var options = new RestClientOptions()
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("https://vpostest.qnbfinansbank.com/Gateway/Default.aspx", Method.Post);
            request.AlwaysMultipartFormData = true;
            request.AddParameter("Pan", "9792091234123455");
            request.AddParameter("Cvv2", "123");
            request.AddParameter("Expiry", "1220");
            request.AddParameter("MbrId", MbrId);
            request.AddParameter("MerchantID", MerchantID);
            request.AddParameter("MerchantPass", MerchantPass);
            request.AddParameter("UserCode", UserCode);
            request.AddParameter("UserPass", UserPass);
            request.AddParameter("SecureType", SecureType);
            request.AddParameter("TxnType", TxnType);
            request.AddParameter("InstallmentCount", InstallmentCount);
            request.AddParameter("Currency", Currency);
            request.AddParameter("OkUrl", OkUrl);
            request.AddParameter("FailUrl", FailUrl);
            request.AddParameter("OrderId", OrderId);
            request.AddParameter("PurchAmount", PurchAmount);
            request.AddParameter("Lang", Lang);
            request.AddParameter("Rnd", rnd);
            request.AddParameter("Hash", hash);
            RestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
        }
    }
}