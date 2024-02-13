# Запуск БД
  Чтобы развернуть БД, необходимо в месте, где находится файл 'docker-compose.yml' в консоли прописать команду 'docker-compose up -d' для создания контейнера с БД, также в внутри модуля TestAPI.Web нужно запустить миграцию на создание БД через команду в терминале 'dotnet ef database update'

# Запуск приложения
  Для того, чтобы запустить приложение, необходимо зайти в папку 'Start' и запустить файл 'TestAPI.Web.exe', после чего перейти в браузере по ссылке  'http://localhost:5000/swagger/index.html'
