# ScrapManagement

NTT DATA Business Solutions Turkey firması için geliştirilen web projesidir. Proje senaryosuna göre, uygulama NTT DATA'nın hizmet verdiği B/S/H firmasının hurdaya ayrılan ürünlerinin takip ve yönetim sürecini içermektedir.

# Kullanılan Teknolojiler

- [.NET Core](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core)

- [Entity Framework Core (Code First)](https://docs.microsoft.com/en-us/ef/core/)

- [MSSQL](https://www.microsoft.com/tr-tr/sql-server/)

- [Identity]( https://docs.microsoft.com/en-us/azure/active-directory/develop/v2-overview)

- Clean Architecture

- FluentValidation

- Design Patterns

# Başlarken

1. [**MSSQL**](https://www.microsoft.com/tr-tr/sql-server/)'in bilgisayarınızda yüklü olduğundan emin olunuz. Ardından aşağıdaki adımları uygulayınız.

2. **ScrapManagement.Web** içerisinde bulunan **appsettings.json** dosyasındaki **"ApplicationConnection"** ve **"IdentityConnection"** içerisinde bulunan veritabanı bağlantı cümlelerini kendinize uygun şekilde düzenleyeniz.

3. **Package Manager Console** veya **terminal** üzerinde aşağıdaki komutu çalıştırıp veritabanı ve içeriğinin kurulmasını sağlayınız.

      ```bash
      update-database
      ```
      
4. **ScrapManagement.Web** klasörü içerisinde bir **terminal** açıp aşağıdaki komut ile projeyi çalıştırabilirsiniz.

      ```bash
      dotnet run
      ```

5. **Login** sayfasında aşağıdaki default kullanıcı bilgilerini kullanarak uygulamaya giriş yapabilirsiniz.

      ```bash
      Admin : admin@admin.com / Admin.12345 / Default
      User  : user@user.com / User.12345 / Default
      ```
