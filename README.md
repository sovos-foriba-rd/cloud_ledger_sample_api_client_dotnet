
# Foriba Bulut e-Defter API Test Projesi

Bu proje Foriba Bulut e-Defter API web servis metodlarının nasıl kullanılması gerektiği ile ilgili örnek olması için oluşturulmuştur. Proje yalnızca 
test sisteminde çalışmakta ve web servislere bağlantı ayarları da projede bulunmaktadır.

 **e-Defter Ürünü İçin:**

- Defter kaynak dosyasının sisteme yüklenmesi
- Yüklenen defterlerin statülerinin sorgulanması


işlemleri yapılmaktadır.

Web servis erişim güvenliği basic authentication ile sağlanmaktadır. Web servisleri kullanacak istemcilerin Foriba Bulut e-Defter Portal test sistemi
kullanıcı adı ve şifresine sahip olmaları gerekmektedir. Bu kullanıcı adı ve şifre ile web service doğrulaması gerçekleştirilebilir.


# Kurulum

Bu proje Visual Studio 2015 .Net Framework 4.7 ortamında oluşturulmuştur.

Foriba.OE.CLIENT Projesi Altında Bulunan Servislerin Kurulumu:

-Referans eklemek istenilen projenin References bölümü üzerinde sağ tıklayıp Add Service Reference  veya Visual Studio menüleri üzerinden 
Project-> Add Service Reference takip ederek Servis ekleme ekranına gelinir. 
-Servis ekleme ekranında zip dosyasından çıkartılan WSDL dokümanının dizini belirtilir ve GO butonuna basılır. 
-Sonrasında ekranda WSDL dosyasında bulunan Servis ve Servise ait metodların bir listesi çıkmalıdır.
-Bu metodların kullanılacağı Class’ları içerecek olan Proxy isimlendirmesi NameSpace bölümünden yapılabilir.
-Namespace ServiceReference1 olarak otomatik gelmektedir.(ServiceReference1 alanı farklı bir namespace kullanılırak isteğe bağlı değiştirilebilir.) 
-OK Butonuna basıldığında projeye Servis Referansı eklenmiş olur. 
-Bu adımdan sonra web servis metodlarına ServiceReference1 namespace’i kullanılarak erişilebilir.


# Lisans
  
Foriba Bulut API Test Projesi, **Foriba R&D** ekibi tarafından API kullanımını anlatmak için hazırlanmıştır, izinsiz olarak ticari uygulamalarda kullanılması yasaktır.  
