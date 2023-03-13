<div id="MainTitle">

# Мой интернет - профиль

</div>
<div id="SubTitle"> 

### Веб - страница моего профиля с автообновлением контента через инструменты Github API

</div>

Данное приложение проводит опрос Github API согласно расписанию установленному в файле ```MyProfile/UserConfiguration.Production.json```,
При наличии новых репозиториев или изменениях в них производиться добавление/обновение соответственно.
При загрузке веб-страницы происходит запрос данных пользователя о репозиториях и создается Timeline проектов на вкладке "projects"

---

## Технологический стек:
<div id="TechStack">

* React
* ASP.NET Core
* xUnit

</div>

## Как запустить?

1. Скопировать репозиторий коммандой ```git clone https://github.com/NoTh0ughts/My-profile My-profile```
2. Установить имя пользоватея в файле ```MyProfile/UserConfiguration.Production.json```
3. Перейти в дирректорию  MyProfile
4. Запустить контейнер коммандой ```docker-compose up```
5. Теперь страница доступна по адресу localhost:50000
