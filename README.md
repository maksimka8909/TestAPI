# Запуск БД
  Чтобы развернуть БД, необходимо в месте, где находится файл 'docker-compose.yml' в консоли прописать команду 'docker-compose up -d' для создания контейнера с БД, также внутри модуля TestAPI.Data нужно создать и запустить миграцию на создание БД через команды в терминале 'dotnet ef migrations add InitialCreate --startup-project ..\TestAPI.Web\TestAPI.Web.csproj' и ' dotnet ef database update --startup-project ..\TestAPI.Web\TestAPI.Web.csproj'

# Запуск приложения
  Для того, чтобы запустить приложение, необходимо зайти в папку 'Start' и запустить файл 'TestAPI.Web.exe', после чего перейти в браузере по ссылке  'http://localhost:5000/swagger/index.html'
