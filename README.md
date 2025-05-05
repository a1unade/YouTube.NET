## Этап 2 (Flutter)

Страницы:

1) Главная (список с видео)

2) Плеер

3) Подписки

4) Профиль

5) YouTube Premium (+ 2 модалки-заглушки)

6) Авторизация и регистрация

### Главная 

Открывается при запуске приложения, попасть можно через нижний навбар:

<img src="./materials/flutter_1.png" />

При нажатии на видео откроется страница плеера

### Плеер

Для плеера использовали готовое [решение](https://pub.dev/packages/video_player).

<img src="./materials/flutter_2.png" />

### Подписки

Экран с каналами, на которые подписан пользователь, попасть можно через нижний навбар.

<img src="./materials/flutter_3.png" />

### Профиль

Экран профиля пользователя, при выходе из аккаунта на странице будет показано соотвествующее сообщение, с возможностью перехода на экран авторизации.

<img src="./materials/flutter_4.png" />

<img src="./materials/flutter_5.png" />

### YouTube Premium (для оплаты)

Страница с описанием фич подписки, плюс две модалки-заглушки, которые будут использованы для пополнения баланса и покупки подписки

<img src="./materials/flutter_6.png" />

<img src="./materials/flutter_7.png" />

### Авторизация/регистрация

Экраны для авторизации/регистрации без валидации, просто макеты, для входа в аккаунт/регистрации достаточно просто нажать на кнопку

После входа/регистрации - редирект на экран профиля, делали регистрацию с помощью провайдера, вынесли его отдельно, для отслеживания состояния пользователя (авторизован или нет, нужно на экране профиля)

<img src="./materials/flutter_8.png" />

<img src="./materials/flutter_9.png" />

## CI status
![Frontend CI](https://github.com/a1unade/YouTube.NET/actions/workflows/frontend-ci.yml/badge.svg)
[![Codecov](https://codecov.io/gh/a1unade/YouTube.NET/branch/main/graph/badge.svg?flag=frontend)](https://codecov.io/gh/a1unade/YouTube.NET)

![Backend CI](https://github.com/a1unade/YouTube.NET/actions/workflows/backend-ci.yml/badge.svg)
[![Codecov](https://codecov.io/gh/a1unade/YouTube.NET/branch/main/graph/badge.svg?flag=backend)](https://codecov.io/gh/a1unade/YouTube.NET)

## Как запускать проект в Docker

В корневой папке нужно ввести команду ```make docker```, чтобы поднять все контейнеры.

В результате должен быть примерно такой вывод: 

<img src="./materials//7.png">

## Запуск тестов для всего проекта

В корневой папке нужно ввести команду ```make tests```. Запустятся тесты как для фронтенда, так и для бэкенда.

В консоли должен появиться такой вывод с результатми тестов:

<img src="./materials//8.png">