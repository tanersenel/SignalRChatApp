
# .Net 5 SignalR Chat Sample

Proje .Net 5 Core MVC altyapısı ile SignalR kullanılarak hazırlanmış Sohbet uygulamasıdır.

Projeyi docker üzerinde ayağa kaldırmak için aşağıdaki komut kullanılır. 

    docker-compose  -f "docker-compose.yml" -f "docker-compose.override.yml" up -d

Projede Kullanılan kütüphane ve paketler

 - MongoDB 
 - SignalR
 - Redis

Docker üzerinden ayağa kaldırıldığında
http://localhost:8001 adresinden erişilebilir.
 
 Projeyi localde çalıştırırken 
 appsettings.json dosyası aşağıdaki gibi değiştirilmelidir.
 

    {
      "ChatDatabaseSettings": {
        "ConnectionString": "mongodb://localhost:27017",
        "DatabaseName": "ChatMongoDB"
      },
      "ConnectionStringsCache": {
        "Redis": "localhost:6379"
      },
      "Logging": {
        "LogLevel": {
          "Default": "Information",
          "Microsoft": "Warning",
          "Microsoft.Hosting.Lifetime": "Information"
        }
      },
      "AllowedHosts": "*"
    }


 
 

