using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Messages
{
    public static class FlashMessages
    {
        public static string Success => "Kayıt başarılı";
        public static string SuccessAdd => "Kayıt başarılı bir şekilde eklendi";
        public static string SuccessUpdate => "Kayıt başarılı bir şekilde güncellendi";
        public static string Error => "Kayıt sırasında hata alındı";
        public static string Info => "Bilgilendirme : {0}";
    }
}
